using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Forms;
using TaskManagment.Models;

namespace TaskManagment.Forms
{
    public partial class TeamLeaderWorkers : Form
    {
        int numHours = 0;
        public User worker;
        dynamic hoursList;
        float maxHours;
        Button confirmation;
        public TeamLeaderWorkers(User w)
        {
            InitializeComponent();
            worker = w;
            this.Text = $"{Global.CurrentWorker.Name} worker name: {worker.Name}";
            UpdateWorkerDeatails();
            getWorkerHours();
        }

        /// <summary>
        /// show worker's hours
        /// </summary>
        private void getWorkerHours()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(Global.path);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync($"getWorkerHours/{Global.CurrentWorker.Id}/{worker.Id}").Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                hoursList = JsonConvert.DeserializeObject(result);
                if (hoursList != null&&result!="[]")
                {
                    dgv_workerHours.DataSource = hoursList;
                    dgv_workerHours.Columns["Id"].Visible = false;
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        /// <summary>
        /// shou worker deatails
        /// </summary>
        private void UpdateWorkerDeatails()
        {
            lbl_workerName.Text = worker.Name;
            lbl_workerUserName.Text = worker.UserName;
            lbl_workerEmail.Text = worker.EMail;
            lbl_job.Text = Global.jobs.Find(j => j.Id == worker.StatusId).Name;
        }

        ///validation to numHours
        private void onlyNumbers(object sender, KeyPressEventArgs e)
        {
            Global.onlyNumbers(sender, e);
        }
        public void checkNumHours(object sender, EventArgs e)
        {
            float.TryParse((sender as TextBox).Text, out float f);
            if (f > maxHours + numHours)
            confirmation.Enabled = false;
            else   confirmation.Enabled = true;

        }

        public int ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Width = 400, Text = $" {text} (max hours: {maxHours + numHours}):" };
            Label textLabelMessege = new Label() { Left = 50, Top = 65, Width = 400 };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 50, Text = numHours.ToString() };
            textBox.KeyPress += onlyNumbers;
            textBox.TextChanged += checkNumHours;
            confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) =>
            {
                prompt.Close();
            };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(textLabelMessege);
            prompt.AcceptButton = confirmation;
            return prompt.ShowDialog() == DialogResult.OK ? Convert.ToInt32(textBox.Text) : numHours;
        }
        
        //get maximum hours for the project 
        private void GetRemainingHours(int projectWorkerId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(Global.path);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync($"getRemainingHours/{projectWorkerId}/{worker.StatusId}").Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                maxHours = JsonConvert.DeserializeObject<float>(result);
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        private void dgv_workerHours_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            numHours = Convert.ToInt32(hoursList[e.RowIndex]["allocatedHours"].Value);
            int projectWorkerId = Convert.ToInt32(hoursList[e.RowIndex]["Id"].Value);
            GetRemainingHours(projectWorkerId);
            numHours = ShowDialog("enter allocate hours", "change allocate hours");
            //update in dataBase
            if (numHours != Convert.ToInt32(hoursList[e.RowIndex]["allocatedHours"].Value))
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(Global.path + "updateWorkerHours");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "PUT";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "{\"projectWorkerId\":\"" + projectWorkerId + "\"," +
                       "\"numHours\":\"" + numHours + "\"}";
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                try
                {
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        hoursList[e.RowIndex]["allocatedHours"].Value = numHours;
                        var result = streamReader.ReadToEnd();
                        dynamic obj = JsonConvert.DeserializeObject<User>(result);
                        dgv_workerHours.DataSource = hoursList;
                    }
                }
                catch (WebException ex)
                {

                }
            }
        }
    }

}



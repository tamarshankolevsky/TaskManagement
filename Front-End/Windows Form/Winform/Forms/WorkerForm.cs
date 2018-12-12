using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Forms;
using TaskManagment.Forms.Work;

namespace TaskManagment.Forms
{
    public partial class WorkerHome : Form
    {
        bool isBegin = true;
        DateTime startdate;
        dynamic projectList;
        int projectId;
        double allocatedHours;
        string[] hours;
        bool IsmoreThenAllocatedHours = false;
        public WorkerHome()
        {
            InitializeComponent();
            this.Text = Global.CurrentWorker.Name;
            GetProjects();

        }
        /// <summary>
        /// get the worker's projects and show them in dataGridView
        /// </summary>
        private void GetProjects()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(Global.path);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync($"getProject/{Global.CurrentWorker.Id}").Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                projectList = JsonConvert.DeserializeObject(result);
                if (projectList != null)
                {
                    dgv_task.DataSource = projectList;
                    dgv_task.Columns["Id"].Visible = false;
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        /// <summary>
        ///update presence for project
        /// </summary>
        private void UpdateTime()
        {
            lbl_message.Text = "";
            DateTime time = DateTime.Now;
            //if the worker start work
            if (isBegin)
            {
                startdate = DateTime.Now;
                try
                {
                    projectId = int.Parse((String)dgv_task.SelectedRows[0].Cells[0].FormattedValue);
                    allocatedHours = double.Parse((String)dgv_task.SelectedRows[0].Cells[4].FormattedValue);////??????????
                    if ((String)dgv_task.SelectedRows[0].Cells[3].FormattedValue == "")
                        hours[0] = "00";
                    else
                        hours = ((String)dgv_task.SelectedRows[0].Cells[3].FormattedValue).Split(':');
                    lbl_beginningTime.Text = startdate.ToString("hh:mm:ss tt");
                    btn_task.Text = "end Task";
                }
                catch
                {
                    lbl_message.Text = "choose project to start";
                    return;
                }
            }
            //if is finish
            else
            {
                btn_task.Text = "Start Task";
                lbl_beginningTime.Text = "";
                lbl_timer.Text = "";
                IsmoreThenAllocatedHours = false;
            }
            timer.Enabled = !timer.Enabled;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create($"{Global.path}updateStartHour");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"hour\":\"" + time + "\"," +
                "\"idProjectWorker\":\"" + projectId + "\"," +
                "\"isFirst\":\"" + isBegin + "\"}";
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    if (!isBegin)
                    {
                        GetProjects();
                        if (projectList != null)
                            UpdateChart();
                    }
                    isBegin = !isBegin;
                }
            }
            catch { }
        }

        /// <summary>
        /// show project-chart
        /// </summary>
        void UpdateChart()
        {
            Dictionary<string, int> allocatedHours = new Dictionary<string, int>();
            List<float> workedHours = new List<float>();
            foreach (var item in projectList)
            {
                allocatedHours.Add((String)item["Name"].Value, (Int32)item["allocatedHours"].Value);
                if (item.Hours != "")
                {
                    var t = item["Hours"].Value.Split(':');
                    workedHours.Add(float.Parse(t[0]) + (float.Parse(t[1]) / 100));
                }
                else workedHours.Add(0);
            }
            chart1.Series[0].Points.DataBindXY(allocatedHours.Keys, allocatedHours.Values);
            chart1.Series[1].Points.DataBindXY(allocatedHours.Keys, workedHours);
        }

        private void WorkerHome_Load(object sender, EventArgs e)
        {
            if (projectList != null)
                UpdateChart();
        }

        private void btn_task_Click(object sender, EventArgs e)
        {
            UpdateTime();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            var d = (DateTime.Now - startdate);
            int h = Convert.ToInt32(hours[0]) + d.Hours;
            int m = Convert.ToInt32(hours[1]) + d.Minutes;
            if (m > 59)
            {
                m -= 60;
                h++;
            }
            if (Convert.ToDouble(h + "." + m % 100) >= allocatedHours && !IsmoreThenAllocatedHours)
            {
                timer.Enabled = false;
                if (MessageBox.Show("you work more them allocated hours Are you whant continue?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    IsmoreThenAllocatedHours = true;
                    timer.Enabled = true;
                }
                else
                {
                    timer.Enabled = true;
                    UpdateTime();
                }
            }
            lbl_timer.Text = d.ToString(@"hh\:mm\:ss");
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            WorkEmail w = new WorkEmail();
            w.Show();
        }

        private void WorkerHome_FormClosing(object sender, FormClosingEventArgs e)
        {
            //When closing the page,  finish the project
            if (!isBegin)
            {
                UpdateTime();
            }
        }

        private void btn_logOut_Click(object sender, EventArgs e)
        {
            Global.LogOut();
        }
    }
}

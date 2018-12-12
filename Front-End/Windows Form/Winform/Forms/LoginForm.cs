using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Forms;
using System.Xml.Linq;
using TaskManagment.Forms;
using TaskManagment.Models;

namespace TaskManagment
{
    public partial class LogIn : Form
    {
        public LogIn()
        {
            InitializeComponent();
            btn_logIn.Enabled = false;
            txt_password.PasswordChar = '*';
            txtNewPassword.PasswordChar = '*';
            txtConfirmPassword.PasswordChar = '*';
            lblChangePassword.Visible = false;
            try { 
            var doc = XDocument.Load("user.xml");
            foreach (var u in doc.Descendants("CurrentWorker"))
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Global.path);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync($"getWorkerDetails/{u.Value}").Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    Global.CurrentWorker = JsonConvert.DeserializeObject<User>(result);
                    OpenCurrectPage();
                }
            }
        }  
            catch { }
        }

        #region validations
        public void checkValidate(object sender, EventArgs e)
        {
            lbl_bad_request.Text = "";
            errorProvider1.Clear();
                bool b=Global.checkVaidationLength(2, 10, txt_userName) &&
                Global.checkVaidationLength(6, 10, txt_password);
            btn_logIn.Enabled = b;
            btnChange.Enabled = Global.checkVaidationLength(6, 10, txtNewPassword) &&
                Global.checkVaidationLength(6, 10, txtConfirmPassword) &&
             btn_logIn.Enabled;
            if (txtConfirmPassword.Text != txtNewPassword.Text)
            {
                errorProvider1.SetError(txtConfirmPassword, "confirm password must be same new password");
                btnChange.Enabled = false;
            }
        }

        #endregion
        private void btn_logIn_Click(object sender, EventArgs e)
        {
          //  try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(Global.path + "login");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "{\"userName\":\"" + txt_userName.Text + "\"," +
                       "\"password\":\"" + Global.sha256(txt_password.Text) + "\"}";
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                try
                {
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        Global.CurrentWorker = JsonConvert.DeserializeObject<User>(result);
                        new XDocument(new XElement("root",new XElement
                            ("CurrentWorker", Global.CurrentWorker.Id))).Save("user.xml");
                        OpenCurrectPage();
                    }
                }
                catch (WebException ex)
                {
                    lbl_bad_request.Text = "One or more of the data is incorrect";
                }
            }
          //  catch (Exception exe)
          //  {
           //     lbl_bad_request.Text = "The service is not connected";
           // }
        }

        public void OpenCurrectPage()
        {
            switch (Global.CurrentWorker.StatusId)
            {
                case 1:
                    {
                        ManagerHome managerHome = new ManagerHome();
                        managerHome.Show();
                        break;
                    }

                case 2:
                    {
                        TeamLeaderHome teamLeader = new TeamLeaderHome();
                        teamLeader.Show();
                        break;
                    }

                default:
                    {
                        WorkerHome worker = new WorkerHome();
                        worker.Show();
                        break;
                    }
            }
        }
        
        /// <summary>
        /// change controls from login to change password
        /// </summary>
        /// <param name="b"></param>
        void changeControls(bool b)
        {
            lblPassword.Text = b ? "oldPassword" : "password";
            lblConfirmPassword.Visible = b;
            lblNewPassword.Visible = b;
            txtNewPassword.Visible = b;
            txtConfirmPassword.Visible = b;
            btn_logIn.Visible = !b;
            btnChange.Visible = b;
            lbl_closeChangePasswod.Visible = b;
            lblChangePassword.Visible = !b;
        }

        /// <summary>
        /// change password
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChange_Click(object sender, EventArgs e)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Global.path + "updatePassword");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"userName\":\"" + txt_userName.Text + "\"," +
                       "\"oldpassword\":\"" + Global.sha256(txt_password.Text) + "\"," +
                       "\"newPassord\":\"" + Global.sha256(txtNewPassword.Text) + "\"}";
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    MessageBox.Show($"the password changed");
                    txt_password.Text = txtNewPassword.Text;
                    changeControls(false);
                }
            }
            catch (WebException ex)
            {
                lbl_bad_request.Text = "One or more of the data is incorrect";
            }
        }

        private void lblChangePassword_Click(object sender, EventArgs e)
        {
            changeControls(true);

        }

        private void lbl_closeChangePasswod_Click(object sender, EventArgs e)
        {
            changeControls(false);
        }

       
    }

}




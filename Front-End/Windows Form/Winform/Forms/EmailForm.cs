using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace TaskManagment.Forms.Work
{
    public partial class WorkEmail : Form
    {
        public WorkEmail()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Global.path + "sendMsg");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"sub\":\"" + txt_subject.Text + "\"," +
                   "\"body\":\"" + txt_contact.Text + "\"," +
                   "\"id\":\"" + Global.CurrentWorker.Id + "\"}";
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    MessageBox.Show("Your email sent successfully!!!");
                }
            }
            catch (WebException ex)
            {
            }
            this.Close();
        }

    }
}

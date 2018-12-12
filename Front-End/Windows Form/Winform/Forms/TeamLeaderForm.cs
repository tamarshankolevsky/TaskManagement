using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Forms;
using TaskManagment.Models;


namespace TaskManagment.Forms
{
    public partial class TeamLeaderHome : Form
    {
        List<Project> projectList;
        List<User> workerList;
        private string status="Status";

        public TeamLeaderHome()
        {
            InitializeComponent();
            this.Text = Global.CurrentWorker.Name;
            getProject();
        }
        
        /// <summary>
        /// show all teamLeader's workers
        /// </summary>
        private void getWorkers()
        {
            lbl_click.Text = "click on worker to show deatails";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(Global.path);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync($"GetWorkersDeatails/{Global.CurrentWorker.Id}").Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                workerList = JsonConvert.DeserializeObject<List<User>>(result);
                dgv_Deatails.DataSource = workerList;
                dgv_Deatails.Columns["Id"].Visible = false;
                dgv_Deatails.Columns["ManagerId"].Visible = false;
                dgv_Deatails.Columns[3].HeaderText = "Job";
                for (int i = 0; i < workerList.Count; i++)
                {
                   dgv_Deatails.Rows[i].Cells[3].Value = Global.jobs.Find(j => j.Id == (int)dgv_Deatails.Rows[i].Cells[4].Value).Name;
                }
                dgv_Deatails.Columns["StatusId"].Visible = false;
                dgv_Deatails.RowHeaderMouseClick -= dgv_projects_RowHeaderMouseClick;
                dgv_Deatails.RowHeaderMouseClick -= dgv_Deatails_RowHeaderMouseClick;
                dgv_Deatails.RowHeaderMouseClick += dgv_Deatails_RowHeaderMouseClick;
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

        }
        /// <summary>
        /// show all teamLeader's projects
        /// </summary>
        private void getProject()
        {
            lbl_click.Text = "click on project to show deatails";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(Global.path);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync($"getProjectDeatails/{Global.CurrentWorker.Id}").Result;
            if (response.IsSuccessStatusCode)
            {
                string[] r = new string[] { "1", "hh", "jj" };
                var result = response.Content.ReadAsStringAsync().Result;
                projectList = JsonConvert.DeserializeObject<List<Project>>(result);
                dgv_Deatails.DataSource = projectList;
                dgv_Deatails.Columns["Id"].Visible = false;
                dgv_Deatails.Columns["TeamLeaderId"].Visible = false;
                dgv_Deatails.RowHeaderMouseClick -= dgv_Deatails_RowHeaderMouseClick;
                dgv_Deatails.RowHeaderMouseClick -= dgv_projects_RowHeaderMouseClick;
                dgv_Deatails.RowHeaderMouseClick += dgv_projects_RowHeaderMouseClick;
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        private void dgv_projects_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            TeamLeaderProjectDeatails p = new TeamLeaderProjectDeatails(projectList[e.RowIndex]);
            p.Show();
        }

        private void dgv_Deatails_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            TeamLeaderWorkers w = new TeamLeaderWorkers(workerList[e.RowIndex]);
            w.Show();
        }

        private void projectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getProject();
        }

        private void workersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getWorkers();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Global.LogOut();
        }
    }


}

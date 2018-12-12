using System.ServiceProcess;

namespace MailingService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            SchedularManager.StartSchedular();
        }
        public void OnDebug()
        {
            OnStart(null);
        }

        protected override void OnStop()
        {
        }
    }
}

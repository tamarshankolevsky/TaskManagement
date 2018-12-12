using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailingService
{
    class Email
    {
        public List<string> employeesEmail { get; set; }
        public string teamLeaderEmail { get; set; }
        public string projectName { get; set; }
        public DateTime endDate { get; set; }
    }
}

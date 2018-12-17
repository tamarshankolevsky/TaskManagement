using System;
using System.Collections.Generic;

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

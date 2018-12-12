using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seldat.MDS.Connector.Models.Messages.MessagesDistribution
{
    public enum StatusMessage
    {
        MessageSent = 1,
        MdsError = 2
    }
    public class BNResponse
    {
        public StatusMessage Status { get; set; }
        public string Description { get; set; }
        public Dictionary<string, object> ParametersValue { get; set; }
    }
}

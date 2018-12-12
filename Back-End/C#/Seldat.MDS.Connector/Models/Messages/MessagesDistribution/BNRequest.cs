using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seldat.MDS.Connector.Models.Messages.MessagesDistribution
{
    [Flags]
    public enum MessageParams
    {
        Receiver = 1,
        Destination = 2,
        content = 4,
        type = 8
    }
    public class BNRequest
    {
        public string Url { get; set; }
        public MessageParams MessageParams { get; set; }
    }
}

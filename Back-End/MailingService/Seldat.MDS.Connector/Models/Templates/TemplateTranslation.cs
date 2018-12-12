using System.Collections.Generic;

namespace Seldat.MDS.Connector
{
    public class TemplateTranslation
    {
        public List<Attachment> Attachments { get; set; }

        public int Id { get; set; }

        public Language Language { get; set; }

        public int TemplateId { get; set; }

        public string Body { get; set; }

        public string Subject { get; set; }

        public IDictionary<string, object> Parameters { get; set; }
    }
}
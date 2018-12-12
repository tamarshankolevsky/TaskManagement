using System.Collections.Generic;

namespace Seldat.MDS.Connector
{
    public enum TemplateType
    {
        Email = 1,
        Simple = 2,
        PN = 3
    }
    public class Template : BaseModel
    {
        public List<TemplateTranslation> Translations { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public TemplateType Type { get; set; }
        public int AccountId { get; set; }
        public bool Unsubscribe { get; set; }
        public bool IsMaster { get; set; }
    }
}

namespace Seldat.MDS.Connector
{
    public enum AttachmentType
    {
        Template,
        Message
    }
    public abstract class Attachment
    {
        public int Id { get; set; }
        public string FileId { get; set; }
        public string FileName { get; set; }

        public abstract AttachmentType Type { get; }
    }
}

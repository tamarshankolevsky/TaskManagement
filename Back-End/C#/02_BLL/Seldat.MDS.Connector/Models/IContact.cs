namespace Seldat.MDS.Connector
{
    public interface IContact
    {
        int Id { get; }
        RecipientType Type { get; }
    }
}
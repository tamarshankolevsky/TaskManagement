namespace Seldat.MDS.Connector
{
    public interface IServerConfiguration
    {
        string Host { get; }
        int Port { get; }
        string Username { get; }
        string Password { get; }
        string From { get; }
        bool EnableSsl { get; }
        string FromDisplayName { get; }
    }
}
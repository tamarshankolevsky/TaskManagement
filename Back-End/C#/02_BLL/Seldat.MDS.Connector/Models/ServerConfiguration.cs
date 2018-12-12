namespace Seldat.MDS.Connector
{
    public class ServerConfiguration : IServerConfiguration
    {
        public int Id { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string From { get; set; }
        public bool EnableSsl { get; set; }
        public string FromDisplayName { get; set; }
    }
}

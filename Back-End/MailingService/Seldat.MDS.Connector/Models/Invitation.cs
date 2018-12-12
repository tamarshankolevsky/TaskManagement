namespace Seldat.MDS.Connector
{
    public class Invitation : BaseModel
    {
        public int AccountId { get; set; }
        public string Email { get; set; }
        public string RoleId { get; set; }
    }
}

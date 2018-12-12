using System;

namespace Seldat.MDS.Connector
{
    public abstract class  BaseModel
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public User CreatedBy { get; set; }
    }
}

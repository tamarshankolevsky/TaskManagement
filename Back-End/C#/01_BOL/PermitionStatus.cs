using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _01_BOL
{
    public class PermitionStatus
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("statuses")]
        public int StatusId { get; set; }

        [ForeignKey("PermitionId")]
        public int PermitionId { get; set; }

    }
}

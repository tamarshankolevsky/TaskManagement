using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _01_BOL
{
  public  class UserProject
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }

        [ForeignKey("Projects")]
        public Project ProjectId { get; set; }

        public int AllocatedHours { get; set; }

        public List<User> Workers { get; set; }

        public List<Project> Projects { get; set; }
    }
}

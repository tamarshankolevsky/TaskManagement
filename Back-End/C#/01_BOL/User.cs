using _01_BOL.validations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;

namespace _01_BOL
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Required field")]
        [RegularExpression(@"[A-Za-z]+", ErrorMessage = "User name can contain only letters")]
        [StringValidator(MaxLength = 15, MinLength = 2)]
        public string Name { get; set; }

        [Required]
        [MinLength(2), MaxLength(10)]
        [UniqeUserName]
        public string UserName { get; set; }

        [MinLength(64), MaxLength(64), Required]
        [StringValidator(InvalidCharacters = "\"'!@#$%^&*()[]{}-_+=:?.,;/\\", MaxLength = 64, MinLength = 64)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
       
        [EmailAddress]
        [DataType(DataType.EmailAddress), Required]
        public string EMail { get; set; }

        [ForeignKey("Status")]
        public int StatusId { get; set; }

        [ForeignKey("Manager")]
        public int? ManagerId { get; set; }

        public string ProfileImageName { get; set; }

        [DefaultValue(true)]
        public bool IsActive { get; set; } 

    }
}

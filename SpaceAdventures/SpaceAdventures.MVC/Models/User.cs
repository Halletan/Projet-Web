using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SpaceAdventures.MVC.Models
{
    [Serializable]
    public class User
    {
        public int IdUser { get; set; }  

        [Required]
        [MaxLength(20)]
        [MinLength(5)]
        public string Username { get; set; }

        [Required]
        [MaxLength(15)]
        public string Lastname { get; set; } 

        [Required]
        [MaxLength(15)]
        public string Firstname { get; set; } 

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public bool VerifiedEmail { get; set; }   

        public string Connection { get; set; }

        //public string IdUserAuth0 { get; set; }     // To keep ?

        [DisplayName("Role")]                   
        public int IdRole { get; set; }         

        public string? RoleName { get; set; }         

        [PasswordPropertyText]
        [Required]
        [MinLength(6)]
        public string Password { get; set; }  
    }

    [Serializable]
    public class Users
    {
        public List<User> UsersList { get; set; }
    }
}
    
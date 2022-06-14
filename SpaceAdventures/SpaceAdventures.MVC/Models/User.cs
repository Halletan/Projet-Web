using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SpaceAdventures.MVC.Models
{
    [Serializable]
    public class User
    {
        public int IdUser { get; set; }  // in test, added

        [Required]
        [MaxLength(20)]
        [MinLength(5)]
        public string Username { get; set; }

        [Required]
        [MaxLength(15)]
        public string Lastname { get; set; } // Test, added

        [Required]
        [MaxLength(15)]
        public string Firstname { get; set; } // Test, added

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public bool VerifiedEmail { get; set; }   // in test, added

        public string Connection { get; set; }

        //public string IdUserAuth0 { get; set; }     // To keep ?

        [DisplayName("Role")]                   // added, in test
        public int IdRole { get; set; }         // changed from idRole, in test

        public string? RoleName { get; set; }   // added, in test       

        [PasswordPropertyText]
        [Required]
        [MinLength(6)]
        public string Password { get; set; }  // Test, added
    }

    [Serializable]
    public class Users
    {
        public List<User> UsersList { get; set; }
    }

}
    // To Delete when evth is ok

//    [Serializable]
//    public class UserInput
//    {
//        [Required]
//        [MaxLength(20)]
//        [MinLength(5)]
//        public string Username { get; set; }
//        [Required]
//        [MaxLength(15)]
//        public string Lastname { get; set; }
//        [Required]
//        [MaxLength(15)]
//        public string Firstname { get; set; }
//        [Required]
//        [EmailAddress]
//        public string Email { get; set; }
//        public string Connection { get; set; }

//        [PasswordPropertyText]
//        [Required]
//        [MinLength(6)]
//        public string Password { get; set; }
//        public int IdRole { get; set; }
//    }

//    [Serializable]
//    public class UserDto
//    {
//        public int IdUser { get; set; }
//        public string Username { get; set; }
//        public string Email { get; set; }
//        public bool VerifiedEmail { get; set; }
//        [DisplayName("Role")]
//        public int IdRole { get; set; }
//        public string? RoleName { get; set; }
//    }
//}

//public class UserVm
//{
//    public string Username { get; set; }
//    public string Email { get; set; }
//    public string Role { get; set; }

//}
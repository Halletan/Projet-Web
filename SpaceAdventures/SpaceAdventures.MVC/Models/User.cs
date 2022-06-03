using System.ComponentModel;

namespace SpaceAdventures.MVC.Models
{
    [Serializable]
    public class User
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Connection { get; set; }
        public int idRole { get; set; }
        public string IdUserAuth0 { get; set; }

        // public bool VerifiedEmail { get; set; }
        //public int IdRole { get; set; }

    }

    public class UserVm
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        // public bool VerifiedEmail { get; set; }
        //public int IdRole { get; set; }
        //public string IdUserAuth0 { get; set; }
    }


    [Serializable]
    public class Users
    {
        public List<User> UsersList { get; set; }
    }

    [Serializable]
    public class UserInput
    {
        public string Username { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Email { get; set; }
        public string Connection { get; set; }
        public string Password { get; set; }
        public int IdRole { get; set; }
    }

    [Serializable]
    public class UserDto
    {
        public int IdUser { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool VerifiedEmail { get; set; }
        [DisplayName("Role")]
        public int IdRole { get; set; }
        public string? RoleName { get; set; }
    }
}
namespace SpaceAdventures.MVC.Models
{
    [Serializable]
    public class User
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Connection { get; set; }
        // public bool VerifiedEmail { get; set; }
        //public int IdRole { get; set; }
        //public string IdUserAuth0 { get; set; }
    }




    [Serializable]
    public class Users
    {
        public List<User> UsersList { get; set; }
    }
}

namespace DriveTogether.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public string Telephone { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string RepeatedPassword { get; set; }
    }
}
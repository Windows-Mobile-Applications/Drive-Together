namespace DriveTogether.Models
{
    using SQLite.Net.Attributes;

    public class UserModel
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int PhoneNumber { get; set; }

        public string Password { get; set; }
    }
}

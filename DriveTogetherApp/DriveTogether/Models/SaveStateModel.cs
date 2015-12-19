namespace DriveTogether.Models
{
    using SQLite.Net.Attributes;

    public class SaveStateModel
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public bool IsSignedIn { get; set; }
    }
}

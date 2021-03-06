﻿namespace DriveTogether.Models
{
    using SQLite.Net.Attributes;

    public class SaveStateModel
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public bool IsSignedIn { get; set; }

        public string Email { get; set; }
        
        public string Password { get; set; }
    }
}

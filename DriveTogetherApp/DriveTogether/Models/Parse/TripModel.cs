namespace DriveTogether.Models.Parse
{
    using System;
    using System.Collections.Generic;

    using global::Parse;

    [ParseClassName("Trips")]
    public class TripModel : ParseObject
    {
        [ParseFieldName("driver")]
        public ParseUser Driver
        {
            get { return GetProperty<ParseUser>(); }
            set { SetProperty<ParseUser>(value); }
        }

        [ParseFieldName("from")]
        public string From
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("to")]
        public string To
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("timeOfLeavingStart")]
        public DateTime TimeOfLeavingStart
        {
            get { return GetProperty<DateTime>(); }
            set { SetProperty<DateTime>(value); }
        }

        [ParseFieldName("timeOfLeavingEnd")]
        public DateTime TimeOfLeavingEnd
        {
            get { return GetProperty<DateTime>(); }
            set { SetProperty<DateTime>(value); }
        }

        [ParseFieldName("price")]
        public int Price
        {
            get { return GetProperty<int>(); }
            set { SetProperty<int>(value); }
        }

        [ParseFieldName("info")]
        public string Info
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("freeSeats")]
        public int FreeSeats
        {
            get { return GetProperty<int>(); }
            set { SetProperty<int>(value); }
        }

        [ParseFieldName("dateOfLeaving")]
        public DateTime DateOfLeaving
        {
            get { return GetProperty<DateTime>(); }
            set { SetProperty<DateTime>(value); }
        }

        [ParseFieldName("passengers")]
        public IEnumerable<ParseUser> Passengers
        {
            get { return GetProperty<IEnumerable<ParseUser>>(); }
            set { SetProperty<IEnumerable<ParseUser>>(value); }
        }
    }
}

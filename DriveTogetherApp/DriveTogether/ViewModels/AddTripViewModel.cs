using DriveTogether.Common;
using DriveTogether.Models.Parse;
using Parse;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DriveTogether.ViewModels
{
    public class AddTripViewModel : BaseViewModel
    {
        public string ServerErrorMessage { get; set; }

        public ParseUser Driver { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public DateTime TimeOfLeavingStart { get; set; }

        public DateTime TimeOfLeavingEnd { get; set; }

        public string Price { get; set; }

        public string Info { get; set; }

        public string FreeSeats { get; set; }

        public DateTime DateOfLeaving { get; set; }

        public IEnumerable<ParseUser> Passengers { get; set; }


        public bool ValidateInput()
        {
            //if (!InputValidator.ValidateRegularTextInput(this.From))
            //{
            //    return false;
            //}
            //
            //if (!InputValidator.ValidateRegularTextInput(this.To))
            //{
            //    return false;
            //}
            //
            //int seats;
            //bool isDigit = int.TryParse(this.FreeSeats, out seats);
            //
            //int price;
            //bool isValidPrice = int.TryParse(this.Price, out price);
            //
            //if (!isDigit)
            //{
            //    return false;
            //}

            //if (!isValidPrice)
            //{
            //    return false;
            //}

            return true;
        }

        public async Task<bool> AddNewTripData()
        {
            try
            {
                var trip = new TripModel()
                {
                    Driver = ParseUser.CurrentUser,
                    From = this.From,
                    To = this.To,
                    TimeOfLeavingStart = this.TimeOfLeavingStart,
                    TimeOfLeavingEnd = this.TimeOfLeavingEnd,
                    Price = int.Parse(this.Price),
                    Info = this.Info,
                    FreeSeats = int.Parse(this.FreeSeats),
                    DateOfLeaving = this.DateOfLeaving,
                    Passengers = new List<ParseUser>()
                    {
                        ParseUser.CurrentUser
                    }
                };

                await trip.SaveAsync();

                return true;
            }
            catch (Exception ex)
            {
                this.ServerErrorMessage = ex.Message;
                return false;
            }
        }
    }
}
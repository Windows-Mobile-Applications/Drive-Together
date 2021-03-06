﻿namespace DriveTogether.ViewModels
{
    using System;
    using System.Threading.Tasks;

    using DriveTogether.Common;
    using Parse;
    using System.Net.NetworkInformation;
    using Windows.UI.Popups;
    using Windows.Networking.Connectivity;

    public class SignUpViewModel : BaseViewModel
    {
        public UserViewModel User { get; set; }

        public string ServerErrorMessage { get; set; }

        public bool IsActive { get; set; }

        public SignUpViewModel()
        {
            this.User = new UserViewModel();
        }

        /// <summary>
        /// Validate sign up form inputs
        /// </summary>
        /// <returns></returns>
        public bool ValidateInput()
        {
            if (!InputValidator.ValidateRegularTextInput(this.User.FirstName))
            {
                return false;
            }

            if (!InputValidator.ValidateRegularTextInput(this.User.LastName))
            {
                return false;
            }
            
            if (!InputValidator.ValidatePhoneTextInput(this.User.Telephone))
            {
                return false;
            }

            if (!InputValidator.ValidateEmailTextInput(this.User.Email))
            {
                return false;
            }

            if (!InputValidator.ValidatePasswordTextInput(this.User.Password))
            {
                return false;
            }

            if (!InputValidator.ValidateRepeatedPasswordTextInput(this.User.Password, this.User.RepeatedPassword))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Sign up user async using Parse
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SignUp()
        {
            var parseUser = new ParseUser()
            {
                Username = this.User.Email,
                Email = this.User.Email,
                Password = this.User.Password
            };

            parseUser["firstName"] = this.User.FirstName;
            parseUser["lastName"] = this.User.LastName;
            parseUser["telephone"] = this.User.Telephone;
            
            if (await this.IsConnectedToInternet())
            {
                try
                {
                    await parseUser.SignUpAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    this.ServerErrorMessage = ex.Message;
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}

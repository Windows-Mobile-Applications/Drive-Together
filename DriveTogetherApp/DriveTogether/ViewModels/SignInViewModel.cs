namespace DriveTogether.ViewModels
{
    using System;
    using System.Threading.Tasks;

    using Common;
    using Parse;


    public class SignInViewModel : BaseViewModel
    {
        public UserViewModel User { get; set; }

        public string ServerErrorMessage { get; set; }

        public bool IsActive { get; set; }

        public SignInViewModel()
        {
            this.User = new UserViewModel();
        }

        public bool ValidateInput()
        {
            if (!InputValidator.ValidateRegularTextInput(this.User.Email))
            {
                return false;
            }

            if (!InputValidator.ValidatePasswordTextInput(this.User.Password))
            {
                return false;
            }

            return true;
        }

        public async Task<bool> SignInAsync()
        {
            if (await this.IsConnectedToInternet())
            {
                try
                {
                    await ParseUser.LogInAsync(this.User.Email, this.User.Password);
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

namespace DriveTogether.ViewModels
{
    using System.Windows.Input;

    using Windows.UI.Xaml.Controls;
    using Commands;
    using Pages;

    public class StartUpViewModel
    {
        private Frame frame;
        private ICommand signInCommand;
        private ICommand signUpCommand;

        public StartUpViewModel(Frame frame)
        {
            this.frame = frame;
        }

        /// <summary>
        /// Defines property for executing Sign In Command
        /// </summary>
        public ICommand SignIn
        {
            get
            {
                if (this.signInCommand == null)
                {
                    this.signInCommand = new RelayCommand(this.SignInExec);
                }

                return this.signInCommand;
            }
        }

        /// <summary>
        /// Defines property for executing Sign Up Command
        /// </summary>
        public ICommand SignUp
        {
            get
            {
                if (this.signUpCommand == null)
                {
                    this.signUpCommand = new RelayCommand(this.SignUpExec);
                }

                return this.signUpCommand;
            }
        }

        private void SignInExec()
        {
            this.frame.Navigate(typeof(SignInPage), this.frame);
        }

        private void SignUpExec()
        {
            this.frame.Navigate(typeof(SignUpPage), this.frame);
        }
    }
}

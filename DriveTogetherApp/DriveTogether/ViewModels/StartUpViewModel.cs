namespace DriveTogether.ViewModels
{
    using System.Windows.Input;

    using Commands;
    using Windows.UI.Xaml.Controls;
    using Pages;
    public class StartUpViewModel
    {
        private Frame frame;
        private ICommand signInCommand;

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

        private void SignInExec()
        {
            this.frame.Navigate(typeof(SignInPage), this.frame);
        }
    }
}

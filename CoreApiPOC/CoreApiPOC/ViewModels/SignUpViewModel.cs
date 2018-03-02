namespace CoreApiPOC.ViewModels
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;
    using CoreApiPOC.Validations;
    using CoreApiPOC.ViewModels.Base;
    using Models;
    using Services;
    using System.Net.Http;

    public class SignUpViewModel : ViewModelBase
    {
        private bool _isValid;
        public bool IsValid
        {
            get
            {
                return _isValid;
            }
            set
            {
                _isValid = value;
                RaisePropertyChanged(() => IsValid);
            }
        }

        private ValidatableObject<string> _firstName;
        public ValidatableObject<string> FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                RaisePropertyChanged(() => FirstName);
            }
        }

        private ValidatableObject<string> _lastName;
        public ValidatableObject<string> LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                RaisePropertyChanged(() => LastName);
            }
        }

        private ValidatableObject<string> _userName;
        public ValidatableObject<string> UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                RaisePropertyChanged(() => UserName);
            }
        }

        private ValidatableObject<string> _email;
        public ValidatableObject<string> Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private ValidatableObject<string> _password;
        public ValidatableObject<string> Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }

        private ValidatableObject<string> _confirmPassword;
        public ValidatableObject<string> ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                _confirmPassword = value;
                RaisePropertyChanged(() => ConfirmPassword);
            }
        }

        private bool _isAdmin;
        public bool IsAdmin
        {
            get { return _isAdmin; }
            set
            {
                _isAdmin = value;
                RaisePropertyChanged(() => IsAdmin);
            }
        }

        private string _IsAdminImg;
        public string IsAdminImg
        {
            get { return _IsAdminImg; }
            set
            {
                _IsAdminImg = value;
                RaisePropertyChanged(() => IsAdminImg);
            }
        }

        public ICommand IsAdminCommand { get; private set; }
        public SignUpViewModel()
        {
            _firstName = new ValidatableObject<string>();
            _lastName = new ValidatableObject<string>();
            _userName = new ValidatableObject<string>();
            _email = new ValidatableObject<string>();
            _password = new ValidatableObject<string>();
            _confirmPassword = new ValidatableObject<string>();

            IsAdminCommand = new Command(IsAdminAsync);
            IsAdminImg = "uncheck.png";
            AddValidations();
        }

        public ICommand MockSignUpCommand => new Command(async () => await SignUpAsync());

        public ICommand ValidateFirstNameCommand => new Command(() => ValidateFirstName());
        public ICommand ValidateLastNameCommand => new Command(() => ValidateLastName());
        public ICommand ValidateUserNameCommand => new Command(() => ValidateUserName());
        public ICommand ValidateEmailCommand => new Command(() => ValidateEmail());
        public ICommand ValidatePasswordCommand => new Command(() => ValidatePassword());
        public ICommand EmailUnfocusedCommand => new Command(() => EmailUnfocused());
        public ICommand ConfirmPasswordUnfocusedCommand => new Command(() => ConfirmPasswordUnfocused());

        private bool ConfirmPasswordUnfocused()
        {
            _confirmPassword.Validations.Add(new IsCompareRule<string> { Text = Password.Value, ValidationMessage = "Password doesn't match." });
            return _confirmPassword.Validate();
        }

        private bool EmailUnfocused()
        {
            _email.Validations.Add(new IsEmailRule<string> { ValidationMessage = "Email in invalid format." });
            return _email.Validate();
        }

        private async Task SignUpAsync()
        {
            IsBusy = true;
            IsValid = true;
            bool isValid = Validate();

            if (isValid)
            {
                try
                {
                    UserDto obj = new UserDto { Firstname = FirstName.Value, Lastname = LastName.Value, Username = UserName.Value, Email = Email.Value, Password = Password.Value, IsAdmin = false, IsApproved = false };

                    await ServiceHandler.PostDataAsync<string, UserDto>(obj).ContinueWith((t) =>
                    {
                        if (t.IsFaulted)
                        {
                            Application.Current.MainPage.DisplayAlert("Error!!!", "Something Went Wrong", "OK");
                        }
                        else
                        {
                            if (t.Result == "OK")
                            {
                                Application.Current.MainPage.DisplayAlert("Success", "Thanks for signing up!", "OK");
                                NavigationService.NavigateToAsync<LoginViewModel>();
                            }
                            else
                            {
                                Application.Current.MainPage.DisplayAlert("Error!!!", "Something Went Wrong", "OK");
                            }
                        }
                    }, TaskScheduler.FromCurrentSynchronizationContext());
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[Sign Up] Error signing up: {ex}");
                }
            }
            else
            {
                IsValid = false;
            }
            
            IsBusy = false;
        }

        private void IsAdminAsync(object param)
        {
            bool val = (bool)param;
            IsAdminImg = val ? "uncheck.png" : "check.png";
            IsAdmin = !val;
        }

        private bool ValidateFirstName()
        {
            return _firstName.Validate();
        }

        private bool ValidateLastName()
        {
            return _lastName.Validate();
        }

        private bool ValidateUserName()
        {
            return _userName.Validate();
        }

        private bool ValidateEmail()
        {
            return _email.Validate();
        }

        private bool ValidatePassword()
        {
            return _password.Validate();
        }

        private bool ValidateConfirmPassword()
        {
            return _confirmPassword.Validate();
        }

        private bool Validate()
        {
            bool isValidFirstName = ValidateFirstName();
            bool isValidLastName = ValidateLastName();
            bool isValidUser = ValidateUserName();
            bool isValidEmail = ValidateEmail();
            bool isValidPassword = ValidatePassword();
            bool isValidConfirmPassword = ValidateConfirmPassword();
            return isValidFirstName && isValidLastName && isValidUser && isValidPassword && isValidEmail && isValidConfirmPassword;
        }

        private void AddValidations()
        {
            _firstName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please enter first name" });
            _lastName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please enter last name" });
            _userName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please enter username" });
            _email.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please enter email" });
            _email.Validations.Add(new IsEmailRule<string> { ValidationMessage = "Please enter valid email" });
            _password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please enter password" });
            _confirmPassword.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Passwords don't match" });

        }
    }
}

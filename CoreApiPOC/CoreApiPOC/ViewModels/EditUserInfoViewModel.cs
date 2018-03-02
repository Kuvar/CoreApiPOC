using CoreApiPOC.Models;
using CoreApiPOC.Services;
using CoreApiPOC.Validations;
using CoreApiPOC.ViewModels.Base;
using CoreApiPOC.Views;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CoreApiPOC.ViewModels
{
    public class EditUserInfoViewModel : ViewModelBase
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

        public EditUserInfoViewModel()
        {
            _firstName = new ValidatableObject<string>();
            _lastName = new ValidatableObject<string>();
            _userName = new ValidatableObject<string>();
            _email = new ValidatableObject<string>();
            _password = new ValidatableObject<string>();
            _confirmPassword = new ValidatableObject<string>();
            //GetUserInfo();
            AddValidations();
        }

        private async Task GetUserInfo()
        {
            try
            {
                await ServiceHandler.GetDataAsync<UserDto>(App.Id.ToString()).ContinueWith((t) =>
                {
                    if (t.IsFaulted)
                    {
                        Application.Current.MainPage.DisplayAlert("Error!!!", "Something Went Wrong", "OK");
                    }
                    else
                    {
                        if (t.Result != null)
                        {
                            var data = t.Result;
                            FirstName.Value = data.Firstname;
                            LastName.Value = data.Lastname;
                            UserName.Value = data.Username;
                            Email.Value = data.Email;
                        }
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[EditUserInfoViewModel] Error GetUserInfo: {ex}");
            }
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
            return isValidFirstName && isValidLastName && isValidUser && isValidEmail;
        }
        private void ValidateChangePassword()
        {
            bool isValidPassword = ValidatePassword();
            bool isValidConfirmPassword = ValidateConfirmPassword();
        }

        public ICommand UpdateUserInfoCommand => new Command(async () => await UpdateUserInfoAsync());
        public ICommand ChangePasswordCommand => new Command(() => ChangePasswordPage());

        private void ChangePasswordPage()
        {
            try
            {
                var page = (Page)Activator.CreateInstance(typeof(LandingView));
                MessagingCenter.Send<Page,string>(page, "MessagingCenterNavigateToPage", "Landing View");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[EditUserInfoViewModel] Error ChangePasswordPage: {ex}");
            }
        }

        private async Task UpdateUserInfoAsync()
        {
            IsBusy = true;
            IsValid = true;
            bool isValid = Validate();

            if (isValid)
            {
                try
                {
                    UserDto obj = new UserDto { Firstname = FirstName.Value, Lastname = LastName.Value, Username = UserName.Value, Email = Email.Value};

                    await ServiceHandler.PutDataAsync<string, UserDto>(App.Id, obj).ContinueWith((t) =>
                    {
                        if (t.IsFaulted)
                        {
                            Application.Current.MainPage.DisplayAlert("Error!!!", "Something Went Wrong", "OK");
                        }
                        else
                        {
                            var data = t.Result;
                        }
                    }, TaskScheduler.FromCurrentSynchronizationContext());
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[EditUserInfoViewModel] Error UpdateUserInfo: {ex}");
                }
            }
        }
    }
}

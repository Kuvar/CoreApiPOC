namespace CoreApiPOC.ViewModels
{
    using CoreApiPOC.Validations;
    using CoreApiPOC.ViewModels.Base;
    using Models;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class LoginViewModel : ViewModelBase
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

        public LoginViewModel()
        {
            IsBusy = true;
            _userName = new ValidatableObject<string>();
            _password = new ValidatableObject<string>();

            AddValidations();
        }

        public ICommand GoToSignupCommand => new Command(async () => await SignUpAsync());

        public ICommand MockSignInCommand => new Command(async () => await MockSignInAsync1());

        public ICommand ValidateUserNameCommand => new Command(() => ValidateUserName());

        public ICommand ValidatePasswordCommand => new Command(() => ValidatePassword());

        private async Task MockSignInAsync()
        {
            IsBusy = true;
            IsValid = true;
            bool isValid = Validate();

            if (isValid)
            {
                try
                {
                    UserDto obj = new UserDto { Username = UserName.Value, Password = Password.Value };
                    await ServiceHandler.AuthAsync<AuthResponse, UserDto>(obj).ContinueWith((t) =>
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

                                App.Id = data.id;
                                App.Name = data.username;
                                App.Email = data.email;
                                App.IsAdmin = data.isAdmin;
                                App.Token = data.token;
                                NavigationService.NavigateToAsync<MainViewModel>();
                                //NavigationService.RemoveLastFromBackStackAsync();
                            }
                            else
                            {
                                Application.Current.MainPage.DisplayAlert("Fail!!!", "Invalid username or password", "OK");
                            }
                        }
                    }, TaskScheduler.FromCurrentSynchronizationContext());

                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[SignIn] Error signing in: {ex}");
                }
            }
            else
            {
                IsValid = false;
            }

            IsBusy = false;
        }

        private async Task SignUpAsync()
        {
            IsBusy = true;

            await NavigationService.NavigateToAsync<SignUpViewModel>();

            IsBusy = false;
        }

        private async Task GetAllUsers()
        {
            await ServiceHandler.GetDataAsync<List<UserDto>>(string.Empty).ContinueWith((t) =>
            {
                if (t.IsFaulted)
                {
                    // Application.Current.MainPage.DisplayAlert("", t.Exception.Message, "OK");
                }
                else
                {
                    var data = t.Result;
                }
            });
        }

        private async Task MockSignInAsync1()
        {
            IsBusy = true;
            IsValid = true;
            bool isValid = Validate();
            //bool isAuthenticated = false;

            await NavigationService.NavigateToAsync<MainViewModel>();
            //await NavigationService.RemoveLastFromBackStackAsync();

            //await App.Navigation.PushModalAsync(new MainView());

            IsBusy = false;
        }

        private void AddValidations()
        {
            _userName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A username is required." });
            _password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A password is required." });
        }

        private bool Validate()
        {
            bool isValidUser = ValidateUserName();
            bool isValidPassword = ValidatePassword();

            return isValidUser && isValidPassword;
        }

        private bool ValidateUserName()
        {
            return _userName.Validate();
        }

        private bool ValidatePassword()
        {
            return _password.Validate();
        }


    }
}

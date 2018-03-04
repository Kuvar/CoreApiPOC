using CoreApiPOC.Validations;
using CoreApiPOC.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CoreApiPOC.ViewModels
{
    partial class ChangePasswordViewModel : ViewModelBase
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

        public ChangePasswordViewModel()
        {
            _password = new ValidatableObject<string>();
            _confirmPassword = new ValidatableObject<string>();

            AddValidations();
        }

        private void AddValidations()
        {
            _password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please enter password" });
            _confirmPassword.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please enter Confirm password" });
        }
        private bool ValidatePassword()
        {
            return _password.Validate();
        }
        private bool ConfirmPasswordUnfocused()
        {
            _confirmPassword.Validations.Add(new IsCompareRule<string> { Text = Password.Value, ValidationMessage = "Password doesn't match" });
            return _confirmPassword.Validate();
        }
        private bool ValidateConfirmPassword()
        {
            return _confirmPassword.Validate();
        }
        private bool Validate()
        {
            bool isValidPassword = ValidatePassword();
            bool isValidConfirmPassword = ValidateConfirmPassword();
            bool isConfirmPasswordMatch = ConfirmPasswordUnfocused();

            return isValidPassword && isValidConfirmPassword && isConfirmPasswordMatch;
        }

        public ICommand ChangePasswordCommandAsync => new Command(async () => await ChangePasswordAsync());

        private async Task ChangePasswordAsync()
        {
            IsBusy = true;
            IsValid = true;
            bool isValid = Validate();
            if (isValid)
            {
                await Application.Current.MainPage.DisplayAlert("Success", "Your password has been changed successfully!", "OK");
            }
            else
            {
                IsValid = false;
            }

            IsBusy = false;
        }
    }
}

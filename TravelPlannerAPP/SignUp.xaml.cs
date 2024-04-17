using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlannerAPP.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelPlannerAPP
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SignUp : ContentPage
	{
		public SignUp ()
		{
			InitializeComponent ();
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string username = usernameEntry.Text;
            string email = emailEntry.Text;
            string password = passwordEntry.Text;
            string confirmPassword = confirmPasswordEntry.Text;
            string validationMessage = ValidateSignUpInput(username, email, password, confirmPassword);
            if (!string.IsNullOrEmpty(validationMessage))
            {
                await DisplayAlert("Error", validationMessage, "OK");
                return;
            }

            User newUser = new User() 
            {
                UserName = username,
                Email = email,
                Password = password,
            };

            if (password == confirmPassword)
            {
                await App.DB.SaveUserAsync(newUser);
                await Navigation.PushAsync(new SignIn());
            }
            else
            {
               await DisplayAlert("Error", "Passwords are not matching!", "OK");
            }


        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignIn());
        }

        private string ValidateSignUpInput(string username, string email, string password, string confirmPassword)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return "Please enter a username.";
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                return "Please enter an email address.";
            }

            if (!IsValidEmail(email))
            {
                return "Please enter a valid email address.";
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                return "Please enter a password.";
            }

            if (password != confirmPassword)
            {
                return "Passwords do not match.";
            }

            
            return null;
        }
        private bool IsValidEmail(string email)
        {
            return email.Contains("@");
        }
    }
}
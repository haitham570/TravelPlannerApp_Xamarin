using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlannerAPP.Model;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelPlannerAPP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTrip : ContentPage
    {
        TripDatabase tripDatabase;
        Trip newTrip;
        public AddTrip()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(destinationEntry.Text))
            {
                await DisplayAlert("Error", "Destination cannot be empty", "OK");
                return;
            }

            if (startDatePicker.Date > endDatePicker.Date)
            {
                await DisplayAlert("Error", "End date cannot be earlier than start date", "Ok");
                return;
            }

            if (string.IsNullOrWhiteSpace(ImageUrlEntry.Text))
            {
                await DisplayAlert("Error", "Please enter an image URL", "OK");
                return;
            }

            try
            {
                Trip newTrip = new Trip
                {
                    Destination = destinationEntry.Text,
                    StartDate = startDatePicker.Date,
                    EndDate = endDatePicker.Date,
                    TripCost = float.Parse(tripCostEntry.Text),
                    Program = programEntry.Text,
                    ImageUrl = ImageUrlEntry.Text,
                    UserId = App.CurrentUserId
                    
                };

                int rowAffected = await App.tripDatabase.SaveTripAsync(newTrip);

                if (rowAffected > 0)
                {
                    await DisplayAlert("Success", "Trip added successfully", "OK");

                    
                    await Navigation.PushAsync(new UpcomingTrips());
                }
                else
                {
                    await DisplayAlert("Error", "Failed to add trip", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }



        }

        private async void uploadImage_Clicked(object sender, EventArgs e)
        {
            string imageUrl = ImageUrlEntry.Text;

            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                await DisplayAlert("Error", "Image URL cannot be empty", "OK");
                return;
            }

            try
            {
                Uri imageUri = new Uri(imageUrl);

                resutlImage.Source = ImageSource.FromUri(imageUri);
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "Invalid image URL", "OK");
            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
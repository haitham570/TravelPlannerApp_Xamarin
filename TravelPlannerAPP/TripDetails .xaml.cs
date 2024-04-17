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
    public partial class TripDetails : ContentPage
    {
        private Trip trip;
        public TripDetails(Trip trip)
        {
            InitializeComponent();
            this.trip = trip;
            BindingContext = trip;
        }

        
        private void Button_Clicked(object sender, EventArgs e)
        {

        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {

        }

        private async void DeleteTripButton_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Delete Trip", "Are you sure you want to delete this trip?",
                "Yes", "No");
            if (answer)
            {
                try
                {
                    await App.tripDatabase.DeleteTripAsync(trip);
                    await DisplayAlert("Success", "Trip deleted successfully", "Ok");
                    await Navigation.PushAsync(new UpcomingTrips());
                }catch (Exception ex)
                {
                    await DisplayAlert("Error", $"Failed to delete trip: {ex.Message}", "OK");
                }
            }

        }

        private async void EditTripButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditTrip(trip));
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UpcomingTrips());
        }

        private async void ShowMapButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync (new MapPage(trip));
        }
    }
}
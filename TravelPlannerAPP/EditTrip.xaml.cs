using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TravelPlannerAPP.Model;

namespace TravelPlannerAPP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditTrip : ContentPage
    {
        private Trip trip;
        public EditTrip(Trip trip)
        {
            InitializeComponent();
            this.trip = trip;
            BindingContext = trip;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }

        private async void UpdateTripButton_Clicked(object sender, EventArgs e)
        {
            trip.Destination = destinationEntry.Text;
            trip.StartDate = startDatePicker.Date;
            trip.EndDate = endDatePicker.Date;
            trip.TripCost = (float)Convert.ToDecimal(tripCostEntry.Text);
            trip.Program = programEntry.Text;
            trip.ImageUrl = ImageUrlEntry.Text;

            await App.tripDatabase.SaveTripAsync(trip);

            await Navigation.PushAsync(new UpcomingTrips());
        }

        private void UpdateImage_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ImageUrlEntry.Text))
            {
                resutlImage.Source = ImageUrlEntry.Text;
            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
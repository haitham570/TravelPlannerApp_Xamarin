using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TravelPlannerAPP.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelPlannerAPP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UpcomingTrips : ContentPage
	{
        private List<Trip> allTrips;
        
		public UpcomingTrips ()
		{
			InitializeComponent ();
           LoadTrips ();
			
		}
        private async void LoadTrips()
        {
            try
            {
                
                allTrips = await App.tripDatabase.GetUpcomingTripAsync(App.CurrentUserId);

                
                tripsCollectionView.ItemsSource = allTrips;

                
            }
            catch (Exception ex)
            {
                
                await DisplayAlert("Error", $"Error loading trips: {ex.Message}", "OK");
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

			await Navigation.PushAsync(new AddTrip());
        }

        private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchTerm = e.NewTextValue;
            if(string.IsNullOrWhiteSpace(searchTerm) )
            {
                tripsCollectionView.ItemsSource = allTrips;
            }
            else
            {
                List<Trip> filteredTrips = allTrips.Where(trip => trip.Destination.ToLower().Contains(searchTerm.ToLower())).ToList();
                tripsCollectionView.ItemsSource = filteredTrips;
            }
        }

        

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (sender is Image tappedImage)
            {
                // Get the associated trip object from the binding context
                if (tappedImage.BindingContext is Trip selectedTrip)
                {
                    try
                    {
                        // Navigate to the TripDetails page with the selected trip
                        await Navigation.PushAsync(new TripDetails(selectedTrip));
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Error", $"Error navigating to TripDetails: {ex.Message}", "OK");
                    }
                }
            }
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignIn());
        }
    }
}
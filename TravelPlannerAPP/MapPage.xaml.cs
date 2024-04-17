using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlannerAPP.Model;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace TravelPlannerAPP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        Trip trip;
        public MapPage(Trip trip)
        {
            InitializeComponent();
            this.trip = trip;
            BindingContext = trip;

            MoveToDestination();
        }

        private async void MoveToDestination()
        {
            if (!string.IsNullOrEmpty(trip.Destination))
            {
                await SearchAndMoveToLocation(trip.Destination);
            }
        }

        private async void targetsearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            string searchQuery = targetsearchBar.Text;
            await SearchAndMoveToLocation(searchQuery);

        }
        private async Task SearchAndMoveToLocation(string searchQuery)
        {
            var locations = await Geocoding.GetLocationsAsync(searchQuery);
            if (locations != null && locations.Any())
            {
                var location = locations.First();
                map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position
                    (location.Latitude, location.Longitude), Distance.FromMiles(1)));

                var pin = new Pin
                {
                    Position = new Position (location.Latitude, location.Longitude),
                    Label = "Destination",
                    Address = searchQuery
                };
                map.Pins.Add(pin);
            }
            else
            {
                await DisplayAlert("Error", "No locations found for the search query.", "OK");
            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async Task ShowDirections()
        {
            if(!string.IsNullOrEmpty(trip.Destination))
            {
                var locations = await Geocoding.GetLocationsAsync(trip.Destination);
                if (locations != null && locations.Any())
                {
                    var location = locations.First();

                    await Xamarin.Essentials.Map.OpenAsync(location.Latitude, location.Longitude);
                }
                else
                {
                    await DisplayAlert("Error", "No locations found for the destination", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "Destination is not set", "Ok");
            }
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            await ShowDirections();
        }
    }
}
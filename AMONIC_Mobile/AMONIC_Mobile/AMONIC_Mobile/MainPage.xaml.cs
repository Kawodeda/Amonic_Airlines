using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AMONIC_Mobile
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private async void search_btn_Clicked(object sender, EventArgs e)
        {
            activity_indicator.IsRunning = true;
            await Navigation.PushAsync(new SearchFlightsPage());
            activity_indicator.IsRunning = false;
        }

        private async void reserve_btn_Clicked(object sender, EventArgs e)
        {
            activity_indicator.IsRunning = true;
            await Navigation.PushAsync(new ReservePage());
            activity_indicator.IsRunning = false;
        }

        private async void amenities_btn_Clicked(object sender, EventArgs e)
        {
            activity_indicator.IsRunning = true;
            await Navigation.PushAsync(new AmenitiesPage());
            activity_indicator.IsRunning = false;
        }

        private async void about_btn_Clicked(object sender, EventArgs e)
        {
            activity_indicator.IsRunning = true;
            await Navigation.PushAsync(new AboutPage());
            activity_indicator.IsRunning = false;
        }
    }
}

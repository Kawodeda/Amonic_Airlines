using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

using Newtonsoft.Json;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AMONIC_Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchFlightsPage : ContentPage
    {
        private HttpClient client;
        private List<Airport> airports;
        public SearchFlightsPage()
        {
            InitializeComponent();

            client = new HttpClient();
        }

        private async void back_btn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }

        private async void search_btn_Clicked(object sender, EventArgs e)
        {
            if(from_picker.SelectedItem is Airport from && to_picker.SelectedItem is Airport to)
            {
                string url = $"http://192.168.13.183:4118/user18/api/schedules?from={from.Name}&to={to.Name}&date={date_picker.Date}";

                ShowActivityIndicator();
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();

                var flights = JsonConvert.DeserializeObject<List<Flight>>(result);
                flights.ForEach(x => x.Date = date_picker.Date);
                flights_view.ItemsSource = flights;

                HideActivityIndicator();
            }
        }

        private async void ContentPage_Appearing(object sender, EventArgs e)
        {
            string url = "http://192.168.13.183:4118/user18/api/airports";

            ShowActivityIndicator();
            var response = await client.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            

            airports = JsonConvert.DeserializeObject<List<Airport>>(result);
            UpdateFromPicker();
            HideActivityIndicator();
        }

        private void from_picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateToPicker();
        }

        private void to_picker_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void UpdateFromPicker()
        {
            from_picker.ItemsSource = airports;
            from_picker.SelectedIndex = 0;
        }

        private void UpdateToPicker()
        {
            if (from_picker.SelectedItem is Airport airport)
            {
                to_picker.ItemsSource = airports.Except(new List<Airport>() { airport }).ToList();
                to_picker.SelectedIndex = 0;
            }
        }

        private void ShowActivityIndicator()
        {
            ai_layout.IsVisible = true;
            activity_indicator.IsRunning = true;
            main_layout.Children.ToList().ForEach(x => x.IsEnabled = false);
        }

        private void HideActivityIndicator()
        {
            ai_layout.IsVisible = false;
            activity_indicator.IsRunning = false;
            main_layout.Children.ToList().ForEach(x => x.IsEnabled = true);
        }
    }
}
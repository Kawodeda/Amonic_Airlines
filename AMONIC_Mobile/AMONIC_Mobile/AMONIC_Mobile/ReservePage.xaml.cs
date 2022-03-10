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
    public partial class ReservePage : ContentPage
    {
        private const string businessImagePath = "AMONIC_Mobile.Images.BusinessClassSeats.png";
        private const string firstClassPath = "AMONIC_Mobile.Images.FirstClassSeats.png";

        private HttpClient client;
        private CabinType selectedCabinType;
        private SeatBooking seatBooking;
        private int ticketId;
        public ReservePage()
        {
            InitializeComponent();

            client = new HttpClient();
        }

        private async void back_btn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }

        private async void reserve_btn_Clicked(object sender, EventArgs e)
        {
            activity_indicator.IsRunning = true;

            if(selectedCabinType != null)
            {
                await Navigation.PushModalAsync(new SeatBookingPage(selectedCabinType, seatBooking, ticketId, this));
            }
            else
            {
                await DisplayAlert("", "", "OK");
            }

            activity_indicator.IsRunning = false;
        }

        private async void next_btn_Clicked(object sender, EventArgs e)
        {
            await UpdateTicketInfo();
        }

        public async Task UpdateTicketInfo()
        {
            if (int.TryParse(ticket_number_entry.Text, out ticketId))
            {
                string url = $"http://192.168.13.183:4118/user18/api/tickets?ticketId={ticketId}";

                activity_indicator.IsRunning = true;

                {
                    var response = await client.GetAsync(url);
                    var result = await response.Content.ReadAsStringAsync();
                    selectedCabinType = JsonConvert.DeserializeObject<CabinType>(result);
                }

                url = $"http://192.168.13.183:4118/user18/api/seats?ticketId={ticketId}";

                {
                    var response = await client.GetAsync(url);
                    var result = await response.Content.ReadAsStringAsync();
                    seatBooking = JsonConvert.DeserializeObject<SeatBooking>(result);

                    if (seatBooking != null)
                    {
                        current_booking_label.Text = $"Current reserved:\nRow {seatBooking.Row}\nSeat {seatBooking.Seat}";
                    }
                }

                switch (selectedCabinType.Type)
                {
                    case "Economy":
                        cabin_image.Source = null;
                        current_booking_label.Text = "";
                        await DisplayAlert("", "Пассажирам эконом класса недоступно бронироание мест", "OK");
                        break;
                    case "Business":
                        cabin_image.Source = ImageSource.FromResource(businessImagePath);
                        break;
                    case "First Class":
                        cabin_image.Source = ImageSource.FromResource(firstClassPath);
                        break;
                }

                activity_indicator.IsRunning = false;
            }
            else
            {
                DisplayAlert("", "Некорректный ввод", "OK");
                ticket_number_entry.Text = "";
            }
        }
    }
}
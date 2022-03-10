using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Newtonsoft.Json;

namespace AMONIC_Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SeatBookingPage : ContentPage
    {
        private List<SeatRow> businessRows = Enumerable.Range(1, 4).Select(x => new SeatRow() { Name = x.ToString()}).ToList();
        private List<SeatRow> firstClassRows = Enumerable.Range(1, 3).Select(x => new SeatRow() { Name = x.ToString() }).ToList();
        private List<Seat> businessSeats = new List<Seat>()
        {
            new Seat() { Name = "A"},
            new Seat() { Name = "B"},
            new Seat() { Name = "C"},
            new Seat() { Name = "D"}
        };
        private List<Seat> firstClassSeats = new List<Seat>()
        {
            new Seat() { Name = "A"},
            new Seat() { Name = "C"},
            new Seat() { Name = "D"}
        };

        private CabinType cabinType;
        private SeatBooking seatBooking;
        private int ticketId;
        private HttpClient client;
        private ReservePage reservePage;

        public SeatBookingPage(CabinType _cabinType, SeatBooking _seatBooking, int _ticketId, ReservePage _reservePage)
        {
            InitializeComponent();

            cabinType = _cabinType;
            seatBooking = _seatBooking;
            ticketId = _ticketId;
            reservePage = _reservePage;
            client = new HttpClient();
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            switch(cabinType.Type)
            {
                case "Business":
                    row_picker.ItemsSource = businessRows;
                    seat_picker.ItemsSource = businessSeats;
                    cabin_image.Source = ImageSource.FromResource("AMONIC_Mobile.Images.BusinessClassSeats.png");
                    break;
                case "First Class":
                    row_picker.ItemsSource = firstClassRows;
                    seat_picker.ItemsSource = firstClassSeats;
                    cabin_image.Source = ImageSource.FromResource("AMONIC_Mobile.Images.FirstClassSeats.png");
                    break;
            }

            row_picker.SelectedIndex = 0;
            seat_picker.SelectedIndex = 0;
        }

        private async void confirm_btn_Clicked(object sender, EventArgs e)
        {
            if(row_picker.SelectedItem is SeatRow row && seat_picker.SelectedItem is Seat seat)
            {
                string url = $"http://192.168.13.183:4118/user18/api/seats?ticketId={ticketId}&row={row.Name}&seat={seat.Name}";

                var respond = await client.PostAsync(url, null);
                
                if(respond.IsSuccessStatusCode)
                {
                    await DisplayAlert("", "Место забронировано", "OK");
                }
                else
                {
                    await DisplayAlert("Ошибка", "Не удалось отправить данные", "OK");
                }


                await Navigation.PopToRootAsync();
            }
        }

        private async void back_btn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void row_picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (row_picker.SelectedItem is SeatRow row)
            {
                bool match = seatBooking != null ? row.Name == seatBooking.Row : false;

                    switch (cabinType.Type)
                {
                    case "Business":
                        if (seatBooking != null)
                        {
                            seat_picker.ItemsSource = businessSeats.Where(x => match ? x.Name != seatBooking.Seat : true).ToList();
                        }
                        break;
                    case "First Class":
                        if (seatBooking != null)
                        {
                            seat_picker.ItemsSource = firstClassSeats.Where(x => match ? x.Name != seatBooking.Seat : true).ToList();
                        }
                        break;
                }

                seat_picker.SelectedIndex = 0;
            }
        }
    }
}
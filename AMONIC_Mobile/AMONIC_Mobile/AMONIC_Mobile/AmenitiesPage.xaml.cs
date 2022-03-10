using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AMONIC_Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AmenitiesPage : ContentPage
    {
        public AmenitiesPage()
        {
            InitializeComponent();
        }

        private async void back_btn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            activity_indicator.IsRunning = true;

            List<string> lines = Properties.Resources.Amenities.Split('\n').ToList();
            lines.ForEach(x => x.Trim('\r'));

            List<Amenity> amenities = new List<Amenity>();
            foreach (string line in lines)
            {
                try
                {
                    string[] columns = line.Split(';');
                    amenities.Add(new Amenity() { Name = columns[0], Price = decimal.Parse(columns[1]) });
                }
                catch
                {

                }
            }
            amenities_view.ItemsSource = amenities;

            activity_indicator.IsRunning = false;
        }
    }
}
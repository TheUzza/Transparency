using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;
using MySqlConnector;
using Transparency;

namespace Tranparency
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            MySqlDataBase dbConnection = new MySqlDataBase();
            await dbConnection.CreateTableAsync();
        }


        private async void ScanButton_Clicked(object sender, EventArgs e)
        {
            var scanPage = new ZXingScannerPage();
            await Navigation.PushAsync(scanPage);

            scanPage.OnScanResult += (result) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync();
                    await DisplayAlert("QR Code Value", result.Text, "OK");
                });
            };
        }

        private async void ProcessQrCodeResult(string qrCodeData)
        {
            try
            {
                MySqlDataBase dbConnection = new MySqlDataBase();
                using (var connection = dbConnection.GetConnection())
                {
                    await connection.OpenAsync();

                    string query = "INSERT INTO my_table (qr_code_data) VALUES (@qrCodeData);";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@qrCodeData", qrCodeData);
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}

using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
using System.Text.Json.Nodes;
using static SQLite.SQLite3;

namespace TravelApp
{
    public partial class MainPage : ContentPage
    {
        public string apiKey;

        public MainPage()
        {
            InitializeComponent();
        }

        private async Task LoadApiKey()
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("apiKey.txt");
            using var reader = new StreamReader(stream);
            apiKey = reader.ReadToEnd();
        }

        private async void LoadData(object sender, EventArgs e)
        {
            // Load the API Key
            await LoadApiKey();

            // Use HttpClient to get JSON data from the URL using the API key
            var client = new HttpClient();
            var response = await client.GetAsync($"https://maps.googleapis.com/maps/api/place/textsearch/json?query=new+york+city+point+of+interest&language=en&key={apiKey}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                // Deserialize JSON to select names of tourist attractions
                var jsonObject = JsonObject.Parse(content);
                var results = jsonObject["results"];
                foreach(var result in results)
                {
                    if(result["name"])
                    {

                    }
                }
                System.Diagnostics.Debug.WriteLine(results);

            }

            await Navigation.PushAsync(new InfoPage());
        }
    }
}
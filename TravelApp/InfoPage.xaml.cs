namespace TravelApp;
using System.Text.Json;
using static System.Net.WebRequestMethods;

public partial class InfoPage : ContentPage
{
    private string destination { get; set; }
    private List<string> attractions { get; set; }
    private string apiKey { get; set; }

    public InfoPage(string Destination, List<string> Attractions, string ApiKey)
    {
        InitializeComponent();
        destination = Destination;
        attractions = Attractions;
        apiKey = ApiKey;
    }

    private async void ShowMapButton_Clicked(object sender, EventArgs e)
    {
        // Navigate to MapPage
        await Navigation.PushAsync(new NavigationPage(new MapPage(destination, apiKey)));
    }
}
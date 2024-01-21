namespace TravelApp;

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.Json;
using static System.Net.WebRequestMethods;


public partial class InfoPage : ContentPage
{
    private string destination { get; set; }
    private List<string> attractions { get; set; }
    private string apiKey { get; set; }
    private readonly string gptResponse;
    // private readonly string gptResponseAtt;

    public InfoPage(string Destination, List<string> Attractions, string ApiKey, string GptResponse)
    {
        InitializeComponent();
        destination = Destination;
        attractions = Attractions;
        gptResponse = GptResponse;
        //gptResponseAtt = GptResponseAtt;
        apiKey = ApiKey;
        Okay();
    }

    private async void ShowMapButton_Clicked(object sender, EventArgs e)
    {
        // Navigate to MapPage
        await Navigation.PushAsync(new NavigationPage(new MapPage(destination, apiKey)));
    }
    private void Okay()
    {
        // display gpt response along with other information
        gptLabel.Text = $"gpt response: {gptResponse}";
        destinationLabel.Text = $"destination: {destination}";
        attractionsLabel.Text = $"attractions: {string.Join(", ", attractions)}";



    }


}
namespace TravelApp;

public partial class MapPage : ContentPage
{
    public string GoogleMapsUrl { get; set; }

    public MapPage(string destination, string apiKey)
    {
        InitializeComponent();

        // Construct the google maps url
        var encodedText = destination.Replace(" ", "+");
        GoogleMapsUrl = $"https://www.google.com/maps/search/?api=1&query={encodedText}&key={apiKey}";

        BindingContext = this;
    }
}
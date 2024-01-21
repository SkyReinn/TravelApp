using System.Diagnostics;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
using System.Text.Json.Nodes;
using static SQLite.SQLite3;

namespace TravelApp
{
    public partial class MainPage : ContentPage
    {
        public string apiKey;
        List<string> attractions = new List<string>();

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

            // Encode the entered destination
            var entry = (Entry)sender;
            var destination = entry.Text;
            
            var encodedText = Uri.EscapeDataString(destination);
            var gptResponse = GetGPTResponse(entry.Text);

            // Use HttpClient to get JSON data from the URL using the API key
            var client = new HttpClient();
            var response = await client.GetAsync($"https://maps.googleapis.com/maps/api/place/textsearch/json?query={encodedText}+points+of+interest&language=en&key={apiKey}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                // Deserialize JSON to select names of tourist attractions
                var jsonObject = JsonObject.Parse(content);
                var results = jsonObject["results"];

                // Add the names of the attractions to a list
                foreach (var result in results.AsArray())
                {
                    string name = result["name"].ToString();
                    attractions.Add(name);
                }

                // Print out the list in console
                foreach (var attraction in attractions)
                    System.Diagnostics.Debug.WriteLine(attraction);
            }

            await Navigation.PushAsync(new InfoPage(destination, attractions, apiKey, gptResponse));
        }

        private static string GetGPTResponse(string? userMessage)
        {
            string pythonInterpreterPath = "C:\\Users\\Arnav Choudhary\\OneDrive\\Documents\\gpt4freeTesting\\venv\\Scripts\\python.exe";  // Replace with the actual path to your python.exe
            string pythonScriptPath = "C:\\Users\\Arnav Choudhary\\OneDrive\\Documents\\gpt4freeTesting\\test.py";

            ProcessStartInfo start = new ProcessStartInfo
            {
                FileName = pythonInterpreterPath,
                Arguments = $"\"{pythonScriptPath}\" \"{userMessage}\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
            };

            using (Process process = new Process { StartInfo = start })
            {
                process.Start();

                // Read GPT response from the standard output
                string response = process.StandardOutput.ReadToEnd();


                return response;
            }
        }
    }

}
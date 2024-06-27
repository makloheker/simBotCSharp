// yahahah ga bisa oop y?
using Newtonsoft.Json.Linq;

class Program
{
    private static readonly HttpClient client = new HttpClient();

    static async Task Main()
    {
        while (true)
        {
            Console.Write("you>: ");
            string? inputText = Console.ReadLine();

            if (inputText == null ||
                inputText.Equals("exit", StringComparison.OrdinalIgnoreCase) ||
                inputText.Equals("quit", StringComparison.OrdinalIgnoreCase) ||
                inputText.Equals("murtad", StringComparison.OrdinalIgnoreCase) ||
                inputText.Equals("keluar", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("yahh logout bang...");
                break;
            }

            string message = await SendRequest(inputText);
            Console.WriteLine($"bot>: {message}");
        }
    }

    static async Task<string> SendRequest(string text)
    {
        var values = new Dictionary<string, string>
        {
            { "text", text },
            { "lc", "id" }
        };

        var content = new FormUrlEncodedContent(values);
        HttpResponseMessage response = await client.PostAsync("https://api.simsimi.vn/v1/simtalk", content);

        if (response.IsSuccessStatusCode)
        {
            string responseString = await response.Content.ReadAsStringAsync();
            var responseJson = JObject.Parse(responseString);
            return responseJson["message"]?.ToString() ?? "Tidak ada pesan yang diterima";
        }
        else
        {
            return $"Error bang {response.StatusCode}";
        }
    }
}

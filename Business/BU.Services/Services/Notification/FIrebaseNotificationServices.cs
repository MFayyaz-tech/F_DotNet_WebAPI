using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class FirebaseMessagingService
{
    private static readonly HttpClient _httpClient = new HttpClient();
    private const string FirebaseUrl = "https://fcm.googleapis.com/v1/projects/figgersenterprise-b28db/messages:send";

    public static async Task<string> GetAccessTokenAsync()
    {
        var credential = GoogleCredential.FromFile("resourse/figgersenterprise-b28db-firebase-adminsdk-mdued-8891dbcfe5.json")
                                         .CreateScoped("https://www.googleapis.com/auth/firebase.messaging");
        var accessToken = await credential.UnderlyingCredential.GetAccessTokenForRequestAsync();
        Console.WriteLine($"Access Token: {accessToken}");  // Log the access token
        return accessToken;
    }

    public static async Task SendMessageAsync(string token, string title, string body, Dictionary<string, string> data)
    {
        var accessToken = await GetAccessTokenAsync();

        var payload = new
        {
            message = new
            {
                token = token,
                notification = new
                {
                    title = title,
                    body = body
                },
                data = data


            }
        };

        string jsonPayload = JsonConvert.SerializeObject(payload);
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + accessToken);
            StringContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(FirebaseUrl, content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("FCM message sent successfully.");
            }
            else
            {
                Console.WriteLine($"Error sending FCM message: {response.StatusCode}");
                Console.WriteLine($"Response: {await response.Content.ReadAsStringAsync()}"); // Log the response content
                Console.WriteLine($"Request Headers: {response.RequestMessage.Headers}");
                Console.WriteLine($"Request Uri: {response.RequestMessage.RequestUri}");
            }
        }
    }
}

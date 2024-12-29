using System.Threading.Tasks;
using RestSharp;
using UnityEngine;

public class StyleTransfer : MonoBehaviour
{
    private async void Start()
    {
        await PerformStyleTransfer();
    }

    private async Task PerformStyleTransfer()
    {
        var options = new RestClientOptions("https://api.picsart.io/tools/1.0/styletransfer");
        var client = new RestClient(options);
        var request = new RestRequest("", Method.Post)
        {
            AlwaysMultipartFormData = true
        };

        request.AddHeader("accept", "application/json");
        request.AddHeader("X-Picsart-API-Key", "eyJraWQiOiI5NzIxYmUzNi1iMjcwLTQ5ZDUtOTc1Ni05ZDU5N2M4NmIwNTEiLCJhbGciOiJSUzI1NiJ9.eyJzdWIiOiJhdXRoLXNlcnZpY2UtYjc5MDJmM2ItOTgxYS00MjJmLTllZjQtYzMyNzgyNmQ4MDMwIiwiYXVkIjoiNDY4OTU1NDgxMDcyMTAxIiwibmJmIjoxNzM1MzUxNTY1LCJzY29wZSI6WyJiMmItYXBpLmdlbl9haSIsImIyYi1hcGkuaW1hZ2VfYXBpIl0sImlzcyI6Imh0dHBzOi8vYXBpLnBpY3NhcnQuY29tL3Rva2VuLXNlcnZpY2UiLCJvd25lcklkIjoiNDY4OTU1NDgxMDcyMTAxIiwiaWF0IjoxNzM1MzUxNTY1LCJqdGkiOiIyNGEwMDZhYy04MjQwLTQzMWYtODQ2YS0xNTVjMjI4Y2I0OTQifQ.RLqXZNszc4boeK3oz3Vru8VLOB-S2fn6WV0q1z5GOBkUT6HTbDE3pvl4WXfERD9pT1KXNOsvl7TlqvgFkxDUs6j8wlrwf7GyhRQyGCMTcNvneCr0NNAnH7tLws6tZ5bT5Zeq4oE_7bCy7O0p-gg4LP9olZ7an62dwTBwM1lLB4UYgHNDGojmMBhVJIkhyBDn0-EAJ79zSAd-z1sISHEFwQuPTTOlRjE1cvighuzdf8G4zewO9q26Z9nlVxAdP6c8euHQHrcMy_OV_7UltEeK_s9uUHOVLD83eS8I7AT8wjI5YM5FebbIq8I0Vpdd9ylu-rMxzTlJPQhE-zgzoqq5Vg");
        request.AddParameter("level", "l1");
        request.AddParameter("format", "JPG");

        // Add images (ensure the files are in the StreamingAssets folder or accessible by path)
        var imagePath = Application.streamingAssetsPath + "/PortraitdeMademoisellePhuong.jpg";
        var referenceImagePath = Application.streamingAssetsPath + "/TheScream.jpg";

        request.AddFile("image", imagePath);
        request.AddFile("reference_image", referenceImagePath);

        // Send the request
        var response = await client.PostAsync(request);

        // Log the response
        if (response != null)
        {
            Debug.Log($"Response: {response.Content}");
        }
        else
        {
            Debug.LogError("Failed to get a response from the API.");
        }
    }
}

using UnityEngine;

public class StyleTransfer : MonoBehaviour
{
    [SerializeField] private GameObject uiButton;
    [SerializeField] private GameObject srpButton;
    private UploadImage uiScript;
    private SelectRefPainting srpScript;

    void Start()
    {
        // Get the required scripts from the assigned GameObjects
        uiScript = uiButton.GetComponent<UploadImage>();
        srpScript = srpButton.GetComponent<SelectRefPainting>();
    }

    public void ExecuteFunction()
    {
        if (uiScript == null || srpScript == null)
        {
            Debug.LogError("UploadImage or SelectRefPainting scripts are not assigned.");
            return;
        }

        // Get the uploaded image and reference painting URLs
        string uploadedImageUrl = uiScript.GetUploadedImageUrl();
        string referencePaintingUrl = srpScript.GetCurrentRefPaintingURL();

        if (string.IsNullOrEmpty(uploadedImageUrl) || string.IsNullOrEmpty(referencePaintingUrl))
        {
            Debug.LogError("Both an uploaded image and a reference painting must be selected.");
            return;
        }

        Debug.Log($"Starting style transfer with:\nUploaded Image: {uploadedImageUrl}\nReference Painting: {referencePaintingUrl}");

        // Build the JavaScript code to handle the API request
        string jsCode = $@"
            const base64ToBlob = (base64, type) => {{
                const binary = atob(base64.split(',')[1]); // Extract base64 data
                const array = [];
                for (let i = 0; i < binary.length; i++) {{
                    array.push(binary.charCodeAt(i));
                }}
                return new Blob([new Uint8Array(array)], {{ type }});
            }};

            const form = new FormData();
            form.append('level', 'l2');

            // Convert uploaded image to Blob
            const uploadedImageBase64 = '{uploadedImageUrl}';
            const uploadedImageBlob = base64ToBlob(uploadedImageBase64, 'image/jpeg');
            form.append('image', uploadedImageBlob, 'uploaded_image.jpg');

            // Add reference image URL directly
            form.append('reference_image_url', '{referencePaintingUrl}');

            const options = {{
                method: 'POST',
                headers: {{
                    accept: 'application/json',
                    'X-Picsart-API-Key': 'eyJraWQiOiI5NzIxYmUzNi1iMjcwLTQ5ZDUtOTc1Ni05ZDU5N2M4NmIwNTEiLCJhbGciOiJSUzI1NiJ9.eyJzdWIiOiJhdXRoLXNlcnZpY2UtYjc5MDJmM2ItOTgxYS00MjJmLTllZjQtYzMyNzgyNmQ4MDMwIiwiYXVkIjoiNDY4OTU1NDgxMDcyMTAxIiwibmJmIjoxNzM1MzUxNTY1LCJzY29wZSI6WyJiMmItYXBpLmdlbl9haSIsImIyYi1hcGkuaW1hZ2VfYXBpIl0sImlzcyI6Imh0dHBzOi8vYXBpLnBpY3NhcnQuY29tL3Rva2VuLXNlcnZpY2UiLCJvd25lcklkIjoiNDY4OTU1NDgxMDcyMTAxIiwiaWF0IjoxNzM1MzUxNTY1LCJqdGkiOiIyNGEwMDZhYy04MjQwLTQzMWYtODQ2YS0xNTVjMjI4Y2I0OTQifQ.RLqXZNszc4boeK3oz3Vru8VLOB-S2fn6WV0q1z5GOBkUT6HTbDE3pvl4WXfERD9pT1KXNOsvl7TlqvgFkxDUs6j8wlrwf7GyhRQyGCMTcNvneCr0NNAnH7tLws6tZ5bT5Zeq4oE_7bCy7O0p-gg4LP9olZ7an62dwTBwM1lLB4UYgHNDGojmMBhVJIkhyBDn0-EAJ79zSAd-z1sISHEFwQuPTTOlRjE1cvighuzdf8G4zewO9q26Z9nlVxAdP6c8euHQHrcMy_OV_7UltEeK_s9uUHOVLD83eS8I7AT8wjI5YM5FebbIq8I0Vpdd9ylu-rMxzTlJPQhE-zgzoqq5Vg'
                }},
                body: form
            }};

            fetch('https://api.picsart.io/tools/1.0/styletransfer', options)
                .then(res => res.json())
                .then(res => console.log(res))
                .catch(err => console.error(err));
        ";

        // Execute the JavaScript code in the browser
        Application.ExternalEval(jsCode);
    }
}

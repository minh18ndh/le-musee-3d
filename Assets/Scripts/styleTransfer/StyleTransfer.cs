using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class StyleTransfer : MonoBehaviour
{
    [SerializeField] private GameObject targetObject; 
    [SerializeField] private GameObject uiButton;
    [SerializeField] private GameObject srpButton;

    private UploadImage uiScript;
    private SelectRefPainting srpScript;

    private Vector3 originalScale;

    public string resultImageURL;

    public bool isTransferCompleted;

    void Start()
    {
        // Get the required scripts from the assigned GameObjects
        uiScript = uiButton.GetComponent<UploadImage>();
        srpScript = srpButton.GetComponent<SelectRefPainting>();

        targetObject.SetActive(false);

        isTransferCompleted = true;

        // Save the original scale of the cube
        if (targetObject != null)
        {
            originalScale = targetObject.transform.localScale;
        }
        else
        {
            Debug.LogError("Target object is not assigned.");
        }
    }

    public void ExecuteFunction()
    {
        if (uiScript == null || srpScript == null)
        {
            Debug.LogError("UploadImage or SelectRefPainting scripts are not assigned.");
            return;
        }

        UIManager.Instance.ShowNotification("Transferring, please wait...");
        isTransferCompleted = false;

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
                    'X-Picsart-API-Key': 'eyJraWQiOiI5NzIxYmUzNi1iMjcwLTQ5ZDUtOTc1Ni05ZDU5N2M4NmIwNTEiLCJhbGciOiJSUzI1NiJ9.eyJzdWIiOiJhdXRoLXNlcnZpY2UtZGZhYjFjZjMtZTExNi00NGZlLWI3N2QtYTg4YTc3MzhhNzAzIiwiYXVkIjoiNDc0MDUwMTg2MDEzMTAxIiwibmJmIjoxNzM2MzU0MjM2LCJzY29wZSI6WyJiMmItYXBpLmdlbl9haSIsImIyYi1hcGkuaW1hZ2VfYXBpIl0sImlzcyI6Imh0dHBzOi8vYXBpLnBpY3NhcnQuY29tL3Rva2VuLXNlcnZpY2UiLCJvd25lcklkIjoiNDc0MDUwMTg2MDEzMTAxIiwiaWF0IjoxNzM2MzU0MjM2LCJqdGkiOiI3YzNjMWQ4My00OTk4LTRlOGEtYjQzZS1jZmFjOWM4ZmJiZmMifQ.LGqRMop61GDZ8AESiA99B4qtOXi3o2TvMX9bxFh8dn3FnM8s2S6B7cdWwk9uxwdqksFzyoUKt2ffcVQTSjJ2BcTQKOqYv0kslmkVXdTehbcujPK0kWUKJAOiJtdjPm8dbRLnipnxiEkZxuFy-CZHjs9AYe74JmXSXB0mIBjJ7-8FZsbHkx9FWQlpGGZUzoS6dkMjltgHQcmN0_DaCVfy5bV7spvsxf6vuG7h--ZMOMMNl5sW4nwkNhOXNCDTRHsZZSAJqI_FsERSnhsq1a1e3fzztacyse_zMYer8GD2Dkc7VSYSK3NWyctZmVo4Oys4CwNovPpgD7TOARM0fNxQig'
                }},
                body: form
            }};

            fetch('https://api.picsart.io/tools/1.0/styletransfer', options)
                .then(res => res.json())
                .then(res => {{
                    if (res.status === 'success') {{
                        SendMessage('{gameObject.name}', 'OnStyleTransferComplete', res.data.url);
                    }} else {{
                        console.error('Style Transfer failed:', res);
                    }}
                }})
                .catch(err => console.error(err));
        ";

        // Execute the JavaScript code in the browser
        Application.ExternalEval(jsCode);
    }

    // Callback when style transfer is complete
    public void OnStyleTransferComplete(string imageUrl)
    {
        Debug.Log($"Style Transfer completed. Image URL: {imageUrl}");

        resultImageURL = imageUrl;

        // Start coroutine to download and apply the image
        StartCoroutine(DownloadAndApplyImage(imageUrl));
        UIManager.Instance.ShowNotification("Style transfer completed!");
        isTransferCompleted = true;
    }

    private IEnumerator DownloadAndApplyImage(string imageUrl)
    {
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageUrl))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                // Get the downloaded texture
                Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;

                targetObject.SetActive(true);

                // Apply the texture to the target cube's material
                if (targetObject != null)
                {
                    Renderer renderer = targetObject.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        renderer.material.mainTexture = texture;

                        // Adjust the cube's scale based on the aspect ratio of the texture
                        AdjustObjectScale(targetObject, texture.width, texture.height);
                    }
                    else
                    {
                        Debug.LogError("Target object does not have a Renderer component.");
                    }
                }
                else
                {
                    Debug.LogError("Target object is not assigned.");
                }
            }
            else
            {
                Debug.LogError($"Failed to download image from {imageUrl}. Error: {request.error}");
            }
        }
    }

    private void AdjustObjectScale(GameObject obj, int textureWidth, int textureHeight)
    {
        float aspectRatio = (float)textureWidth / textureHeight;

        // Get the original scale of the object
        Vector3 scale = originalScale;

        // Adjust the scale based on the aspect ratio
        if (aspectRatio > 1)
        {
            // Wider than tall
            obj.transform.localScale = new Vector3(scale.y * aspectRatio, scale.y, scale.z);
        }
        else
        {
            // Taller than wide
            obj.transform.localScale = new Vector3(scale.y, scale.y / aspectRatio, scale.z);
        }

        // Debug.Log($"Adjusted object scale to match aspect ratio: {aspectRatio}");
    }
}

using UnityEngine;

public class UploadImage : MonoBehaviour
{
    public GameObject targetObject; // Assign the 3D object (e.g., a plane)
    private Vector3 originalScale;

    private string uploadedImageUrl; // Store the uploaded image URL in the desired format

    void Start()
    {
        originalScale = targetObject.transform.localScale;
    }

    public void ExecuteFunction()
    {
        string jsCode = @"
            var input = document.createElement('input');
            input.type = 'file';
            input.accept = 'image/*';

            input.onchange = function (event) {
                //const file = event.target.files[0]; // First file user uploads
                //console.log(file); // This is Blob
                var file = event.target.files[0];
                if (file) {
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        var base64Data = e.target.result.split(',')[1]; // Extract base64 data
                        var mimeType = file.type; // Get MIME type (e.g., image/jpeg)
                        var fileName = file.name; // Get file name
                        var dataUri = `data:${mimeType};name=${fileName};base64,${base64Data}`;
                        SendMessage('" + gameObject.name + @"', 'OnImageUploaded', dataUri);
                    };
                    reader.readAsDataURL(file); // Read file as Data URL
                }
            };

            input.click();
        ";

        Application.ExternalEval(jsCode);

        targetObject.SetActive(true);

        UIManager.Instance.ShowNotification("UploadImage");
        Debug.Log("UploadImage executed.");
    }

    // Callback to handle the uploaded image
    public void OnImageUploaded(string dataUri)
    {
        Debug.Log("Image uploaded and received in Unity.");

        // Store the data URI in the desired format
        uploadedImageUrl = dataUri;

        // Extract the base64 data from the data URI
        string base64Data = dataUri.Substring(dataUri.IndexOf(",") + 1);

        // Convert the base64 string to a byte array
        byte[] imageBytes = System.Convert.FromBase64String(base64Data);

        // Create a Texture2D and load the image data
        Texture2D texture = new Texture2D(1, 1);
        texture.LoadImage(imageBytes);

        // Apply the texture to the target object's material
        if (targetObject != null)
        {
            Renderer renderer = targetObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.mainTexture = texture;

                // Adjust the scale of the target object based on the image's aspect ratio
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

        Debug.Log($"Uploaded Image URL: {uploadedImageUrl}");
    }

    // Adjust the object's scale to match the aspect ratio of the texture
    private void AdjustObjectScale(GameObject obj, int textureWidth, int textureHeight)
    {
        // Calculate aspect ratio
        float aspectRatio = (float)textureWidth / textureHeight;

        // Get the current scale of the object
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

        Debug.Log($"Adjusted object scale to match aspect ratio: {aspectRatio}");
    }

    // Public method to return the uploaded image URL
    public string GetUploadedImageUrl()
    {
        return uploadedImageUrl;
    }
}

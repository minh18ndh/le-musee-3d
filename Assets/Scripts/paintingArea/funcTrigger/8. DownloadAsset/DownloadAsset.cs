using UnityEngine;
using System.IO;

public class DownloadAsset : MonoBehaviour
{
    // File name
    public string fileName = "PWTF.jpg";

    // Hosted file path
    public string hostedFilePath;

    // Local file path
    public string localFilePath = @"E:\le-musee-3d\Assets\Models\Paintings\PoolwithTwoFigures.jpg";

    // Local path where the file will be saved (for Unity Editor/Standalone)
    private string savePathforEditororStandaloneBuilds;

    void Start()
    {
        // Define the save path for Editor or Standalone builds
        savePathforEditororStandaloneBuilds = Path.Combine(@"C:\Users\minhn\Downloads", "painting.jpg");
    }

    public void ExecuteFunction()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        // In Editor or Standalone builds, perform the file operations
        LoadAndSaveLocalImage();
#elif UNITY_WEBGL
        // In WebGL, download using JavaScript
        DownloadImageForWebGL();
#endif
    }

#if UNITY_EDITOR || UNITY_STANDALONE
    private void LoadAndSaveLocalImage()
    {
        if (File.Exists(localFilePath))
        {
            try
            {
                // Load the image file as bytes
                byte[] imageBytes = File.ReadAllBytes(localFilePath);

                // Save the image to the defined path
                File.WriteAllBytes(savePathforEditororStandaloneBuilds, imageBytes);

                Debug.Log("Image successfully copied to: " + savePathforEditororStandaloneBuilds);
            }
            catch (System.Exception e)
            {
                Debug.LogError("Failed to process image: " + e.Message);
            }
        }
        else
        {
            Debug.LogError("Local image file does not exist: " + localFilePath);
        }
    }
#endif

#if UNITY_WEBGL
    private void DownloadImageForWebGL()
    {
        // Use Unity's WebGL integration to call JavaScript
        string jsCode = @"
            var link = document.createElement('a');
            link.target = ""_blank"";
            link.href = '" + hostedFilePath + @"';
            link.download = '" + fileName + @"';
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
        ";
        Application.ExternalEval(jsCode);
    }
#endif
}

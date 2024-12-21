using UnityEngine;
using System.IO;

public class DownloadAsset : MonoBehaviour
{
    // File name
    //public string hostedFileName;

    // Hosted file path
    public string hostedFilePath;

    // Local file path
    //public string localFilePath = @"E:\le-musee-3d\Assets\Models\Paintings\PoolwithTwoFigures.jpg";

    // Local path where the file will be saved (for Unity Editor/Standalone)
    /*private string savePathforEditororStandaloneBuilds;

    void Start()
    {
        // Define the save path for Editor or Standalone builds
        savePathforEditororStandaloneBuilds = Path.Combine(@"C:\Users\minhn\Downloads", "painting.jpg");
    }*/

    public void ExecuteFunction()
    {
        UIManager.Instance.ShowNotification("Opening asset's link for download...");
        Debug.Log("Download Asset executed.");
/*#if UNITY_EDITOR || UNITY_STANDALONE
        // In Editor or Standalone builds, perform the file operations
        LoadAndSaveLocalFile();*/
//#elif UNITY_WEBGL
        // In WebGL, download using JavaScript
        DownloadFileForWebGL();
//#endif
    }

    public void HaltFunction()
    {

    }

/*#if UNITY_EDITOR || UNITY_STANDALONE
    private void LoadAndSaveLocalFile()
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
#endif*/

//#if UNITY_WEBGL
    private void DownloadFileForWebGL()
    {
        // Use Unity's WebGL integration to call JavaScript
        // Delay for 1 second before opening the asset's link in a new tab
        string jsCode = @"
            var link = document.createElement('a');
            link.target = ""_blank"";
            link.href = '" + hostedFilePath + @"';
            setTimeout(function() {
                document.body.appendChild(link);
                link.click();
                document.body.removeChild(link);
            }, 1000);
        ";
        Application.ExternalEval(jsCode);
    }
//#endif
}

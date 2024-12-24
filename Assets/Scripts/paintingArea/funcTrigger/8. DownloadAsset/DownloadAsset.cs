using UnityEngine;
using System.IO;

public class DownloadAsset : MonoBehaviour
{
    // Hosted file path
    public string hostedFilePath;

    public void ExecuteFunction()
    {
        UIManager.Instance.ShowNotification("Opening asset's link for download...");
        Debug.Log("Download Asset executed.");

        // In WebGL, download using JavaScript
        DownloadFileForWebGL();
    }

    public void HaltFunction()
    {

    }

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

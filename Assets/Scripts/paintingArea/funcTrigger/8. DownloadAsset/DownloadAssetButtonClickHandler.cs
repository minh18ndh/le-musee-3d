using UnityEngine;

public class DownloadAssetButtonClickHandler : MonoBehaviour
{
    private DownloadAsset script;

    void Start()
    {
        script = GetComponent<DownloadAsset>();
    }

    void OnMouseDown()
    {
        if (script != null)
        {
            // Call a method from function script when click
            script.ExecuteFunction();
        }
    }
}


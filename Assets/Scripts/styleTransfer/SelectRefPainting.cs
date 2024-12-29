using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class SelectRefPainting : MonoBehaviour
{
    public GameObject targetObject;
    private string[] paintingURLs;    // Array to hold painting file paths
    private string[] paintingPaths;   // Array to hold painting file paths (local)
    private int currentIndex = 0;     // Current selected painting index

    private Vector3 originalScale;

    private void Start()
    {
        // Save the original scale of the object for dynamic scaling
        if (targetObject != null)
        {
            originalScale = targetObject.transform.localScale;
        }
        else
        {
            Debug.LogError("Target object is not assigned.");
        }

        paintingURLs = new[]
        {
            "https://minh18ndhlemusee.blob.core.windows.net/lemusee-artfiles/PoolwithTwoFigures.jpg",
            "https://minh18ndhlemusee.blob.core.windows.net/lemusee-artfiles/NympheasenFleur.jpg",
            "https://minh18ndhlemusee.blob.core.windows.net/lemusee-artfiles/SalvatorMundi.jpg",
            "https://minh18ndhlemusee.blob.core.windows.net/lemusee-artfiles/TheScream.jpg"
        };

        // Initialize the paths to the paintings (relative to StreamingAssets)
        paintingPaths = new[]
        {
            GetLocalPaintingPath("PoolwithTwoFigures.jpg"),
            GetLocalPaintingPath("NympheasenFleur.jpg"),
            GetLocalPaintingPath("SalvatorMundi.jpg"),
            GetLocalPaintingPath("TheScream.jpg")
        };

        // Display the first painting initially
        StartCoroutine(DisplayPainting(paintingPaths[currentIndex]));
    }

    public void ExecuteFunction()
    {
        if (paintingPaths.Length == 0) return;

        // Move to the next painting
        currentIndex = (currentIndex + 1) % paintingPaths.Length;

        // Display the selected painting
        StartCoroutine(DisplayPainting(paintingPaths[currentIndex]));

        UIManager.Instance.ShowNotification("SelectRefPainting");
        Debug.Log($"SelectRefPainting executed. Current painting: {paintingPaths[currentIndex]}");
    }

    public string GetCurrentRefPaintingURL()
    {
        return paintingURLs[currentIndex];
    }

    private IEnumerator DisplayPainting(string paintingPath)
    {
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(paintingPath))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                // Get the downloaded texture
                Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;

                // Apply the texture to the target object's material
                if (targetObject != null)
                {
                    Renderer renderer = targetObject.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        renderer.material.mainTexture = texture;

                        // Adjust the object's scale dynamically to match the painting's aspect ratio
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
                Debug.LogError($"Failed to load painting from path: {paintingPath}. Error: {request.error}");
            }
        }
    }

    private string GetLocalPaintingPath(string fileName)
    {
        // Path to the StreamingAssets folder
#if UNITY_WEBGL
        return Application.streamingAssetsPath + "/Paintings/" + fileName;
#else
        return "file://" + Application.streamingAssetsPath + "/Paintings/" + fileName;
#endif
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

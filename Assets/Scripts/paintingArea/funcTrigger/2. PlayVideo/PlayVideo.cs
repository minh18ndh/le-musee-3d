using UnityEngine;
using UnityEngine.Video;

public class PlayVideo : MonoBehaviour
{
    private PlayVideoButtonClickHandler pvButton;
    [SerializeField] private GameObject TV;
    private VideoPlayer videoPlayer;
    private bool isVideoFinishedPlaying;

    private void Start()
    {
        pvButton = GetComponent<PlayVideoButtonClickHandler>();
        videoPlayer = TV.GetComponent<VideoPlayer>();
        isVideoFinishedPlaying = false;
    }

    private void Update()
    {
        if (TV.activeSelf && videoPlayer.isPlaying)
        {
            if (Mathf.Abs((float)(videoPlayer.time - videoPlayer.length)) < 0.1f)
            {
                isVideoFinishedPlaying = true;
            }
            /*Debug.Log("Current: " + videoPlayer.time);
            Debug.Log("Total: " + videoPlayer.length);*/
        }

        if (!videoPlayer.isPlaying && isVideoFinishedPlaying)
        {
            HaltFunction();
            isVideoFinishedPlaying = false;
        }
    }

    public void ExecuteFunction()
    {
        TV.SetActive(true);
        UIManager.Instance.ShowNotification("PlayVideo activated.");
        Debug.Log("PlayVideo executed!");
    }

    public void HaltFunction()
    {
        TV.SetActive(false);
        pvButton.FunctionActiveState(false);
        UIManager.Instance.ShowNotification("PlayVideo deactivated.");
        Debug.Log("PlayVideo deactivated.");
    }
}

using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    // Fast forward/rewind in seconds
    public float skipTime = 5f;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    void Update()
    {
        if (videoPlayer != null)
        {
            HandleVideoControls();
        }
    }

    private void HandleVideoControls()
    {
        // Play/Pause
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (videoPlayer.isPlaying)
            {
                videoPlayer.Pause();
                Debug.Log("Video Paused");
            }
            else
            {
                videoPlayer.Play();
                Debug.Log("Video Playing");
            }
        }

        // Rewind
        if (Input.GetKeyDown(KeyCode.R))
        {
            double newTime = videoPlayer.time - skipTime;
            videoPlayer.time = Mathf.Max((float)newTime, 0f); // Ensure time doesn't go below 0
            Debug.Log("Video Rewound");
        }

        // Fast Forward
        if (Input.GetKeyDown(KeyCode.F))
        {
            double newTime = videoPlayer.time + skipTime;
            videoPlayer.time = Mathf.Min((float)newTime, (float)videoPlayer.length); // Ensure time doesn't exceed video length
            Debug.Log("Video Fast Forwarded");
        }
    }
}

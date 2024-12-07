using UnityEngine;

public class PlayAudioGuide : MonoBehaviour
{
    [SerializeField] private GameObject PlayAudioGuideButton;
    [SerializeField] private GameObject artAudioGuide;
    private AudioSource audioGuideSource;
    private bool isAudioGuideFinishedPlaying;

    public void Start()
    {
        audioGuideSource = artAudioGuide.GetComponent<AudioSource>();
        isAudioGuideFinishedPlaying = false;
    }

    public void Update()
    {
        if (artAudioGuide.activeSelf)
        {
            isAudioGuideFinishedPlaying = audioGuideSource.time >= audioGuideSource.clip.length;
            Debug.Log(isAudioGuideFinishedPlaying);
        }

        if (!audioGuideSource.isPlaying && isAudioGuideFinishedPlaying)
        {
            HaltFunction();
            isAudioGuideFinishedPlaying = false;
        }
    }
    public void ExecuteFunction()
    {
        artAudioGuide.SetActive(true);
        audioGuideSource.Play();
        Debug.Log("PlayAudioGuide executed!");
    }

    public void HaltFunction()
    {
        audioGuideSource.Stop();
        artAudioGuide.SetActive(false);
        //isAudioGuideFinishedPlaying = true;
        Debug.Log("PlayAudioGuide deactivated.");
    }
}

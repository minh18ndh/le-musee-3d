using UnityEngine;

public class PlayAudioGuide : MonoBehaviour
{
    //private PlayAudioGuideButtonClickHandler pagButton;
    //[SerializeField] private GameObject PlayAudioGuideButton;
    [SerializeField] private GameObject artAudioGuide;
    private AudioSource audioGuideSource;
    private bool isAudioGuideFinishedPlaying;

    private void Start()
    {
        //pagButton = GetComponent<PlayAudioGuideButtonClickHandler>();
        audioGuideSource = artAudioGuide.GetComponent<AudioSource>();
        isAudioGuideFinishedPlaying = false;
    }

    private void Update()
    {
        if (artAudioGuide.activeSelf && audioGuideSource.clip != null)
        {
            if (Mathf.Abs((float)(audioGuideSource.time - audioGuideSource.clip.length)) < 0.1f)
            {
                isAudioGuideFinishedPlaying = true;
            }
            //Debug.Log(isAudioGuideFinishedPlaying);
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
        UIManager.Instance.ShowNotification("PlayAudioGuide activated.");
        Debug.Log("PlayAudioGuide activated.");
    }

    public void HaltFunction()
    {
        audioGuideSource.Stop();
        artAudioGuide.SetActive(false);
        //pagButton.FunctionActiveState(false);
        UIManager.Instance.ShowNotification("PlayAudioGuide deactivated.");
        Debug.Log("PlayAudioGuide deactivated.");
    }
}

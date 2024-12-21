using UnityEngine;
using UnityEngine.Video;

public class PlayAmbientSound : MonoBehaviour
{
    //private PlayAmbientSoundButtonClickHandler pasButton;
    //[SerializeField] private GameObject PlayAmbientSoundButton;
    [SerializeField] private GameObject artAmbientSound;
    private AudioSource ambientSoundSource;
    private bool isAmbientSoundFinishedPlaying;

    private void Start()
    {
        //pasButton = GetComponent<PlayAmbientSoundButtonClickHandler>();
        ambientSoundSource = artAmbientSound.GetComponent<AudioSource>();
        isAmbientSoundFinishedPlaying = false;
    }

    private void Update()
    {
        if (artAmbientSound.activeSelf && ambientSoundSource.clip != null)
        {
            if (Mathf.Abs((float)(ambientSoundSource.time - ambientSoundSource.clip.length)) < 0.1f)
            {
                isAmbientSoundFinishedPlaying = true;
            }
            //Debug.Log(isAudioGuideFinishedPlaying);
        }

        if (!ambientSoundSource.isPlaying && isAmbientSoundFinishedPlaying)
        {
            HaltFunction();
            isAmbientSoundFinishedPlaying = false;
        }
    }

    public void ExecuteFunction()
    {
        artAmbientSound.SetActive(true);
        ambientSoundSource.Play();
        UIManager.Instance.ShowNotification("PlayAmbientSound activated.");
        Debug.Log("PlayAmbientSound executed!");
    }

    public void HaltFunction()
    {
        ambientSoundSource.Stop();
        artAmbientSound.SetActive(false);
        //pasButton.FunctionActiveState(false);
        UIManager.Instance.ShowNotification("PlayAmbientSound deactivated.");
        Debug.Log("PlayAmbientSound deactivated.");
    }
}

using UnityEngine;

public class PlayAudioGuideButtonClickHandler : MonoBehaviour
{
    private PlayAudioGuide script;

    void Start()
    {
        script = GetComponent<PlayAudioGuide>();
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


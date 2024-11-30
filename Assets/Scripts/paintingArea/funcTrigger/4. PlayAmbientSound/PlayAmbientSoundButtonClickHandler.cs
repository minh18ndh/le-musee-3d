using UnityEngine;

public class PlayAmbientSoundButtonClickHandler : MonoBehaviour
{
    private PlayAmbientSound script;

    void Start()
    {
        script = GetComponent<PlayAmbientSound>();
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


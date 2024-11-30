using UnityEngine;

public class PlayVideoButtonClickHandler : MonoBehaviour
{
    private PlayVideo script;

    void Start()
    {
        script = GetComponent<PlayVideo>();
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


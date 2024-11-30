using UnityEngine;

public class ChangePaintingStyleButtonClickHandler : MonoBehaviour
{
    private ChangePaintingStyle script;

    void Start()
    {
        script = GetComponent<ChangePaintingStyle>();
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


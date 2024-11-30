using UnityEngine;

public class DepthOfFieldButtonClickHandler : MonoBehaviour
{
    private DepthOfField script;

    void Start()
    {
        script = GetComponent<DepthOfField>();
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


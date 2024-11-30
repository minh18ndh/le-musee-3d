using UnityEngine;

public class DepthOfFieldButtonClickHandler : MonoBehaviour
{
    private DepthOfField script;
    private bool isClicked;

    void Start()
    {
        script = GetComponent<DepthOfField>();
        isClicked = false;
    }

    void OnMouseDown()
    {
        isClicked = true;
    }

    void OnMouseUp()
    {
        if (isClicked && script != null)
        {
            // Call a method from function script when click
            script.ExecuteFunction();
        }

        isClicked = false;
    }
}


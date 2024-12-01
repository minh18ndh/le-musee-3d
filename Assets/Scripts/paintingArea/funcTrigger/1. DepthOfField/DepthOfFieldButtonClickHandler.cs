using UnityEngine;

public class DepthOfFieldButtonClickHandler : MonoBehaviour
{
    private DepthOfField dofScript;
    private bool isClicked;
    public bool executeFunction;

    void Start()
    {
        dofScript = GetComponent<DepthOfField>();
        isClicked = false;
        executeFunction = false;
    }

    void OnMouseDown()
    {
        isClicked = true;
    }

    void OnMouseUp()
    {
        executeFunction = !executeFunction;

        if (isClicked && executeFunction && dofScript != null)
        {
            dofScript.ExecuteFunction();
        }

        else
        {
            dofScript.HaltFunction();
        }

        isClicked = false;
    }

    public void SetExecuteFunction(bool enabled)
    {
        executeFunction = enabled;
    }
}


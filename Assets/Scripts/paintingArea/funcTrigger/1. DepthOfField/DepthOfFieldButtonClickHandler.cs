using UnityEngine;

public class DepthOfFieldButtonClickHandler : MonoBehaviour
{
    private DepthOfField dofScript;
    private FuncTriggerManager triggerManager; // Reference to parent manager
    private bool isClicked;

    void Start()
    {
        dofScript = GetComponent<DepthOfField>();
        triggerManager = GetComponentInParent<FuncTriggerManager>(); // Get the manager from parent
        isClicked = false;
    }

    void Update()
    {
        // Exit the function when Q is pressed
        if (Input.GetKeyDown(KeyCode.Q) && dofScript != null)
        {
            dofScript.HaltFunction();
            triggerManager.FunctionDeactivated(); // Notify manager to unlock for interactions
        }
    }

    void OnMouseDown()
    {
        if (!triggerManager.CanActivateFunction() && triggerManager != null)
        {
            // If function activation is denied (another function is active), ignore the click
            return;
        }

        isClicked = true;
    }

    void OnMouseUp()
    {
        if (isClicked && dofScript != null)
        {
            dofScript.ExecuteFunction();
        }
        isClicked = false;
    }
}

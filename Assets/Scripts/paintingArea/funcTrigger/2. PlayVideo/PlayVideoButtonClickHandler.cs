using UnityEngine;

public class PlayVideoButtonClickHanler : MonoBehaviour
{
    private PlayVideo pvScript;
    private FuncTriggerManager triggerManager; // Reference to parent manager
    private bool isClicked;

    void Start()
    {
        pvScript = GetComponent<PlayVideo>();
        triggerManager = GetComponentInParent<FuncTriggerManager>(); // Get the manager from parent
        isClicked = false;
    }

    void Update()
    {
        // Exit the function when Q is pressed
        if (Input.GetKeyDown(KeyCode.Q) && pvScript != null)
        {
            pvScript.HaltFunction();
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
        if (isClicked && pvScript != null)
        {
            pvScript.ExecuteFunction();
        }
        isClicked = false;
    }
}

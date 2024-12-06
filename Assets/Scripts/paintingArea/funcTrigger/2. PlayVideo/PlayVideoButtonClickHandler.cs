using UnityEngine;

public class PlayVideoButtonClickHandler : MonoBehaviour
{
    private PlayVideo pvScript;
    private FuncTriggerManager triggerManager;
    private bool isClicked;

    void Start()
    {
        pvScript = GetComponent<PlayVideo>();
        triggerManager = GetComponentInParent<FuncTriggerManager>();
        isClicked = false;
    }

    void Update()
    {
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
            UIManager.Instance.ShowActiveFunctionWarning();
            return;  // If function activation denied (another function is active), ignore the click
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

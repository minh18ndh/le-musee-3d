using UnityEngine;

public class PlayVideoButtonClickHandler : MonoBehaviour
{
    private PlayVideo pvScript;
    private FuncTriggerManager triggerManager;
    private bool isClicked;
    private bool isQpressed;

    void Start()
    {
        pvScript = GetComponent<PlayVideo>();
        triggerManager = GetComponentInParent<FuncTriggerManager>();
        isClicked = false;
        isQpressed = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isQpressed = true;
        }

        if (isQpressed && Input.GetKeyDown(KeyCode.Alpha2) && pvScript != null)
        {
            pvScript.HaltFunction();
            isQpressed = false;
            //triggerManager.FunctionDeactivated(); // Notify manager to unlock for interactions
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            isQpressed = false;
        }
    }

    void OnMouseDown()
    {
        /*if (!triggerManager.CanActivateFunction() && triggerManager != null)
        {
            UIManager.Instance.ShowActiveFunctionWarning();
            return;  // If function activation denied (another function is active), ignore the click
        }*/

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

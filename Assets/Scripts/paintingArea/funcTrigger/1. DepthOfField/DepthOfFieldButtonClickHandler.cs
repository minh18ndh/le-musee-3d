using UnityEngine;

public class DepthOfFieldButtonClickHandler : MonoBehaviour
{
    private DepthOfField dofScript;
    //private FuncTriggerManager triggerManager;
    private bool isClicked;
    //private bool isFirstClick;
    private bool isQpressed;
    private bool isFunctionActive;

    void Start()
    {
        dofScript = GetComponent<DepthOfField>();
        //triggerManager = GetComponentInParent<FuncTriggerManager>();
        isClicked = false;
        //isFirstClick = true;
        isQpressed = false;
        isFunctionActive = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isQpressed = true;
        }

        if (isQpressed && Input.GetKeyDown(KeyCode.Alpha1) && dofScript != null)
        {
            dofScript.HaltFunction();
            isQpressed = false;
            //triggerManager.FunctionDeactivated(); // Notify manager to unlock for interactions
            //isFirstClick = true;
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            isQpressed = false;
        }
    }

    void OnMouseDown()
    {
        if (isFunctionActive)  // if (!triggerManager.CanActivateFunction())
        {
            UIManager.Instance.ShowNotification("The function is already running.");
            return;  // If function activation denied (the function is active), ignore the click
        }

        isClicked = true;
        //isFirstClick = false;
    }

    void OnMouseUp()
    {
        if (isClicked && dofScript != null)
        {
            dofScript.ExecuteFunction();
        }

        //else
        //{
        //    if (!isFirstClick)
        //    {
        //        UIManager.Instance.ShowActiveFunctionWarning();
        //    }
        //}
        isFunctionActive = true;
        //triggerManager.FunctionActivated(); // Notify manager to lock for interactions
        isClicked = false;
    }

    public void FunctionActiveState(bool isActive)
    {
        isFunctionActive = isActive;
    }
}

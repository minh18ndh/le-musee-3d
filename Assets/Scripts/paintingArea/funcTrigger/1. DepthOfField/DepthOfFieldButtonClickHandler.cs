using UnityEngine;

public class DepthOfFieldButtonClickHandler : MonoBehaviour
{
    private DepthOfField dofScript;
    private FuncTriggerManager triggerManager;
    private bool isClicked;
    //private bool isFirstClick;
    private bool isQpressed;

    void Start()
    {
        dofScript = GetComponent<DepthOfField>();
        triggerManager = GetComponentInParent<FuncTriggerManager>();
        isClicked = false;
        //isFirstClick = true;
        isQpressed = false;
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
        /*if (!triggerManager.CanActivateFunction() && triggerManager != null)
        {
            UIManager.Instance.ShowActiveFunctionWarning();
            return;  // If function activation denied (another function is active), ignore the click
        }*/

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

        isClicked = false;
    }
}

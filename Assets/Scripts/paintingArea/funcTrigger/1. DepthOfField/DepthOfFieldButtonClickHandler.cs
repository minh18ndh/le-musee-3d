using UnityEngine;

public class DepthOfFieldButtonClickHandler : MonoBehaviour
{
    private DepthOfField dofScript;
    private FuncTriggerManager triggerManager;
    private bool isClicked;
    //private bool isFirstClick;

    void Start()
    {
        dofScript = GetComponent<DepthOfField>();
        triggerManager = GetComponentInParent<FuncTriggerManager>();
        isClicked = false;
        //isFirstClick = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && dofScript != null)
        {
            dofScript.HaltFunction();
            triggerManager.FunctionDeactivated(); // Notify manager to unlock for interactions
            //isFirstClick = true;
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

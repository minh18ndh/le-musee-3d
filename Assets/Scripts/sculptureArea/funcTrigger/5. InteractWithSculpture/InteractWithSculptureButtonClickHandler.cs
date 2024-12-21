using UnityEngine;

public class InteractWithSculptureButtonClickHandler : MonoBehaviour
{
    private InteractWithSculpture iwsScript;
    //private FuncTriggerManager triggerManager;
    private bool isClicked;
    private bool isQpressed;
    private bool isFunctionActive;

    void Start()
    {
        iwsScript = GetComponent<InteractWithSculpture>();
        //triggerManager = GetComponentInParent<FuncTriggerManager>();
        isClicked = false;
        isQpressed = false;
        isFunctionActive = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isQpressed = true;
        }

        if (isQpressed && Input.GetKeyDown(KeyCode.Alpha5) && iwsScript != null)
        {
            iwsScript.HaltFunction();
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
        if (isFunctionActive)
        {
            UIManager.Instance.ShowNotification("The function is already running.");
            return;  // If the function is active, ignore the click
        }

        isClicked = true;
    }

    void OnMouseUp()
    {
        if (isClicked && iwsScript != null)
        {
            iwsScript.ExecuteFunction();
        }

        isFunctionActive = true;
        isClicked = false;
    }

    public void FunctionActiveState(bool isActive)
    {
        isFunctionActive = isActive;
    }

    public bool GetFunctionActiveState()
    {
        return isFunctionActive;
    }
}

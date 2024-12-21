using UnityEngine;

public class PlayAudioGuideButtonClickHandler : MonoBehaviour
{
    private PlayAudioGuide pagScript;
    //private FuncTriggerManager triggerManager;
    private bool isClicked;
    private bool isQpressed;
    //private bool isFunctionActive;

    void Start()
    {
        pagScript = GetComponent<PlayAudioGuide>();
        //triggerManager = GetComponentInParent<FuncTriggerManager>();
        isClicked = false;
        isQpressed = false;
        //isFunctionActive = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isQpressed = true;
        }

        if (isQpressed && Input.GetKeyDown(KeyCode.Alpha3) && pagScript != null)
        {
            pagScript.HaltFunction();
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
        /*if (isFunctionActive)
        {
            UIManager.Instance.ShowNotification("The function is already running.");
            return;  // If function activation denied (another function is active), ignore the click
        }*/

        isClicked = true;
    }

    void OnMouseUp()
    {
        if (isClicked && pagScript != null)
        {
            pagScript.ExecuteFunction();
        }

        //isFunctionActive = true;
        isClicked = false;
    }

    /*public void FunctionActiveState(bool isActive)
    {
        isFunctionActive = isActive;
    }*/
}

using UnityEngine;

public class PlayAmbientSoundButtonClickHandler : MonoBehaviour
{
    private PlayAmbientSound pasScript;
    //private FuncTriggerManager triggerManager;
    private bool isClicked;
    private bool isQpressed;
    //private bool isFunctionActive;

    void Start()
    {
        pasScript = GetComponent<PlayAmbientSound>();
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

        if (isQpressed && Input.GetKeyDown(KeyCode.Alpha4) && pasScript != null)
        {
            pasScript.HaltFunction();
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
        if (isClicked && pasScript != null)
        {
            pasScript.ExecuteFunction();
        }

        //isFunctionActive = true;
        isClicked = false;
    }

    /*public void FunctionActiveState(bool isActive)
    {
        isFunctionActive = isActive;
    }*/
}

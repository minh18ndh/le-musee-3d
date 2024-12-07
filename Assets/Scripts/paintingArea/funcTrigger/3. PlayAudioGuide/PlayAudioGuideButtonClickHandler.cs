using UnityEngine;

public class PlayAudioGuideButtonClickHandler : MonoBehaviour
{
    private PlayAudioGuide pagScript;
    private FuncTriggerManager triggerManager;
    private bool isClicked;

    void Start()
    {
        pagScript = GetComponent<PlayAudioGuide>();
        triggerManager = GetComponentInParent<FuncTriggerManager>();
        isClicked = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && pagScript != null)
        {
            pagScript.HaltFunction();
            //triggerManager.FunctionDeactivated(); // Notify manager to unlock for interactions
        }
    }

    void OnMouseDown()
    {
        //if (!triggerManager.CanActivateFunction() && triggerManager != null)
        //{
        //    UIManager.Instance.ShowActiveFunctionWarning();
        //    return;  // If function activation denied (another function is active), ignore the click
        //}

        isClicked = true;
    }

    void OnMouseUp()
    {
        if (isClicked && pagScript != null)
        {
            pagScript.ExecuteFunction();
        }

        isClicked = false;
    }
}

using UnityEngine;

public class DownloadAssetButtonClickHandler : MonoBehaviour
{
    private DownloadAsset daScript;
    //private FuncTriggerManager triggerManager;
    private bool isClicked;
    //private bool isQpressed;
    //private bool isFunctionActive;

    void Start()
    {
        daScript = GetComponent<DownloadAsset>();
        //triggerManager = GetComponentInParent<FuncTriggerManager>();
        isClicked = false;
        //isQpressed = false;
        //isFunctionActive = false;
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
        if (isClicked && daScript != null)
        {
            daScript.ExecuteFunction();
        }

       // isFunctionActive = true;
        isClicked = false;
    }

    /*public void FunctionActiveState(bool isActive)
    {
        isFunctionActive = isActive;
    }*/
}

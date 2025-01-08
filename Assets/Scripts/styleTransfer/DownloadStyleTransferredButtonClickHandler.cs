using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownloadStyleTransferredButtonClickHandler : MonoBehaviour
{
    private DownloadStyleTransferred dstScript;
    //private FuncTriggerManager triggerManager;
    private bool isClicked;
    //private bool isQpressed;
    //private bool isFunctionActive;

    void Start()
    {
        dstScript = GetComponent<DownloadStyleTransferred>();
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
        if (isClicked && dstScript != null)
        {
            dstScript.ExecuteFunction();
        }

        // isFunctionActive = true;
        isClicked = false;
    }

    /*public void FunctionActiveState(bool isActive)
    {
        isFunctionActive = isActive;
    }*/
}

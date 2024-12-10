using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendArtLabelButtonClickHandler : MonoBehaviour
{
    private ExtendArtLabel aiScript;
    [SerializeField] private GameObject funcTrigger;
    private FuncTriggerManager triggerManager;
    private bool isClicked;
    private bool isQpressed;
    private bool isFunctionActive;

    void Start()
    {
        aiScript = GetComponent<ExtendArtLabel>();
        triggerManager = funcTrigger.GetComponent<FuncTriggerManager>();
        isClicked = false;
        isQpressed = false;
        isFunctionActive = false;
    }

    void OnMouseDown()
    {
        if (!triggerManager.IsPlayerInside())
        {
            return;
        }

        isClicked = true;
    }

    void OnMouseUp()
    {
        if (isClicked)
        {
            isFunctionActive = !isFunctionActive;
        }

        if (aiScript != null && isClicked && isFunctionActive)
        {
            aiScript.ExecuteFunction();
        }

        else if (aiScript != null && isClicked && !isFunctionActive)
        {
            aiScript.HaltFunction();
        }

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


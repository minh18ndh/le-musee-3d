using Unity.VisualScripting;
using UnityEngine;

public class FuncTriggerManager : MonoBehaviour
{
    public GameObject[] buttons;
    private bool isPlayerInsideTrigger = false;  // Track if player is inside trigger

    private DepthOfField dofScript;
    private DepthOfFieldButtonClickHandler dofEnable;

    private void Start()
    {
        dofScript = GetComponentInChildren<DepthOfField>();
        dofEnable = GetComponentInChildren<DepthOfFieldButtonClickHandler>();
        SetButtonVisibility(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPlayerInsideTrigger)
        {
            isPlayerInsideTrigger = true;
            SetButtonVisibility(true);
            Debug.Log(other.gameObject.name + " entered the trigger area.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isPlayerInsideTrigger)
        {
            dofScript.HaltFunction();
            dofEnable.SetExecuteFunction(false);

            isPlayerInsideTrigger = false;

            SetButtonVisibility(false);
            Debug.Log(other.gameObject.name + " exited the trigger area.");
        }
    }
    
    // Control button visibility
    private void SetButtonVisibility(bool visibility)
    {
        foreach (var button in buttons)
        {
            button.SetActive(visibility);
        }
    }
}

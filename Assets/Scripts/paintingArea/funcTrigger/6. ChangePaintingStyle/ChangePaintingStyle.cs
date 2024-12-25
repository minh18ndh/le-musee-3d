using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePaintingStyle : MonoBehaviour
{
    private ChangePaintingStyleButtonClickHandler cpsButton;
    public GameObject filteredCanvas;

    private FilterController filterController;

    private void Start()
    {
        cpsButton = GetComponent<ChangePaintingStyleButtonClickHandler>();
        filterController = filteredCanvas.GetComponent<FilterController>();
    }

    public void ExecuteFunction()
    {
        filteredCanvas.SetActive(true);
        filterController.SetQState(false);
        UIManager.Instance.ShowNotification("ChangePaintingStyle activated.");
        Debug.Log("ChangePaintingStyle activated.");
    }

    public void HaltFunction()
    {
        if (filteredCanvas.activeSelf && !filterController.IsBlindnessActive())  // Only hide filteredCanvas when no filter's activated
        {
            filterController.SetFilter(0);
            filteredCanvas.SetActive(false);
        }

        cpsButton.FunctionActiveState(false);
        UIManager.Instance.ShowNotification("ChangePaintingStyle deactivated.");
        Debug.Log("ChangePaintingStyle deactivated.");
    }
}

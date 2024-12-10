using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyDisabilityType : MonoBehaviour
{
    private ApplyDisabilityTypeButtonClickHandler adtButton;
    public GameObject filteredCanvas;

    private FilterController filterController;

    private void Start()
    {
        adtButton = GetComponent<ApplyDisabilityTypeButtonClickHandler>();
        filterController = filteredCanvas.GetComponent<FilterController>();
    }

    public void ExecuteFunction()
    {
        filteredCanvas.SetActive(true);
        //UIManager.Instance.ShowColorBlindnessOption();
        UIManager.Instance.ShowNotification("ApplyDisabilityTypes activated.");
        Debug.Log("ApplyDisabilityTypes activated.");
    }

    public void HaltFunction()
    {
        //if (filteredCanvas.activeSelf)
        //{
        //    filterController.SetFilter(0);
        //}

        if (!filterController.AreTwoFiltersActive())  // Only hide filteredCanvas when no filter's activated
        {
            filteredCanvas.SetActive(false);
        }
   
        adtButton.FunctionActiveState(false);
        //UIManager.Instance.HideColorBlindnessOption();
        UIManager.Instance.ShowNotification("ApplyDisabilityTypes deactivated.");
        Debug.Log("ApplyDisabilityTypes deactivated.");
    }
}

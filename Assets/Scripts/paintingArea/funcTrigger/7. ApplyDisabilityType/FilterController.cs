using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterController : MonoBehaviour
{
    public Texture[] filterTypes; // Drag all your filtered images into this array in the Inspector
    private Renderer filteredCanvasRenderer;

    [SerializeField] private GameObject adtObject;
    [SerializeField] private GameObject cpsObject;

    private ApplyDisabilityTypeButtonClickHandler adtButton;
    private ChangePaintingStyleButtonClickHandler cpsButton;

    private void Start()
    {
        adtButton = adtObject.GetComponent<ApplyDisabilityTypeButtonClickHandler>();
        cpsButton = cpsObject.GetComponent<ChangePaintingStyleButtonClickHandler>();

        filteredCanvasRenderer = GetComponent<Renderer>();

        // Set default texture (first in the array)
        if (filterTypes != null && filterTypes.Length > 0)
        {
            filteredCanvasRenderer.material.mainTexture = filterTypes[0];
        }
    }

    private void Update()
    {
        if (filterTypes != null && filterTypes.Length > 0 && filteredCanvasRenderer != null)
        {
            if (adtButton != null && adtButton.GetFunctionActiveState())
            {
                if (Input.GetKeyUp(KeyCode.Alpha1))
                {
                    SetFilter(0);
                    UIManager.Instance.ShowNotification("Normal mode.");
                    Debug.Log("Normal mode.");
                }

                else if (Input.GetKeyUp(KeyCode.Alpha2))
                {
                    SetFilter(1);
                    UIManager.Instance.ShowNotification("Protanopia mode.");
                    Debug.Log("Protanopia mode.");
                }

                else if (Input.GetKeyUp(KeyCode.Alpha3))
                {
                    SetFilter(2);
                    UIManager.Instance.ShowNotification("Deuteranopia mode.");
                    Debug.Log("Deuteranopia mode.");
                }

                else if (Input.GetKeyUp(KeyCode.Alpha4))
                {
                    SetFilter(3);
                    UIManager.Instance.ShowNotification("Tritanopia mode.");
                    Debug.Log("Tritanopia mode.");
                }

                else if (Input.GetKeyUp(KeyCode.Alpha5))
                {
                    SetFilter(4);
                    UIManager.Instance.ShowNotification("Achromatopsia mode.");
                    Debug.Log("Achromatopsia mode.");
                }
            }

            if (cpsButton != null && cpsButton.GetFunctionActiveState())
            {
                if (Input.GetKeyUp(KeyCode.Alpha6))
                {
                    SetFilter(5);
                    UIManager.Instance.ShowNotification("Normal mode.");
                    Debug.Log("Normal mode.");
                }

                else if (Input.GetKeyUp(KeyCode.Alpha7))
                {
                    SetFilter(6);
                    UIManager.Instance.ShowNotification("Protanopia mode.");
                    Debug.Log("Protanopia mode.");
                }
            }
        }
    }

    // Call this function from UI button to change texture
    public void SetFilter(int index)
    {
        if (filterTypes != null && index >= 0 && index < filterTypes.Length)
        {
            filteredCanvasRenderer.material.mainTexture = filterTypes[index];
        }

        else
        {
            Debug.LogWarning("Invalid index or filterTypes array is empty!");
        }
    }

    public bool AreTwoFiltersActive()
    {
        return adtButton.GetFunctionActiveState() && cpsButton.GetFunctionActiveState();
    }
}

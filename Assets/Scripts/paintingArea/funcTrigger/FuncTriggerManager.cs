using UnityEngine;
using TMPro;

public class FuncTriggerManager : MonoBehaviour
{
    public GameObject[] buttons;
    private bool isPlayerInsideTrigger;
    private bool isAnyFunctionActive;// Lock other interactions when true

    private DepthOfField dofScript;
    private PlayVideo pvScript;

    public TMP_Text warningText; // Reference to a TextMeshPro UI element
    private Coroutine warningCoroutine; // To handle coroutine properly

    private void Start()
    {
        dofScript = GetComponentInChildren<DepthOfField>();
        pvScript = GetComponentInChildren<PlayVideo>();

        isPlayerInsideTrigger = false;
        isAnyFunctionActive = false;

        SetButtonVisibility(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPlayerInsideTrigger)
        {
            isPlayerInsideTrigger = true;
            SetButtonVisibility(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isPlayerInsideTrigger)
        {
            // Reset everything when exit
            HaltAllFunctions();
            isAnyFunctionActive = false;

            isPlayerInsideTrigger = false;
            SetButtonVisibility(false);
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

    // Halt all active functions
    private void HaltAllFunctions()
    {
        if (dofScript != null)
        {
            dofScript.HaltFunction();
        }
        if (pvScript != null)
        {
            pvScript.HaltFunction();
        }
        // Add more calls to stop other functionalities here
    }

    // Lock interactions (cannot activate a function) when another function is active
    public bool CanActivateFunction()
    {
        if (isAnyFunctionActive) return false; // Prevent activation if any another function is active

        isAnyFunctionActive = true;
        return true; // Allow activation
    }

    // Unlock interactions when a function ends
    public void FunctionDeactivated()
    {
        isAnyFunctionActive = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithSculpture : MonoBehaviour
{
    private InteractWithSculptureButtonClickHandler iwsButton;

    private void Start()
    {
        iwsButton = GetComponent<InteractWithSculptureButtonClickHandler>();
    }

    public void ExecuteFunction()
    {
        UIManager.Instance.ShowNotification("Function not available. Coming soon...");
        Debug.Log("InteractWithSculpture activated.");
    }

    public void HaltFunction()
    {
        iwsButton.FunctionActiveState(false);
        UIManager.Instance.ShowNotification("InteractWithSculpture deactivated.");
        Debug.Log("InteractWithSculpture deactivated.");
    }
}

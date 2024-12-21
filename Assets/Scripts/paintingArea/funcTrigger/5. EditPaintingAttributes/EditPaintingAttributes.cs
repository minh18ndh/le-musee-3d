using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditPaintingAttributes : MonoBehaviour
{
    public void ExecuteFunction()
    {
        UIManager.Instance.ShowNotification("Function not available. Coming soon...");
        Debug.Log("EditPaintingAttributes activated.");
    }

    public void HaltFunction()
    {
        Debug.Log("EditPaintingAttributes deactivated.");
    }
}

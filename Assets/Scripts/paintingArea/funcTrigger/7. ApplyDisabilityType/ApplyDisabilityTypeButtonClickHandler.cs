using UnityEngine;

public class ApplyDisabilityTypeButtonClickHandler : MonoBehaviour
{
    private ApplyDisabilityType script;

    void Start()
    {
        script = GetComponent<ApplyDisabilityType>();
    }

    void OnMouseDown()
    {
        if (script != null)
        {
            // Call a method from function script when click
            script.ExecuteFunction();
        }
    }
}


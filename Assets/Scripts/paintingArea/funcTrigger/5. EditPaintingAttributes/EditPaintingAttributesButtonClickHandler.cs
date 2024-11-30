using UnityEngine;

public class EditPaintingAttributesButtonClickHandler : MonoBehaviour
{
    private EditPaintingAttributes script;

    void Start()
    {
        script = GetComponent<EditPaintingAttributes>();
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


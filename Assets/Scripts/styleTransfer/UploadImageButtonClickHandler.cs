using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UploadImageButtonClickHandler : MonoBehaviour
{
    private UploadImage uiScript;
    private bool isClicked;

    void Start()
    {
        uiScript = GetComponent<UploadImage>();
        isClicked = false;
    }

    void OnMouseDown()
    {
        isClicked = true;
    }

    void OnMouseUp()
    {
        if (isClicked && uiScript != null)
        {
            uiScript.ExecuteFunction();
        }

        isClicked = false;
    }
}

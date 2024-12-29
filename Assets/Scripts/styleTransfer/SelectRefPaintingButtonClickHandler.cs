using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRefPaintingButtonClickHandler : MonoBehaviour
{
    private SelectRefPainting srpScript;
    private bool isClicked;

    void Start()
    {
        srpScript = GetComponent<SelectRefPainting>();
        isClicked = false;
    }

    void OnMouseDown()
    {
        isClicked = true;
    }

    void OnMouseUp()
    {
        if (isClicked && srpScript != null)
        {
            srpScript.ExecuteFunction();
        }

        isClicked = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StyleTransferButtonClickHandler : MonoBehaviour
{
    private StyleTransfer stScript;
    private bool isClicked;

    void Start()
    {
        stScript = GetComponent<StyleTransfer>();
        isClicked = false;
    }

    void OnMouseDown()
    {
        isClicked = true;
    }

    void OnMouseUp()
    {
        if (isClicked && stScript != null && stScript.isTransferCompleted)
        {
            stScript.ExecuteFunction();
        }

        else if (isClicked && stScript != null && !stScript.isTransferCompleted)
        {
            Debug.Log("Transfer ongoing");
        }

        isClicked = false;
    }
}

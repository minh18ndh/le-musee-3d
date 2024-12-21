using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendArtLabel : MonoBehaviour
{
    [SerializeField] private GameObject infoPanel;
    private ExtendArtLabelButtonClickHandler aiButton;

    private Vector3 infoPanelOriginalPos;

    void Start()
    {
        aiButton = GetComponent<ExtendArtLabelButtonClickHandler>();
        infoPanelOriginalPos = infoPanel.transform.position;
    }

    public void ExecuteFunction()
    {
        infoPanel.SetActive(true);
        UIManager.Instance.ShowNotification("ArtInfo activated.");
        Debug.Log("ArtInfo activated.");
    }

    public void HaltFunction()
    {
        infoPanel.transform.position = infoPanelOriginalPos;
        infoPanel.SetActive(false);
        aiButton.FunctionActiveState(false);
        UIManager.Instance.ShowNotification("ArtInfo deactivated.");
        Debug.Log("ArtInfo deactivated.");
    }
}

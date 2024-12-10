using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendArtLabel : MonoBehaviour
{
    [SerializeField] private GameObject infoPanel;
    private ExtendArtLabelButtonClickHandler aiButton;

    void Start()
    {
        aiButton = GetComponent<ExtendArtLabelButtonClickHandler>();
    }

    public void ExecuteFunction()
    {
        infoPanel.SetActive(true);
        UIManager.Instance.ShowNotification("ArtInfo activated.");
        Debug.Log("ArtInfo activated.");
    }

    public void HaltFunction()
    {
        infoPanel.SetActive(false);
        aiButton.FunctionActiveState(false);
        UIManager.Instance.ShowNotification("ArtInfo deactivated.");
        Debug.Log("ArtInfo deactivated.");
    }
}

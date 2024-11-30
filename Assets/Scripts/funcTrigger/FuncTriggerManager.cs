using UnityEngine;

public class FuncTriggerManager : MonoBehaviour
{
    // (EDIT THIS REGION WHEN ADD NEW FUNCTIONS)
    #region Prefab for each Function's 3D Button
    public GameObject depthOfFieldButton, videoButton, audioGuideButton, downloadAssetButton,
        playAmbientSoundButton, changePaintingStyleButton, editPaintingAttributesButton,
        applyDisabilityTypesButton;
    #endregion

    private bool isPlayerInsideTrigger = false;  // Track if player is inside trigger
    private bool showButtons = false;

    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPlayerInsideTrigger)
        {
            isPlayerInsideTrigger = true;
            Debug.Log(other.gameObject.name + " entered the trigger area.");
            showButtons = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isPlayerInsideTrigger)
        {
            isPlayerInsideTrigger = false;
            Debug.Log(other.gameObject.name + " exited the trigger area.");
            showButtons = false;
        }
    }
}

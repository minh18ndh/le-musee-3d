using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DepthOfField : MonoBehaviour
{
    private DepthOfFieldButtonClickHandler dofButton;
    [SerializeField] private Volume globalVolume;
    [SerializeField] private Transform playerCamera;

    [SerializeField] Transform artFrame;
    private Vector3 artFrameOriginalPos;
    
    private UnityEngine.Rendering.Universal.DepthOfField depthOfField;
    private float maxFocusDistance = 50f;

    private bool dofEnabled; // Track whether DoF adjustment is enabled

    void Start()
    {
        dofButton = GetComponent<DepthOfFieldButtonClickHandler>();

        dofEnabled = false;
        artFrameOriginalPos = artFrame.localPosition;

        if (globalVolume != null && globalVolume.sharedProfile != null)
        {
            if (globalVolume.sharedProfile.TryGet(out depthOfField))
            {
                depthOfField.focusDistance.Override(1f);
                depthOfField.focalLength.Override(1f);
            }
        }
    }

    void Update()
    {
        if (dofEnabled)
        {
            AdjustDepthOfField();
        }
    }

    public void ExecuteFunction()
    {
        dofEnabled = true;
        UIManager.Instance.ShowNotification("Depth of Field effect activated.");
        Debug.Log("Depth of Field effect activated.");
    }

    public void HaltFunction()
    {
        ResetDepthOfField();
        dofButton.FunctionActiveState(false);
        UIManager.Instance.ShowNotification("Depth of Field effect deactivated.");
        Debug.Log("Depth of Field effect deactivated.");
    }

    private void AdjustDepthOfField()
    {
        if (depthOfField != null)
        {
            artFrame.localPosition = new Vector3(artFrameOriginalPos.x + 0.4f, artFrameOriginalPos.y, artFrameOriginalPos.z);
            
            // Perform raycast to find nearest object in front the player
            if (Physics.Raycast(playerCamera.position, playerCamera.forward, out RaycastHit hit, maxFocusDistance))
            {
                float focusDistance = hit.distance;

                // Adjust depth of field parameters
                depthOfField.focusDistance.Override(focusDistance);
                depthOfField.focalLength.Override(120f);
            }
        }
    }

    public void ResetDepthOfField()
    {
        artFrame.localPosition = artFrameOriginalPos;

        if (depthOfField != null)
        {
            depthOfField.focusDistance.Override(1f);
            depthOfField.focalLength.Override(1f);
        }

        dofEnabled = false;
    }
}

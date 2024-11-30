using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DepthOfFieldToggle : MonoBehaviour
{
    public Volume postProcessingVolume; // Reference to the Post-Processing Volume
    private DepthOfField depthOfField;  // To store the Depth of Field effect
    private bool isDepthOfFieldActive = false; // Toggle state

    void Start()
    {
        // Get the Depth of Field effect from the volume
        if (postProcessingVolume.profile.TryGet<DepthOfField>(out depthOfField))
        {
            depthOfField.active = false; // Ensure it's initially disabled
        }
        else
        {
            Debug.LogError("Depth of Field effect not found in the Post-Processing Volume!");
        }
    }

    void OnMouseDown() // Detect click on the GameObject
    {
        if (depthOfField == null) return;

        // Toggle Depth of Field state
        isDepthOfFieldActive = !isDepthOfFieldActive;
        depthOfField.active = isDepthOfFieldActive;

        // Optional: Adjust settings dynamically (example values)
        if (isDepthOfFieldActive)
        {
            depthOfField.focusDistance.value = 5f; // Set focus distance
            depthOfField.aperture.value = 8f;     // Adjust aperture for blur
            depthOfField.focalLength.value = 50f; // Simulate camera lens focal length
        }
    }
}

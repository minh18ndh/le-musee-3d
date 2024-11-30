using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DepthOfField : MonoBehaviour
{
    [SerializeField] private Volume globalVolume;
    private UnityEngine.Rendering.Universal.DepthOfField depthOfField;

    void Start()
    {
        if (globalVolume != null && globalVolume.sharedProfile != null)
        {
            // Try to get the DepthOfField component from the profile
            if (globalVolume.sharedProfile.TryGet(out depthOfField))
            {
                Debug.Log("Depth of Field component found in the assigned Global Volume.");
            }
            else
            {
                Debug.LogError("Depth of Field component not found in the Global Volume profile.");
            }
        }
        else
        {
            Debug.LogError("Global Volume is not assigned or has no Volume Profile.");
        }
    }

    public void ExecuteFunction()
    {
        if (depthOfField != null)
        {
            depthOfField.focusDistance.Override(10f);
            depthOfField.focalLength.Override(200f);
            Debug.Log("Depth of Field effect activated.");
        }
        else
        {
            Debug.LogError("DepthOfField component is missing.");
        }
    }
}

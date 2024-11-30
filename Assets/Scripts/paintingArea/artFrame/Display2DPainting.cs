using UnityEditor;
using UnityEngine;

[ExecuteAlways] // Runs in Edit and Play mode
public class Display2DPainting : MonoBehaviour
{
    public Texture painting2D;  // Drag 2D painting here
    private Texture currentTexture; // Cache for performance
    [SerializeField] private Renderer canvas;

    void Start()
    {
        Apply2DPainting();
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        Apply2DPainting(); // Apply texture when updated in the Inspector
    }
#endif

    private void Apply2DPainting()
    {
        if (canvas == null)
        {
            Debug.LogWarning("Painting Renderer is not assigned in " + gameObject.name);
            return;
        }

        if (painting2D != null && currentTexture != painting2D)
        {
            currentTexture = painting2D; // Update cache

            if (!Application.isPlaying)
            {
                canvas.sharedMaterial.mainTexture = painting2D;  // Avoid material instance
            }
            else
            {
                canvas.material.mainTexture = painting2D;
            }
        }
    }
}

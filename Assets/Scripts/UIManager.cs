using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance; // Singleton pattern for global access

    public GameObject warningPanel;
    public Text warningText;        
    public float warningDuration = 3f; // How long warning display

    private void Awake()
    {
        // Ensure there's only one instance of UIManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

   public void ShowActiveFunctionWarning()
    {
        if (warningPanel != null && warningText != null)
        {
            warningPanel.SetActive(true);
            Invoke(nameof(HideWarning), warningDuration);
        }
    }

    private void HideWarning()
    {
        if (warningPanel != null)
        {
            warningPanel.SetActive(false);
        }
    }
}

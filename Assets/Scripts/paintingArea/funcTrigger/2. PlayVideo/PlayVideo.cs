using UnityEngine;

public class PlayVideo : MonoBehaviour
{
    private PlayVideoButtonClickHandler pvButton;
    [SerializeField] private GameObject TV;

    private void Start()
    {
        pvButton = GetComponent<PlayVideoButtonClickHandler>();
    }
    public void ExecuteFunction()
    {
        TV.SetActive(true);
        UIManager.Instance.ShowNotification("PlayVideo activated.");
        Debug.Log("PlayVideo executed!");
    }

    public void HaltFunction()
    {
        TV.SetActive(false);
        pvButton.FunctionActiveState(false);
        UIManager.Instance.ShowNotification("PlayVideo deactivated.");
        Debug.Log("PlayVideo deactivated.");
    }
}

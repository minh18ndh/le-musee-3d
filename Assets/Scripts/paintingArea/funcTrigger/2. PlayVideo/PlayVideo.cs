using UnityEngine;

public class PlayVideo : MonoBehaviour
{
    [SerializeField] private GameObject TV;
    public void ExecuteFunction()
    {
        TV.SetActive(true);
        Debug.Log("PlayVideo executed!");
    }

    public void HaltFunction()
    {
        TV.SetActive(false);
        Debug.Log("PlayVideo deactivated.");
    }
}

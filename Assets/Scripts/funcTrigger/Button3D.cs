using UnityEngine;

public class Button3D : MonoBehaviour
{
    private System.Action onClickAction; // Action to perform when clicked

    public void Setup(System.Action action)
    {
        onClickAction = action;
    }

    private void OnMouseDown()
    {
        // Call the assigned action when cube is clicked
        onClickAction?.Invoke();
    }
}

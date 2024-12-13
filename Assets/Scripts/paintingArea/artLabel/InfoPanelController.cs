using UnityEngine;

public class InfoPanelController : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    private Vector3 offset;
    private float zDistance;

    void OnMouseDown()
    {
        // Calculate distance from camera to panel
        zDistance = playerCamera.WorldToScreenPoint(transform.position).z;

        // Calculate offset between mouse position and object
        offset = transform.position - GetMouseWorldPos();
    }

    void OnMouseDrag()
    {
        // Update position of panel based on mouse position and offset
        transform.position = GetMouseWorldPos() + offset;
    }

    private Vector3 GetMouseWorldPos()
    {
        // Get mouse position in 3D space (with respect to camera's Z distance)
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zDistance;
        return playerCamera.ScreenToWorldPoint(mousePoint);
    }
}

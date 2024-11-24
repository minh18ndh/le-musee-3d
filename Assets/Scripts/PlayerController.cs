using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 0.05f;           // Movement speed
    public float flySpeed = 0.05f;            // Fly up/down speed
    public float rotationSpeed = 0.05f;       // Rotation speed for left/right
    public float pitchSpeed = 0.05f;          // Rotation speed for up/down (camera)

    public Transform cameraTransform;         // Reference to the camera's Transform
    private float pitch = 0f;                 // Track camera pitch (up/down)

    private void Update()
    {
        // Movement along X and Z axes (WASD)
        float moveX = Input.GetAxis("Horizontal") * moveSpeed;   // A/D or Left/Right
        float moveZ = Input.GetAxis("Vertical") * moveSpeed;     // W/S or Up/Down
        transform.Translate(new Vector3(moveX * Time.deltaTime, 0, moveZ * Time.deltaTime));

        // Fly up/down (Q/E)
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Translate(Vector3.up * flySpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Translate(Vector3.down * flySpeed * Time.deltaTime);
        }

        // Check if left mouse button is pressed
        if (Input.GetMouseButton(0))  // 0 == left mouse button
        {
            float rotateY = Input.GetAxis("Mouse X") * rotationSpeed * 100f;
            float rotateX = Input.GetAxis("Mouse Y") * pitchSpeed * 100f;

            // Rotate player to left/right with mouse (about Y-axis)
            transform.Rotate(0, rotateY, 0);

            // Rotate player's camera ("eyes") up/down with mouse (about X-axis)
            pitch -= rotateX;
            pitch = Mathf.Clamp(pitch, -50f, 40f);  // Limit up/down rotation to avoid flipping

            // Apply the pitch to camera
            cameraTransform.localRotation = Quaternion.Euler(pitch, 0, 0);
        }
    }
}

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;           // Movement speed
    public float flySpeed = 10f;            // Fly up/down speed
    public float rotationSpeed = 10f;       // Rotation speed for left/right
    public float pitchSpeed = 10f;          // Rotation speed for up/down (camera)

    public Transform cameraTransform;       // Reference to the camera's Transform
    private float pitch = 0f;               // Track camera pitch (up/down)

    private void Update()
    {
        // Movement along X and Z axes (WASD)
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;  // A/D or Left/Right
        float moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;    // W/S or Up/Down
        transform.Translate(new Vector3(moveX, 0, moveZ));

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
        if (Input.GetMouseButton(0))  // 0 = left mouse button
        {
            // Rotate player to left/right with mouse (about Y-axis)
            float rotateY = Input.GetAxis("Mouse X") * rotationSpeed * 500f * Time.deltaTime;
            transform.Rotate(0, rotateY, 0);

            // Rotate player's camera ("eyes") up/down with mouse (about X-axis)
            pitch -= Input.GetAxis("Mouse Y") * pitchSpeed * 500f * Time.deltaTime;
            pitch = Mathf.Clamp(pitch, -50f, 40f);  // Limit up/down rotation to avoid flipping

            // Apply the pitch to camera
            cameraTransform.localRotation = Quaternion.Euler(pitch, 0, 0);
            //Debug.Log("Camera Rotation in Quaternion format: " + cameraTransform.localRotation);
        }
    }
}

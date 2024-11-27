using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;            // Movement speed
    public float flySpeed = 3f;              // Fly up/down speed
    public float rotationSpeed = .05f;       // Rotation speed for left/right
    public float pitchSpeed = .05f;          // Rotation speed for up/down (camera)

    public Transform head;         	    // Reference to the camera's Transform
    private float pitch = 0f;                // Track camera pitch (up/down)
    
    public float bobSpeed = 10f;
    public float bobAmount = .08f;
    private Vector3 headOriginalPos;
    private float timer = 0;
    
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        headOriginalPos = head.localPosition;
    }

    private void Update()
    {
        // Get player input for movement (WASD)
        float moveX = Input.GetAxis("Horizontal");    // A/D or Left/Right
        float moveZ = Input.GetAxis("Vertical");      // W/S or Up/Down

        // Calculate movement direction based on the player's local orientation
        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;

        // Update velocity for movement
        Vector3 velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);
        rb.velocity = velocity;

        // Fly up/down (Q/E)
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.Space))
        {
            rb.velocity = new Vector3(rb.velocity.x, flySpeed, rb.velocity.z);  
        }
        if (Input.GetKey(KeyCode.E))
        {
            rb.velocity = new Vector3(rb.velocity.x, -flySpeed, rb.velocity.z);  
        }

        // Camera and player rotation (mouse input)
        if (Input.GetMouseButton(0))  // 0 == left mouse button
        {
            float rotateY = Input.GetAxis("Mouse X") * rotationSpeed * 100f;
            float rotateX = Input.GetAxis("Mouse Y") * pitchSpeed * 100f;

            // About Y-axis
            transform.Rotate(0, rotateY, 0);

            // Rotate player's camera ("eyes") (about X-axis)
            pitch -= rotateX;
            pitch = Mathf.Clamp(pitch, -50f, 40f);  // Limit up/down rotation to avoid flipping

            head.localRotation = Quaternion.Euler(pitch, 0, 0);
        }
        
        if ((rb.velocity.x != 0) || (rb.velocity.z != 0))
        {
            timer += Time.deltaTime * bobSpeed;
       
            // Applies HeadBob movement
            head.localPosition = new Vector3(headOriginalPos.x, headOriginalPos.y + Mathf.Sin(timer) * bobAmount, headOriginalPos.z);
        }
     }
}

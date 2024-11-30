using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;  
    public float flySpeed = 10f;      
    public float rotationSpeed = .05f;       // Rotation speed for left/right
    public float pitchSpeed = .05f;          // Rotation speed for up/down (camera)
    private float pitch = 0f;                // Track camera pitch (up/down)

    public Transform head;   
    
    public float bobSpeed = 10f;
    public float bobAmount = .08f;
    private float timer = 0;
    private Vector3 headOriginalPos;
    private bool verticalMovement;
    private bool onFoot;

    private Rigidbody rb;
    private AudioSource footsteps;

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        footsteps = GetComponent<AudioSource>();
        headOriginalPos = head.localPosition;
    }

    private void Update()
    {
        verticalMovement = Input.GetKeyDown(KeyCode.Space);

        Movement();

        View();
        
        HeadBob();
        
        Footsteps();
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        onFoot = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Floor")   // Set to false only when exit floor
        {
            onFoot = false;
        }
    }
    
    private void Movement()
    {
        float moveX = Input.GetAxis("Horizontal");    // A/D or Left/Right
        float moveZ = Input.GetAxis("Vertical");      // W/S or Up/Down

        // Calculate movement direction based on player's local orientation
        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;

        // Update velocity
        Vector3 velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);
        rb.velocity = velocity;

        // Fly up
        if (verticalMovement)
        {
            rb.velocity = new Vector3(rb.velocity.x, flySpeed, rb.velocity.z);  
        }
    }
    
    private void View()
    {
        if (Input.GetMouseButton(1))  // 0 == right mouse button
        {
            float rotateY = Input.GetAxis("Mouse X") * rotationSpeed * 100f;
            float rotateX = Input.GetAxis("Mouse Y") * pitchSpeed * 100f;

            // Rotate about Y-axis
            transform.Rotate(0, rotateY, 0);

            // Rotate player's camera ("eyes") (about X-axis)
            pitch -= rotateX;
            pitch = Mathf.Clamp(pitch, -50f, 40f);  // Limit up/down rotation to avoid flipping

            head.localRotation = Quaternion.Euler(pitch, 0, 0);
        }
    }
    
    private void HeadBob()
    {
        if (!verticalMovement)  // Jump => no head bob
        {
            if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && onFoot)  // Horizontal-plane movement detected and contact with ground
            {
                timer += Time.deltaTime * bobSpeed;
       
                // Apply HeadBob movement
                head.localPosition = new Vector3(headOriginalPos.x, headOriginalPos.y + Mathf.Sin(timer) * bobAmount, headOriginalPos.z);
            }
            else  // Return to original head position when stand still
            {
                if (head.localPosition.y == headOriginalPos.y)
                {
                    head.localPosition = new Vector3(headOriginalPos.x, headOriginalPos.y, headOriginalPos.z);
                }
                else
                {
                    // Gradually return to headOriginalPos.y
                    float smoothSpeed = 2f; 
                    head.localPosition = new Vector3(
                        headOriginalPos.x,
                        Mathf.Lerp(head.localPosition.y, headOriginalPos.y, Time.deltaTime * smoothSpeed),
                        headOriginalPos.z);
                }
            }
        }
        else
        {
            timer = 0;
        }
    }
    
    private void Footsteps()
    {
        if (!verticalMovement)  // Jump => no footsteps
        {
            if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && onFoot)  // Horizontal-plane movement detected and contact with ground
            {
                if (!footsteps.isPlaying)
                {
                    footsteps.Play();
                }
            }
            else
            {
                footsteps.Stop();
            }
        }
        else
        {
            footsteps.Stop();
        }
    }  
}

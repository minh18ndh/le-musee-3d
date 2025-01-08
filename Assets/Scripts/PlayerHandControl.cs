using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHandController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 90f;
    private Rigidbody rb;
    private bool isMoving = false;
    private string currentState = "NO_HAND";

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            return;
        }
        rb.freezeRotation = true;
        SocketConnector.OnHandTrackingDataReceived += HandleHandTracking;
    }

    private void OnDestroy()
    {
        SocketConnector.OnHandTrackingDataReceived -= HandleHandTracking;
    }

    private void HandleHandTracking(HandTrackingData data)
    {
        if (rb == null) return;

        if (currentState != data.State)
        {
            currentState = data.State;

            if (currentState == "NO_HAND")
            {
                Stop();
            }
        }
    }

    private void MoveForward()
    {
        isMoving = true;
        rb.AddForce(transform.forward * moveSpeed, ForceMode.VelocityChange);
    }

    private void RotateLeft()
    {
        transform.Rotate(0, -rotateSpeed * Time.deltaTime, 0);
    }

    private void RotateRight()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }

    private void Stop()
    {
        if (!isMoving) return;

        isMoving = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        switch (currentState)
        {
            case "LEFT_HAND":
                RotateLeft();
                break;
            case "RIGHT_HAND":
                RotateRight();
                break;
            case "BOTH_HANDS":
                MoveForward();
                break;
        }

        if (rb != null && rb.velocity.magnitude > moveSpeed)
        {
            rb.velocity = rb.velocity.normalized * moveSpeed;
        }
    }
}
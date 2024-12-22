using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotateBehavior : MonoBehaviour
{
    public float rotationSpeed = 30f;

    void Update()
    {
        // Rotate around the Y-axis
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public GameObject textBox;
    public float floatSpeed = 2f;
    public float floatAmount = 0.2f;

    private Vector3 originalPosition;
    private bool playerNearby = false;

    void Start()
    {
        if (textBox != null)
        {
            textBox.SetActive(false);
            originalPosition = textBox.transform.localPosition;
        }
        Debug.Log("NPCInteraction script initialized.");
    }

    void Update()
    {
        if (playerNearby && textBox != null)
        {
            float newY = originalPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatAmount;
            textBox.transform.localPosition = new Vector3(originalPosition.x, newY, originalPosition.z);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            if (textBox != null)
                textBox.SetActive(true);
            Debug.Log("Player entered NPC trigger area.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            if (textBox != null)
            {
                textBox.SetActive(false);
                textBox.transform.localPosition = originalPosition;
            }
            Debug.Log("Player exited NPC trigger area.");
        }
    }
}
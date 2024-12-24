using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject[] backgrounds;
    private int currentIndex = 0;
    private float changeDuration = 3f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(startSlider());
    }

    private IEnumerator startSlider(){
        yield return new WaitForSeconds(3);
        InvokeRepeating("ChangeBackground", 0f, changeDuration);
    }

    private void ChangeBackground()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].SetActive(i == currentIndex);
        }
        currentIndex = (currentIndex + 1) % backgrounds.Length;
    }
}
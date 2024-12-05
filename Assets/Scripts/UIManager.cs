using UnityEngine;
using TMPro;
using System.Collections;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance;       // Singleton pattern for global access

    public GameObject warningPanel;       
    public TMP_Text warningText;            
    private float warningDuration = 3f;      // How long the warning is displayed

    public GameObject Iphone6;

    public AudioSource backgroundMusicSource;
    public AudioClip[] musicFiles;          // Array to hold music pieces

    public GameObject[] lightObjects;
    private Light[] lights;

    private Coroutine discoCoroutine;   // To store the running Coroutine

    private void Awake()
    {
        // Ensure there's only one instance of UIManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        lights = new Light[lightObjects.Length];

        for (int i = 0; i < lightObjects.Length; i++)
        {
            lights[i] = lightObjects[i].GetComponentInChildren<Light>();
        }
    }

    public void ShowActiveFunctionWarning()
    {
        if (warningPanel != null)
        {
            warningPanel.SetActive(true);
            Invoke(nameof(HideWarning), warningDuration);
        }
    }

    private void HideWarning()
    {
        if (warningPanel != null)
        {
            warningPanel.SetActive(false);
        }
    }

    public void ToggleiPhone6()
    {
        if (Iphone6 != null)
        {
            bool isActive = Iphone6.activeSelf; // Check if the screen is currently active
            Iphone6.SetActive(!isActive);       // Toggle the active state
        }
    }

    public void PlayBackgroundMusic(int musicIndex)
    {
        if (backgroundMusicSource != null && musicFiles != null && musicIndex >= 0 && musicIndex < musicFiles.Length)
        {
            // Assign the selected music clip to the AudioSource
            backgroundMusicSource.clip = musicFiles[musicIndex];

            backgroundMusicSource.Play();
        }
        else
        {
            Debug.LogWarning("Invalid music index or AudioSource not set!");
        }
    }

    public void PauseBackgroundMusic()
    {
        backgroundMusicSource.Pause();
    }

    public void ResumeBackgroundMusic()
    {
        backgroundMusicSource.Play();
    }

    public void Mode1()
    {
        StopDiscoEffect();
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].color = Color.white;
        }
    }

    public void Mode2()
    {
        StopDiscoEffect();
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].color = new Color(0.906f, 0.678f, 0.223f);
        }
    }

    public void Mode3()
    {
        StopDiscoEffect();
        lights[0].color = Color.red;
        lights[1].color = Color.green;
        lights[2].color = Color.blue;
        lights[3].color = Color.yellow;
        lights[4].color = Color.magenta;
        lights[5].color = Color.cyan;
        lights[6].color = Color.white;
    }

    public void Mode4()
    {
        discoCoroutine = StartCoroutine(DiscoBlinkingEffect());
    }

    // Coroutine for the blinking and random color change effect
    private IEnumerator DiscoBlinkingEffect()
    {
        while (true)  
        {
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].color = new Color(Random.value, Random.value, Random.value);
            }

            float blinkDuration = 0.05f;
            yield return new WaitForSeconds(blinkDuration);
        }
    }

    private void StopDiscoEffect()
    {
        if (discoCoroutine != null)
        {
            StopCoroutine(discoCoroutine);
            discoCoroutine = null;
        }
    }
}

using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI; // Thêm namespace để dùng Slider

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;       // Singleton pattern for global access

    public GameObject notificationPanel;       
    public TMP_Text notificationText;            
    private float notificationDuration = 3f;      // How long the warning is displayed

    public GameObject Iphone6;

    public AudioSource backgroundMusicSource;
    public AudioClip[] musicFiles;          // Array to hold music pieces

    public Slider volumeSlider;             // Slider for adjusting volume

    public GameObject[] lightObjects;
    private Light[] lights;

    private Coroutine discoCoroutine;   // To store the running Coroutine

    //public GameObject colorBlindnessOption;

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

        // Set initial volume value if the slider is assigned
        if (volumeSlider != null && backgroundMusicSource != null)
        {
            volumeSlider.value = backgroundMusicSource.volume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    public void ShowNotification(string notiText)
    {
        if (notificationPanel != null)
        {
            CancelInvoke(nameof(HideNotification));

            notificationPanel.SetActive(true);
            notificationText.text = notiText;

            Invoke(nameof(HideNotification), notificationDuration);
        }
    }

    private void HideNotification()
    {
        if (notificationPanel != null)
        {
            notificationPanel.SetActive(false);
        }
    }

    public void ToggleiPhone6()
    {
        if (Iphone6 != null)
        {
            bool isActive = Iphone6.activeSelf; 
            Iphone6.SetActive(!isActive); 
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

    public void TogglePlayPauseMusic()
    {
        if (backgroundMusicSource != null)
        {
            if (backgroundMusicSource.isPlaying)
            {
                backgroundMusicSource.Pause(); // Nếu đang phát, tạm dừng
            }
            else
            {
                backgroundMusicSource.Play(); // Nếu đang tạm dừng, phát nhạc
            }
        }
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
        lights[7].color = Color.red;
        lights[8].color = Color.green;
        lights[9].color = Color.magenta;
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

            float blinkDuration = 0.1f;
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

    // Function to set the volume based on slider value
    public void SetVolume(float volume)
    {
        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.volume = volume;
        }
    }
    public GameObject popupPanel1; // Popup UI panel chứa hình ảnh và nút

    public void ShowPopup1()
    {
        if (popupPanel1 != null)
        {
            popupPanel1.SetActive(true); // Hiển thị popup
        }
    }

    public void HidePopup1()
    {
        if (popupPanel1 != null)
        {
            popupPanel1.SetActive(false); // Ẩn popup, quay lại màn hình chính
        }
    }


    public GameObject popupPanel2; // Popup UI panel chứa hình ảnh và nút

    public void ShowPopup2()
    {
        if (popupPanel2 != null)
        {
            popupPanel2.SetActive(true); // Hiển thị popup
        }
    }

    public void HidePopup2()
    {
        if (popupPanel2 != null)
        {
            popupPanel2.SetActive(false); // Ẩn popup, quay lại màn hình chính
        }
    }

}
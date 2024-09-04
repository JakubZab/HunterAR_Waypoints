using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject aboutMenu;
    public Slider volumeSlider;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        volumeSlider.value = PlayerPrefs.GetFloat("volume", 0.5f);
        audioSource.volume = volumeSlider.value;
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
        aboutMenu.SetActive(false);
    }

    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
    }

    public void OpenAbout()
    {
        settingsMenu.SetActive(false);
        aboutMenu.SetActive(true);
    }

    public void CloseAbout()
    {
        aboutMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("volume", volume);
    }
}

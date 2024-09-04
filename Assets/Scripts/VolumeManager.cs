using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public GameObject volumePanel;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    private AudioSource musicSource;
    private AudioSource sfxSource;

    void Start()
    {
        
        musicSource = GameObject.Find("MusicSource").GetComponent<AudioSource>();
                
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
              
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        
    }

    public void OpenVolumePanel()
    {
        volumePanel.SetActive(true);
    }

    public void CloseVolumePanel()
    {
        volumePanel.SetActive(false);
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
}

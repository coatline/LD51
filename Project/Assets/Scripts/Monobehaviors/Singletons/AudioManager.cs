using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] AudioMixerGroup masterAudioMixer;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioClip calmingMusic;
    [SerializeField] AudioClip gameMusic;
    float targetMusicVolume;
    float masterVolume;
    float musicVolume;
    float sfxVolume;

    void Start()
    {
        musicVolume = 1;
        SceneManager.activeSceneChanged += SceneChanged;
        SceneChanged(new Scene(), SceneManager.GetActiveScene());
    }

    void Update()
    {
        musicVolume = Mathf.Lerp(musicVolume, targetMusicVolume, Time.deltaTime * 3);
        masterAudioMixer.audioMixer.SetFloat("MusicVolume", musicVolume);
    }

    Slider masterVolumeSlider;
    Slider musicVolumeSlider;
    Slider sfxVolumeSlider;

    void SceneChanged(Scene current, Scene next)
    {
        // True parameter so that we can find them even when inactive
        Slider[] sliders = FindObjectsOfType<Slider>(true);
        foreach (Slider slider in sliders)
        {
            if (slider.name == "SfxVolumeSlider")
            {
                sfxVolumeSlider = slider;
                sfxVolumeSlider.onValueChanged.AddListener(SfxVolumeChanged);
                slider.value = AudioMixerRangeToPercent(sfxVolume);
            }
            else if (slider.name == "MusicVolumeSlider")
            {
                musicVolumeSlider = slider;
                musicVolumeSlider.onValueChanged.AddListener(MusicVolumeChanged);
                slider.value = AudioMixerRangeToPercent(targetMusicVolume);
            }
            else if (slider.name == "MasterVolumeSlider")
            {
                masterVolumeSlider = slider;
                masterVolumeSlider.onValueChanged.AddListener(MasterVolumeChanged);
                slider.value = AudioMixerRangeToPercent(masterVolume);
            }
        }

        if (next.name == "Menu")
            musicSource.clip = calmingMusic;
        else
            musicSource.clip = gameMusic;

        musicVolume = 0;
        masterAudioMixer.audioMixer.SetFloat("MusicVolume", 0);

        musicSource.Play();
    }

    void SfxVolumeChanged(float val)
    {
        sfxVolume = PercentToAudioMixerRange(val);
        masterAudioMixer.audioMixer.SetFloat("SfxVolume", sfxVolume);
    }

    void MusicVolumeChanged(float val)
    {
        targetMusicVolume = PercentToAudioMixerRange(val);
        //masterAudioMixer.audioMixer.SetFloat("MusicVolume", PercentToAudioMixerRange(val));
    }

    void MasterVolumeChanged(float val)
    {
        masterVolume = PercentToAudioMixerRange(val);
        masterAudioMixer.audioMixer.SetFloat("MasterVolume", masterVolume);
    }

    float PercentToAudioMixerRange(float percent) => (percent * 100) - 80;
    float AudioMixerRangeToPercent(float val) => ((val + 80) / 100);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;

    float sliderChangedValue;

    // Start is called before the first frame update
    void Start()
    {

        slider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        //slider.

    }

    public void SetLevel(float sliderValue)
    {
        sliderChangedValue = sliderValue;
        mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
        PlayerPrefs.SetInt("isVolumeChanged", 1);
    }

    /*
    public void UpdateSliderValue()
    {
        slider.value = PlayerPrefs.GetFloat("MusicVolume", sliderChangedValue);
    }


    void Update()
    {
        slider.value = PlayerPrefs.GetFloat("MusicVolume", sliderChangedValue);
    }
        */

}

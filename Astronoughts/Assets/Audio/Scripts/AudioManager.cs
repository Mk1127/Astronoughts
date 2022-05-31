//////////////////////////////////////////////////////
//Assignment/Lab/Project: Final Project
//Name: Julian Davis
//Section: (2022SU.SGD.289)
//Instructor: Amber Johnson
//////////////////////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    #region Variables
    public static AudioManager instance;
    public AudioFile[] audioFiles;
    public AudioSource source;
    public Button settings;

    public Animator sliderAnimator;
    public bool Open;
    public bool isHidden;
    public bool isStarted;

    public float musicVolume;
    public float sfxVolume;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    public AudioMixer audioMixer;
    #endregion

    // Use this for initialization
    void Awake()
    {
        sliderAnimator.SetBool("Open",false);

        isHidden = true;
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }

        instance.source.volume = 1f;
        instance.GetComponent<AudioSource>().Play();
    }


    void Start()
    {
        audioMixer.GetFloat("Music", out musicVolume);
        audioMixer.GetFloat("SFx",out sfxVolume);

        musicVolume = musicVolumeSlider.GetComponent<Slider>().value;
        sfxVolume =  sfxVolumeSlider.GetComponent<Slider>().value;

        Debug.Log("Current Music Volume : " + musicVolume);
        Debug.Log("Current Sound Volume : " + sfxVolume);
    }

    public void OnSettingsClick()
    {
        if(isHidden == true)
        {
            sliderAnimator.SetBool("Open",true);
            isHidden = false;
        }
        else
        {
            sliderAnimator.SetBool("Open",false);
            isHidden = true;
        }
    }

    public void SetMusicVolume(float musicVolume)
    {
        audioMixer.SetFloat("Music", musicVolume);
        //audioMixer.GetFloat("Music",out musicVolume);
        Debug.Log("Current Music Volume : " + musicVolume);
    }

    public void SetEffectsVolume(float sfxVolume)
    {
        audioMixer.SetFloat("SFx", sfxVolume);
        //audioMixer.GetFloat("SFx",out musicVolume);
        Debug.Log("Current Sound Volume : " + sfxVolume);
    }

}

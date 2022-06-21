﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

//[RequireComponent(typeof(AudioSource))]
[System.Serializable]

public class AudioManager : MonoBehaviour
{
    #region Variables
    public static AudioManager instance;
    public AudioFile[] audioFiles;
    public Texture playTexture;
    public Texture muteTexture;

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
            return;
        }
        //Persist this instance through level change.
        DontDestroyOnLoad(gameObject);
        source = gameObject.GetComponent<AudioSource>();

        instance.source.volume = 0.15f;
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

    #region Functions
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
    //OnGUI controls for players
    void OnGUI()
    {
        source = gameObject.GetComponent<AudioSource>();
        if(source.mute == false)
        {
            if(GUI.Button(new Rect(10,10,50,50),muteTexture))
            {
                gameObject.GetComponent<AudioSource>().mute = true;
            }
        }
        else if(source.mute == true)
        {
            if(GUI.Button(new Rect(10,10,50,50),playTexture))
            {
                gameObject.GetComponent<AudioSource>().mute = false;
            }
        }
    }

    public void SetMusicVolume(float musicVolume)
    {
        if(isHidden == false)
        {
            audioMixer.SetFloat("Music",musicVolume);
            //audioMixer.GetFloat("Music",out musicVolume);
            Debug.Log("Current Music Volume : " + musicVolume);
        }
    }

    public void SetEffectsVolume(float sfxVolume)
    {
        if(isHidden == false)
        {
            audioMixer.SetFloat("SFx",sfxVolume);
            //audioMixer.GetFloat("SFx",out musicVolume);
            Debug.Log("Current Sound Volume : " + sfxVolume);
        }
    }

    #endregion

}
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

    public Slider musicVolume;
    public Slider effectsVolume;
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

        instance.GetComponent<AudioSource>().Play();
        instance.source.volume = 1f;
    }
    void Start()
    {
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

    public void SetMusicVolume()
    {
        audioMixer.SetFloat("Music",Mathf.Log10(musicVolume.value) * 20);
    }

    public void SetEffectsVolume()
    {
        audioMixer.SetFloat("SFx",Mathf.Log10(effectsVolume.value) * 20);
    }

}

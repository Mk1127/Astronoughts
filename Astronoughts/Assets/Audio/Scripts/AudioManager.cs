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
    public Button settings;
    public Slider musicVolume;
    public Slider effectsVolume;
    public AudioMixer audioMixer;
    #endregion

    // Use this for initialization
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        //        DontDestroyOnLoad(gameObject);
        foreach(var s in audioFiles)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.audioClip;
            s.source.volume = s.volume;
            s.source.loop = s.isLooping;
            if(s.playOnAwake)
            {
                s.source.Play();
            }
        }

    }

    public void OnSettingsClick()
    {
    }

    public void SetMusicVolume()
    {
        audioMixer.SetFloat("Music",Mathf.Log10(musicVolume.value) * 20);
    }

    public void SetEffectsVolume()
    {
        audioMixer.SetFloat("SFX",Mathf.Log10(effectsVolume.value) * 20);
    }

}

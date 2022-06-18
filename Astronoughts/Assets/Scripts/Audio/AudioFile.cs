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

[System.Serializable]

public class AudioFile
{
    public string audioName;
    public AudioClip audioClip;

    [Range(0f,1f)]

    public float volume;

    [HideInInspector] public AudioSource source;

    public bool isLooping;
    public bool playOnAwake;
}

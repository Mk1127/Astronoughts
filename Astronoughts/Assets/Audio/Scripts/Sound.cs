using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class Sound : MonoBehaviour
{
	public string name;      //Store the name of our music/effect
	public AudioClip clip;  //Store the actual music/effect
	public AudioMixerGroup[] masterGroup;
	public AudioMixerGroup soundsGroup;
	public AudioMixerGroup musicGroup;

	[Range(0f, 1f)]     //limit the volume range
	public float volume = 0.75f;    //Store our volume

	[Range(0f, 1f)]
	public float volumeVariance = 1f;

	[Range(1f, 3f)]     //Limit the pitch Range
	public float pitch = 1f; //set the pitch for our music/effect

	[Range(0f, 1f)]
	public float pitchVariance = 1f;

	public bool loop = false; //should this sound loop

	[HideInInspector]
	public AudioSource source;

	public bool isLooping;
	public bool playOnAwake;
}

using UnityEditor;
using UnityEngine;

[System.Serializable]

public class AudioFile
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    public AudioSource source;

    public bool isLooping;
    public bool playOnAwake;
}
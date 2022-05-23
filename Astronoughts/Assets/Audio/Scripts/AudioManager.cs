using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

[System.Serializable]
public class AudioManager : MonoBehaviour
{
    #region VARIABLES
    public Sound[] sounds;
    public Sound[] playlist;
    public AudioSource source;
    private int currentPlayingIndex = 999;
    private bool shouldPlayMusic = false;
    public static AudioManager instance;
    private float mvol;
    private float evol;
    private float timeToReset;
    private bool timerIsSet = false;
    private string tmpName;
    private float tmpVol;
    private bool isLowered = false;
    private bool fadeOut = false;
    private bool fadeIn = false;
    private string fadeInUsedString;
    private string fadeOutUsedString;
    #endregion

    private void Start()
    {
    }

    // Use this for initialization

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        // get preferences
        mvol = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        evol = PlayerPrefs.GetFloat("EffectsVolume", 0.75f);

        createAudioSources(sounds, evol);     // create sources for effects
        createAudioSources(playlist, mvol);   // create sources for music
    }



    public void createAudioSources(Sound[] sounds, float volume)
    {
        foreach (var s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume * volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.isLooping;
            if (s.playOnAwake)
            {
                s.source.Play();
            }
        }

    }

    #region METHODS

    public static void PauseMusic(String name)
    {
        Sound s = Array.Find(instance.sounds, Sound => Sound.name == name);
        if (s == null)
        {
            Debug.LogError("Sound name" + name + "not found!");
            return;
        }
        else
        {
            s.source.Pause();
        }
    }

    public static void UnPauseMusic(String name)
    {
        Sound s = Array.Find(instance.sounds, Sound => Sound.name == name);
        if (s == null)
        {
            Debug.LogError("Sound name" + name + "not found!");
            return;
        }
        else
        {
            s.source.UnPause();
        }
    }
    public void PlaySound(string name)
    {
        // here we get the Sound from our array with the name passed in the methods parameters
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError("Unable to play sound " + name);
            return;
        }
        s.source.Play(); // play the sound
    }

    public void PlayMusic(string name)
    {
        if (shouldPlayMusic == false)
        {
            shouldPlayMusic = true;
            // pick a random song from our playlist
            currentPlayingIndex = UnityEngine.Random.Range(0, playlist.Length - 1);
            playlist[currentPlayingIndex].source.volume = playlist[0].volume * mvol; // set the volume
            playlist[currentPlayingIndex].source.Play(); // play it
        }
    }

    // stop music
    public void StopMusic()
    {
        if (shouldPlayMusic == true)
        {
            shouldPlayMusic = false;
            currentPlayingIndex = 999; // reset playlist counter
        }
    }


    public static void LowerVolume(String name, float _duration)
    {
        if (instance.isLowered == false)
        {
            Sound s = Array.Find(instance.sounds, Sound => Sound.name == name);
            if (s == null)
            {
                Debug.LogError("Sound name" + name + "not found!");
                return;
            }
            else
            {
                instance.tmpName = name;
                instance.tmpVol = s.volume;
                instance.timeToReset = Time.time + _duration;
                instance.timerIsSet = true;
                s.source.volume = s.source.volume / 3;
            }

            instance.isLowered = true;
        }
    }

    public static void FadeOut(String name, float duration)
    {
        instance.StartCoroutine(instance.IFadeOut(name, duration));
    }

    public static void FadeIn(String name, float targetVolume, float duration)
    {
        instance.StartCoroutine(instance.IFadeIn(name, targetVolume, duration));
    }

    //not for use

    private IEnumerator IFadeOut(String name, float duration)
    {
        Sound s = Array.Find(instance.sounds, Sound => Sound.name == name);
        if (s == null)
        {
            Debug.LogError("Sound name" + name + "not found!");
            yield return null;
        }
        else
        {
            if (fadeOut == false)
            {
                fadeOut = true;
                float startVol = s.source.volume;
                fadeOutUsedString = name;
                while (s.source.volume > 0)
                {
                    s.source.volume -= startVol * Time.deltaTime / duration;
                    yield return null;
                }

                s.source.Stop();
                yield return new WaitForSeconds(duration);
                fadeOut = false;
            }
            else
            {
                Debug.Log("Could not handle two fade outs at once : " + name + " , " + fadeOutUsedString + "! Stopped the music " + name);
                //StopMusic(name);
            }
        }
    }

    public IEnumerator IFadeIn(string name, float targetVolume, float duration)
    {
        Sound s = Array.Find(instance.sounds, Sound => Sound.name == name);
        if (s == null)
        {
            Debug.LogError("Sound name" + name + "not found!");
            yield return null;
        }
        else
        {
            if (fadeIn == false)
            {
                fadeIn = true;
                instance.fadeInUsedString = name;
                s.source.volume = 0f;
                s.source.Play();
                while (s.source.volume < targetVolume)
                {
                    s.source.volume += Time.deltaTime / duration;
                    yield return null;
                }

                yield return new WaitForSeconds(duration);
                fadeIn = false;
            }
            else
            {
                Debug.Log("Could not handle two fade ins at once: " + name + " , " + fadeInUsedString + "! Played the music " + name);
                //StopMusic(fadeInUsedString);
                PlayMusic(name);
            }
        }
    }

    void ResetVol()
    {
        Sound s = Array.Find(instance.sounds, Sound => Sound.name == tmpName);
        s.source.volume = tmpVol;
        isLowered = false;
    }

    private void Update()
    {
        if (Time.time >= timeToReset && timerIsSet)
        {
            ResetVol();
            timerIsSet = false;
            // if we are playing a track from the playlist && it has stopped playing
            if (currentPlayingIndex != 999 && !playlist[currentPlayingIndex].source.isPlaying)
            {
                currentPlayingIndex++; // set next index
                if (currentPlayingIndex >= playlist.Length)
                { //have we gone too high
                    currentPlayingIndex = 0; // reset list when max reached
                }
                playlist[currentPlayingIndex].source.Play(); // play that funky music
            }
        }
    }

    // get the song name
    public String getSongName()
    {
        return playlist[currentPlayingIndex].name;
    }

    // if the music volume change update all the audio sources
    public void musicVolumeChanged()
    {
        foreach (Sound m in playlist)
        {
            mvol = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
            m.source.volume = playlist[0].volume * mvol;
        }
    }

    //if the effects volume changed update the audio sources
    public void effectVolumeChanged()
    {
        evol = PlayerPrefs.GetFloat("EffectsVolume", 0.75f);
        foreach (Sound s in sounds)
        {
            s.source.volume = s.volume * evol;
        }
        sounds[0].source.Play(); // play an effect so user can hear effect volume
    }
    #endregion
}
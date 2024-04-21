using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("BG");
    }

    public void PlayMusic(string musicName)
    {
        Sound s = Array.Find(musicSounds, x => x.soundName == musicName);

        if (s == null) 
        {
            Debug.Log("Sound Not Found");
        }

        else 
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string SFXname)
    {
        Sound s = Array.Find(sfxSounds, x=> x.soundName == SFXname);
        if (s == null) 
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }
}

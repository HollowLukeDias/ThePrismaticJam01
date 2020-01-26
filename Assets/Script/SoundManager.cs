using System;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Sounds[] _sounds;
    public static SoundManager instance;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        foreach (var sound in _sounds)
        {
            sound.AudioSource = gameObject.AddComponent<AudioSource>();
            sound.AudioSource.clip = sound.Clip;
            sound.AudioSource.volume = sound.Volume;
            sound.AudioSource.pitch = sound.Pitch;
            sound.AudioSource.loop = sound.Loop;
        }
    }

    private void Start()
    {
        Play("Theme");
    }
    
    public void Play(string name)           //The method that actually plays the clip in any other script
    {
        Sounds s = Array.Find(_sounds, sound => sound.name == name); //Searches for a sound with the name passed as a parameter
        if (s == null)                                             //If it is not found, do nothing
        {
            Debug.Log("WrongName");
            return;
        }
        s.AudioSource.Play();           //Plays the clip with the audioSource
    }

    public void Stop(string name)
    {
        Sounds s = Array.Find(_sounds, sound => sound.name == name); //Searches for a sound with the name passed as a parameter
        if (s == null)                                             //If it is not found, do nothing
        {
            Debug.Log("WrongName");
            return;
        }
        s.AudioSource.Stop();           //Plays the clip with the audioSource
    }
}

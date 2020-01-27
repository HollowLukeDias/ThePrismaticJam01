using System;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Sounds[] _sounds;
    public static SoundManager Instance;

    #region Initialization
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance == null)
        {
            Instance = this;
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

    #endregion
    
    private void Start()
    {
        Play("Theme");
    }
    
    #region Play and Stop logic
    
    public void Play(string musicName)           //The method that actually plays the clip in any other script
    {
        Sounds s = Array.Find(_sounds, sound => sound.name == musicName); //Searches for a sound with the name passed as a parameter
        if (s == null)                                             //If it is not found, do nothing
        {
            Debug.Log("WrongName");
            return;
        }
        s.AudioSource.Play();           //Plays the clip with the audioSource
    }

    public void Stop(string musicName)
    {
        Sounds s = Array.Find(_sounds, sound => sound.name == musicName); //Searches for a sound with the name passed as a parameter
        if (s == null)                                             //If it is not found, do nothing
        {
            Debug.Log("WrongName");
            return;
        }
        s.AudioSource.Stop();           //Plays the clip with the audioSource
    }
    
    #endregion
}

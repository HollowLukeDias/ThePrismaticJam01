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
    
    /// <summary>
    /// Finds a sound inside the array of sounds in the Object and plays it
    /// </summary>
    /// <param name="soundName">The name of the sound it will search inside the array</param>
    public void Play(string soundName)
    {
        Sounds s = Array.Find(_sounds, sound => sound.name == soundName);
        if (s == null)
        {
            Debug.Log("WrongName");
            return;
        }
        s.AudioSource.Play();
    }

    /// <summary>
    /// Finds a sound inside the array of sounds in the Object and plays it
    /// </summary>
    /// <param name="soundName">The name of the sound it will search inside the array</param>
    public void Stop(string soundName)
    {
        Sounds s = Array.Find(_sounds, sound => sound.name == soundName);
        if (s == null)
        {
            Debug.Log("WrongName");
            return;
        }
        s.AudioSource.Stop();
    }
    
    #endregion
}

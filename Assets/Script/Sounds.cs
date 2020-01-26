using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sound")]
public class Sounds : ScriptableObject
{
        private AudioSource _audioSource;
        [SerializeField] private AudioClip _clip;
        [Range(0f, 1f)]  [SerializeField] private float _volume;
        [Range(.01f, 3f)][SerializeField] private float _pitch;
        [SerializeField] private string _name;
        [SerializeField] private bool _loop;

        public AudioSource AudioSource
        {
                get => _audioSource;
                set => _audioSource = value;
        }

        public AudioClip Clip => _clip;
        public float Volume => _volume;
        public float Pitch => _pitch;
        public string Name => _name;
        public bool Loop => _loop;
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private bool _closeWhenEnter;
    [SerializeField] private GameObject[] _doors;
    private float _waitTime = 0.5f;
    
    
    private void Update()
    {
        if (!_closeWhenEnter && Input.GetKeyDown(KeyCode.K))
        {
            foreach(var door in _doors)
            {
                door.SetActive(false);
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerEntrance();
        }
    }
    
    
    /// <summary>
    /// updates the camera's position and if it first time the player enters, enables the doors 
    /// </summary>
    private void PlayerEntrance(){
        CameraController.Instance.Target = transform;
        if (_closeWhenEnter)
        {
            while (_waitTime >= 0)
            {
                _waitTime -= Time.deltaTime;
            }
            foreach (var door in _doors)
            {
                door.SetActive(true);
            }
            _closeWhenEnter = false;
        }
    }


}

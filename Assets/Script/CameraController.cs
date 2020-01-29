 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [HideInInspector] public static CameraController Instance;
    [SerializeField] private float moveSpeed;
    private Transform _target;

    public Transform Target
    {
        get => _target;
        set => _target = value;
    }

    void Start()
    {
        Instance = this;
    }

    void Update()
    {
        if (_target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(_target.position.x, _target.position.y,transform.position.z),
                moveSpeed * Time.deltaTime);
        }
        
    }
    
}

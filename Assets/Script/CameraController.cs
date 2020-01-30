 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [HideInInspector] public static CameraController Instance;
    [SerializeField] private float _moveSpeed;
    private Transform _target;

    public Transform Target
    {
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
            MovesCamera();
        }
        
    }

    private void MovesCamera()
    {
        transform.position = Vector3.MoveTowards(transform.position,
            new Vector3(_target.position.x, _target.position.y, transform.position.z),
            _moveSpeed * Time.deltaTime);
    }

}

using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(FireBullet))]
public class PlayerInputHandler : MonoBehaviour
{
    #region Global Variables
    
    [SerializeField] private float _movementSpeed     = 1f;
    [SerializeField] private Transform _shootingPoint = null;
    [SerializeField] private GameObject _gunObject    = null;
    [SerializeField] private float _bulletAngleRandomnessFactor = 10f;
    private FireBullet _fireBullet;
    private Animator _animator;
    private Camera _main;
    private Command _keyW, _keyS, _keyA, _keyD, _keyNothing;
    private Rigidbody2D _rb2D;
    private Vector2 _moveInput;
    private Coroutine _firingCoroutine;
    
    #endregion

    #region Unity callbacks
    
    private void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        _main = Camera.main;
        _fireBullet = GetComponent<FireBullet>();
        
        _keyW = new MoveUp();
        _keyS = new MoveDown();
        _keyD = new MoveRight();
        _keyA = new MoveLeft();
        _keyNothing = new DoNothing();
    }

    private void Update()
    {
        HandleAim();
        HandleFireInput();
    }

    private void FixedUpdate()
    {
        HandleMovementInput();
    }

    #endregion
    
    #region Gun Aim & Fire
    
    private void HandleAim()
    {
        var angleQuaternion = CalculateAngle(false);
        _gunObject.transform.rotation = angleQuaternion;
    }
    private void HandleFireInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _firingCoroutine = StartCoroutine(HandleFire());
        } else if (Input.GetMouseButtonUp(0))
        {
            StopCoroutine(_firingCoroutine);
        }
    }

    private IEnumerator HandleFire()
    {
        var fireRate = _fireBullet.FireRate;
        while (true)
        {
            var angleQuaternion = CalculateAngle(true);
            FireBullet.FireOneBullet(angleQuaternion, _shootingPoint.transform.position);
            yield return new WaitForSeconds(1/fireRate);
        }
    }
    
    private Quaternion CalculateAngle(bool isShooting)
    {
        float randAngle = Random.Range(-_bulletAngleRandomnessFactor, _bulletAngleRandomnessFactor);
        var target = _main.ScreenToWorldPoint(Input.mousePosition);
        var direction = target - _shootingPoint.transform.position;
        var angleFloat = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (isShooting)
        {
            float fixedRandomAngle = angleFloat - 90f + randAngle;
            var angleQuaternion = Quaternion.Euler(0, 0, fixedRandomAngle);
            return angleQuaternion;
        }
        else
        {
            float fixedAngle = angleFloat - 45f;
            var angleQuaternion = Quaternion.Euler(0, 0, fixedAngle);
            return angleQuaternion;
        }
        
    }

    #endregion
    
    #region Character Movement
    private void HandleMovementInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _keyW.Execute(out _moveInput.y, _animator);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _keyS.Execute(out _moveInput.y, _animator);
        }
        else
        {
            _keyNothing.Execute(out _moveInput.y, _animator);
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            _keyD.Execute(out _moveInput.x, _animator);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _keyA.Execute(out _moveInput.x, _animator);
        }
        else
        {
            _keyNothing.Execute(out _moveInput.x, _animator);
        }
        
        ExecuteMovement();
    }

    private void ExecuteMovement()
    {
        float speedInFrame = _movementSpeed * Time.deltaTime;
        _rb2D.velocity = _moveInput.normalized * speedInFrame;
    }
    
    #endregion
}

using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(FireBullet))]
public class PlayerInputHandler : MonoBehaviour
{
    #region Global Variables
    
    [Header("Dash Setup")] 
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashLength;
    [SerializeField] private float _dashCooldown;
    private static bool isInvincible;
    private float _dashCounter, _dashCoolCounter;
    
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
    private Vector2 _lasMoveInput;
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
        var target = _main.ScreenToWorldPoint(Input.mousePosition);
        var angleQuaternion = CalculateAngle(target, +45);
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

    /// <summary>
    /// Finds the starting angle of the bullet
    ///<para>Calls on FireOneBullet to shoot the bullet from that angle and from the shooting point</para>
    ///<para>Deals with fire rate by setting it to be the number of bullets per second</para>
    /// </summary>
    /// <returns></returns>
    private IEnumerator HandleFire()
    {
        var fireRate = _fireBullet.FireRate;
        while (true)
        {
            var target = _main.ScreenToWorldPoint(Input.mousePosition);
            float randAngle = Random.Range(-_bulletAngleRandomnessFactor, _bulletAngleRandomnessFactor);
            var angleQuaternion = CalculateAngle(target, randAngle);
            FireBullet.FireOneBullet(angleQuaternion, _shootingPoint.transform.position);
            yield return new WaitForSeconds(1/fireRate);
        }
    }
    
    /// <summary>
    /// Calculates the angle between two points
    /// </summary>
    /// <param name="target">The point where the object wants to face</param>
    /// <param name="angleOffset">Offset for fixes and randomness (accuracy)</param>
    /// <returns></returns>
    private Quaternion CalculateAngle(Vector3 target, float angleOffset)
    {
        var direction = target - _shootingPoint.transform.position;
        var angleFloat = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float fixedAngle = angleFloat - 90;
        var angleQuaternion = Quaternion.Euler(0, 0, fixedAngle + angleOffset);
        return angleQuaternion;

    }

    #endregion
    
    #region Character Movement
    
    /// <summary>
    /// Just checks if the player has pressed any keys and deals with them accordingly
    /// </summary>
    private void HandleMovementInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _dashCoolCounter <= 0)
        {
            StartCoroutine(ExecuteDash());
            _dashCoolCounter = _dashCooldown;
        }
        else if(_dashCounter<=0)
        {
            isInvincible = false;
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
            _dashCoolCounter -= Time.deltaTime;
        }
        
    }

    private IEnumerator ExecuteDash()
    {
        _dashCounter = _dashLength;
        isInvincible = true;
        while (_dashCounter >= 0)
        {
            Debug.Log("HEEEEEEEERE!!!!");
            float speedInFrame = _dashSpeed * Time.deltaTime;
            _rb2D.velocity = _lasMoveInput.normalized * speedInFrame;
            _dashCounter -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    /// <summary>
    /// Normalizes the movement direction and multiplies by the speed it should have on that frame
    /// </summary>
    private void ExecuteMovement()
    {
        _lasMoveInput = _moveInput;
        float speedInFrame = _movementSpeed * Time.deltaTime;
        _rb2D.velocity = _moveInput.normalized * speedInFrame;
    }
    
    #endregion
}

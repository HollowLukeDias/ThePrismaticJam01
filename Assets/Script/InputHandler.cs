using System.Collections;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private GameObject gun;
    private FireBullet firing;
    private Animator anim;
    private Camera main;
    private Command KeyW, KeyS, KeyA, KeyD, KeyNothing;
    private float speedInFrame;
    private Rigidbody2D rb2D;
    private Vector2 moveInput;
    private Coroutine firingCoroutine;
    private bool firingCoroutineOn = false;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        main = Camera.main;
        firing = GetComponent<FireBullet>();
        
        KeyW = new MoveUp();
        KeyS = new MoveDown();
        KeyD = new MoveRight();
        KeyA = new MoveLeft();
        KeyNothing = new DoNothing();
    }

    private void Update()
    {
        HandleMovementInput();
        HandleAim();
        HandleFireInput();
    }

    private void HandleAim()
    {
        var target = main.ScreenToWorldPoint(Input.mousePosition);
        var direction = target - shootingPoint.transform.position ;
        var angleFloat = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        var angleQuaternion = Quaternion.Euler(0, 0, angleFloat - 45f);
        gun.transform.rotation = angleQuaternion;

    }
    private void HandleFireInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firingCoroutine = StartCoroutine(HandleFire());
        } else if (Input.GetMouseButtonUp(0))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    private IEnumerator HandleFire()
    {
        var fireRate = firing.FireRate;
        while (true)
        {
            var target = main.ScreenToWorldPoint(Input.mousePosition);
            var direction = target - shootingPoint.transform.position ;
            var angleFloat = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            var angleQuaternion = Quaternion.Euler(0, 0, angleFloat - 90f);
            firing.Fire(angleQuaternion, direction, shootingPoint.transform.position);
            yield return new WaitForSeconds(1/fireRate);
        }
    }
    
    private void HandleMovementInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            KeyW.Execute(out moveInput.y, anim);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            KeyS.Execute(out moveInput.y, anim);
        }
        else
        {
            KeyNothing.Execute(out moveInput.y, anim);
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            KeyD.Execute(out moveInput.x, anim);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            KeyA.Execute(out moveInput.x, anim);
        }
        else
        {
            KeyNothing.Execute(out moveInput.x, anim);
        }
        
        ExecuteMovement();
    }

    private void ExecuteMovement()
    {
        speedInFrame = movementSpeed * Time.deltaTime;
        rb2D.velocity = moveInput * speedInFrame;
    }
}

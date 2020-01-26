using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private GameObject actor;
    [SerializeField] private float speed;
    private Rigidbody2D rb2D;
    private Command KeyW, KeyS, KeyA, KeyD;
    private bool pressing = false;
    
    private void Start()
    {
        rb2D = actor.GetComponent<Rigidbody2D>();
        
        KeyW = new    MoveUp();
        KeyS = new  MoveDown();
        KeyA = new  MoveLeft();
        KeyD = new MoveRight();
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            KeyW.ExecuteMovement(rb2D, speed);
            pressing = true;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            KeyS.ExecuteMovement(rb2D, speed);
            pressing = true;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            KeyA.ExecuteMovement(rb2D, speed);
            pressing = true;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            KeyD.ExecuteMovement(rb2D, speed);
            pressing = true;
        }

        if (pressing == false)
        {
            rb2D.velocity = Vector2.zero;
        }
        pressing = false;
    }
}

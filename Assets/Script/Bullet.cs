using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 moveDirection;
    private Rigidbody2D rb2D;
    
    [SerializeField]
    private float moveSpeed = 0.1f;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        Invoke("DestroyBullet", 3f);
    }

    void Update()
    {
        var frameSpeed = moveSpeed * Time.timeScale;
        rb2D.velocity = moveDirection * frameSpeed;
    }

    public void SetMoveDirection(Vector2 dir) {
        moveDirection = dir.normalized;
    }
    
    private void DestroyBullet()
    {
        gameObject.SetActive(false);
    }

    public void Explode() {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}

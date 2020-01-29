using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [Range(3f, 6f)][SerializeField] private float _timeToDestroy = 3f;
    [SerializeField] private GameObject _explosionFX;
    private Vector2 _moveDirection;
    private Rigidbody2D _rb2D;
    private Coroutine _destroyAfterTime;
    private SpriteRenderer _spriteRenderer;
    
    [SerializeField] private float moveSpeed = 0.1f;

    #region Unity callbacks
    
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        HandleMovement();
        if (!_spriteRenderer.isVisible)
        {
            DestroyBullet();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Bullet") || other.CompareTag("Room"))
        {
            return;
        }
        Explode();
    }

    #endregion
    
    private void DestroyBullet()
    {
        gameObject.SetActive(false);
    }
    
    public void Explode() {
        Instantiate(_explosionFX, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }

    private void HandleMovement()
    {
        float frameSpeed = moveSpeed * Time.timeScale;
        _rb2D.velocity = transform.up * frameSpeed;
    }

}

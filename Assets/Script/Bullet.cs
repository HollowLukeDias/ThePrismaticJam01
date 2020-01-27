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
    
    [SerializeField] private float moveSpeed = 0.1f;

    #region Unity callbacks
    
    private void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Bullet"))
        {
            return;
        }
        Explode();
    }
    
    private void OnEnable()
    {
        _destroyAfterTime = StartCoroutine(DestroyAfterTime());
    }

    private void OnDisable()
    {
        StopCoroutine(DestroyAfterTime());
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
    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(_timeToDestroy);
        DestroyBullet();
    }

    private void HandleMovement()
    {
        float frameSpeed = moveSpeed * Time.timeScale;
        _rb2D.velocity = transform.up * frameSpeed;
    }

}

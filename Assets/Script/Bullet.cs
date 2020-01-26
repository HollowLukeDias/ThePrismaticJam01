using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 moveDirection;
    
    [SerializeField]
    private float moveSpeed = 0.1f;

    private void OnEnable()
    {
        Invoke("destroy", 3f);
    }

    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.timeScale);
    }

    public void setMoveDirection(Vector2 dir) {
        moveDirection = dir;
    }

    private void destroy()
    {
        gameObject.SetActive(false);
    }

    public void explode() {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}

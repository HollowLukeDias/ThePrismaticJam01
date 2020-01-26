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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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

    private void OnDisable()
    {
        CancelInvoke();
    }
}

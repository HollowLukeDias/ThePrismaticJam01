using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    [SerializeField]
    private int bulletsAmount = 1;

    private Vector2 bulletMoveDirection;

    [SerializeField]
    private float startTime, fireRate = 0.5f;

    private void Start()
    {
        startTime = 0f;
    }

    private void Fire()
    {
        GameObject bul = BulletPool.bulletPoolInstance.getBullet();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<Bullet>().setMoveDirection(Vector2.up);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && (Time.time - startTime) >= fireRate) {
            Fire();
            fireRate = Time.time;
        }
    }

}

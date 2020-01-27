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

    public float FireRate => fireRate;

    private void Start()
    {
        startTime = 0f;
    }
    
    

    public void Fire(Quaternion rotation, Vector3 direction, Vector3 shootingPoint)
    {
        GameObject bul = BulletPool.bulletPoolInstance.getBullet();
            bul.transform.position = shootingPoint;
            bul.transform.rotation = rotation; //Changed this so you can shooting using player!
            bul.SetActive(true);
            bul.GetComponent<Bullet>().SetMoveDirection(direction);
        }

}

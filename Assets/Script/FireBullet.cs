using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    [SerializeField]
    private int bulletsAmount = 1;

    private Vector2 bulletMoveDirection;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Fire", 0f, 2f);
    }

    private void Fire()
    {
        for (int i = 0; i < bulletsAmount; i++) {
            GameObject bul = BulletPool.bulletPoolIntance.getBullet();
                bul.transform.position = transform.position;
                bul.transform.rotation = transform.rotation;
                bul.SetActive(true);
                bul.GetComponent<Bullet>().setMoveDirection(Vector2.up);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

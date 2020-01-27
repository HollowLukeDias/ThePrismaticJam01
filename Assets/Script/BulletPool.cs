using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool BulletPoolInstance;

    [SerializeField] private GameObject _pooledBullet;
    [SerializeField] private int _initialBullets;
    [SerializeField] private int _bulletObjectsLimit = 100;
    private List<GameObject> _bullets;

    #region Unity Callbacks
    
    private void Awake()
    {
        if (BulletPoolInstance == null)
        {
            BulletPoolInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        _bullets = new List<GameObject>();
        CreateInitialBulletPool();
    }
    
    #endregion
    
    #region Auxiliay Methods
    
    private void SearchBullet(out GameObject bullet, out bool found)
    {
        bullet = null;
        found = false;
        for (int i = 0; i < _bullets.Count; i++) {
            if (!_bullets[i].activeInHierarchy) {
                bullet = _bullets[i];
                found = true;
            }
        }
    }

    private void CreateBullet()
    {
        var bullet = Instantiate(_pooledBullet, transform);
        bullet.SetActive(false);
        _bullets.Add(bullet);
    }

    private void CreateInitialBulletPool()
    {
        for (int i = 0; i < _initialBullets; i++)
        {
            CreateBullet();
        }
    }
    
    #endregion

    public void GetBullet(out GameObject bullet)
    {
        SearchBullet(out bullet, out bool found);
        if (!found && (_bullets.Count <= _bulletObjectsLimit))
        {
            CreateBullet();
            bullet = _bullets[_bullets.Count - 1];   
        }
    }
    
}
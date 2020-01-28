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
    
    /// <summary>
    /// Searches for a disabled (not currently in use) bullet inside the object pool
    /// </summary>
    /// <param name="bullet">The bullet to be returned, can be null if no disabled bullet is found</param>
    /// <param name="found">If it found a disabled bullet, CreateBullet is not called</param>
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

    /// <summary>
    /// Creates a bullet and adds it to the object pool for it to be used later
    /// </summary>
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

    /// <summary>
    /// Searches for a bullet that is not currently in use inside the object pool
    /// <para>If no bullet is found it creates another if the cap has not been reached</para>
    /// </summary>
    /// <param name="bullet">Returns the object bullet</param>
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _pooledObject;
    [SerializeField] private int _initialObjects;
    [SerializeField] private int _objectsLimit = 100;

    private List<GameObject> _objects;
    
    #region Unity Callbacks

    void Start()
    {
        _objects = new List<GameObject>();
        CreateInitialBulletPool();
    }
    
    #endregion
    
    #region Auxiliay Methods

    /// <summary>
    /// Searches for a disabled (not currently in use) objects inside the object pool
    /// </summary>
    /// <param name="objects">The objects to be returned, can be null if no disabled object is found</param>
    /// <param name="found">If it found a disabled object, CreateObject is not called</param>
    private void SearchBullet(out GameObject gameObject, out bool found)
    {
        gameObject = null;
        found = false;
        for (int i = 0; i < _objects.Count; i++) {
            if (!_objects[i].activeInHierarchy) {
                gameObject = _objects[i];
                found = true;
            }
        }
    }

    /// <summary>
    /// Creates an object and adds it to the object pool for it to be used later
    /// </summary>
    private void CreateBullet()
    {
        var gameObject = Instantiate(_pooledObject, transform);
        gameObject.SetActive(false);
        _objects.Add(gameObject);
    }
    
    private void CreateInitialBulletPool()
    {
        for (int i = 0; i < _initialObjects; i++)
        {
            CreateBullet();
        }
    }
    
    #endregion

    /// <summary>
    /// Searches for an object that is not currently in use inside the object pool
    /// <para>If no object is found it creates another if the cap has not been reached</para>
    /// </summary>
    /// <param name="bullet">Returns the object bullet</param>
    public void GetObject(out GameObject gameObject)
    {
        SearchBullet(out gameObject, out bool found);
        if (!found && (_objects.Count <= _objectsLimit))
        {
            CreateBullet();
            gameObject = _objects[_objects.Count - 1];   
        }
    }
}
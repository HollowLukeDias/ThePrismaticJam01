using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    [Range(0f, 60f)][SerializeField] private float _fireRate;
    public float FireRate => _fireRate;
    
    /// <summary>
    /// Activates One Bullet in the shooting point
    /// </summary>
    /// <param name="rotation">bullet initial facing direction</param>
    /// <param name="shootingPoint">from where the bullet is shot from</param>
    public static void FireOneBullet(Quaternion rotation, Vector3 shootingPoint)
    {
        BulletPool.BulletPoolInstance.GetBullet(out var bul);
        if (bul != null)
        {
            bul.transform.position = shootingPoint;
            bul.transform.rotation = rotation; //Changed this so you can shooting using player!
            bul.SetActive(true);
        }
        else
        {
            Debug.LogError("Error in Pool - Null Object");
        }
        
    }

}

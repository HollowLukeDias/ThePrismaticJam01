using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponentInParent<EnemyBehaviour>().distant = false;
    }
}

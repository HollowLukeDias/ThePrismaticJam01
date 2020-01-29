using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private int _hp;
    [SerializeField] private int _baseDamage;
    [SerializeField] private GameObject _hitBox;
    [SerializeField] private Rigidbody2D _rb2d;
    private bool _engage;
    private Transform _player;
    [SerializeField] private float _range;

    #region Unity Callbacks

    void Start()
    {
        _player = FindObjectOfType<PlayerInputHandler>().transform;
        _rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(_player.position, transform.position) > _range)
        {
            StartCoroutine(Relaxed());
        }
        else {
            _engage = true;
            _rb2d.velocity = Vector2.zero;
            StopCoroutine(Relaxed());
            StopCoroutine(StopWalk());
            Engage();
        }
    }
    #endregion

    #region Coroutines
    /// <summary>
    /// When the player is away
    /// </summary>
    /// <returns></returns>
    private IEnumerator Relaxed() {
        yield return new WaitForSeconds(3f);
        _rb2d.velocity = new Vector2(Random.Range(0f, 2f), Random.Range(0f, 2f));
        StartCoroutine(StopWalk());
    }
    /// <summary>
    /// Called inside Relaxed() to stop velocity
    /// </summary>
    /// <returns></returns>
    private IEnumerator StopWalk() {
        yield return new WaitForSeconds(0.2f);
        _rb2d.velocity = Vector2.zero;
    }
    #endregion
    public void receiveDamage(int damage) {
        _hp -= damage;
    }
    #region Abstract Methods
    /// <summary>
    /// defines de battle behaviour
    /// </summary>
    public abstract void Engage();

    /// <summary>
    /// defines action for meelee attack
    /// </summary>
    /// <param name="damage"></param>
    public abstract void BasicAttack(int damage);

    public abstract void Die();
    #endregion
}



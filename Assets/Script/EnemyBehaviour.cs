using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] protected int _hp;
                        public int hp { get { return _hp; } }
    [SerializeField] protected int _baseDamage;
    [SerializeField] protected GameObject _hitBox;
    [SerializeField] protected Rigidbody2D _rb2d;
    protected bool _engage;
    public bool distant;
    protected Transform _player;
    [SerializeField] protected float _range;
    protected Coroutine _coroutine;

    #region Coroutines
    /// <summary>
    /// When the player is away
    /// </summary>
    /// <returns></returns>
    protected IEnumerator Relaxed() {
        yield return new WaitForSeconds(3f);
        _rb2d.velocity = new Vector2(Random.Range(0f, 2f), Random.Range(0f, 2f));
        StartCoroutine(StopWalk());
    }
    
    /// <summary>
    /// Called inside Relaxed() to stop velocity
    /// </summary>
    /// <returns></returns>
    protected IEnumerator StopWalk() {
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
    public abstract int BasicAttack(int damage, int characterHP);

    public abstract void Die();
    
    #endregion
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] protected int Hp;
                        public int hp { get { return Hp; } }
    [SerializeField] protected int BaseDamage;
    [SerializeField] protected Rigidbody2D Rb2d;
    [SerializeField] protected BoxCollider2D HitBox;
    protected bool IsEngaged;
    public bool distant;
    [SerializeField] protected Transform Player;
    [SerializeField] protected float Range;
    protected Coroutine Coroutine;
    protected bool IsRelaxed = false;

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
    /*protected IEnumerator Relaxed() {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);
            Debug.Log("Relax");
            Rb2d.velocity = new Vector2(Random.Range(0f, 2f), Random.Range(0f, 2f));
            //yield return new WaitForSeconds(0.2f);
            Rb2d.velocity = Vector2.zero;
        }
    }
    */
    #endregion
    public void ReceiveDamage(int damage) {
        Hp -= damage;
    }
    #region Abstract Methods
    /// <summary>
    /// defines de battle behaviour
    /// </summary>
    public abstract void Engagement();

    /// <summary>
    /// defines action for meelee attack
    /// </summary>
    /// <param name="damage"></param>
    public abstract int BasicAttack(int damage, int characterHP);

    public abstract void Die();
    
    #endregion
}



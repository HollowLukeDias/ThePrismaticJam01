using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonEnemy : EnemyBehaviour
{
    [SerializeField] private float fireRate, attackRate;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerInputHandler>().transform;
        _rb2d = GetComponent<Rigidbody2D>();
        _hitBox = GetComponentInChildren<Collider2D>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(_player.position, transform.position) > _range)
        {
            StopAllCoroutines();
            StartCoroutine(Relaxed());
        }
        else if(!_engage)
        {
            _engage = true;
            _rb2d.velocity = Vector2.zero;
            transform.LookAt(_player.position);
            Engage();
        }
    }
    
    public override int BasicAttack(int damage, int characterHP)
    {
        //Set attack animation
        characterHP -= damage;
        Debug.Log("Atacando");
        Debug.Log("Current HP: " + characterHP);
        return characterHP;
    }

    public override void Die()
    {
        Debug.Log("Dead");
    }

    public override void Engage()
    {
        _coroutine = StartCoroutine(Attack(distant));
    }

    private IEnumerator Attack(bool distant) {
        do
        {
            PlayerInputHandler.hp = BasicAttack(_baseDamage, PlayerInputHandler.hp);
            yield return new WaitForSeconds(attackRate);
        } while (!distant);

        do
        {
            Debug.Log("Atirando");
            yield return new WaitForSeconds(fireRate);
        } while (distant);
    }
}

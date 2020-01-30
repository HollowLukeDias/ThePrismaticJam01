using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonEnemy : EnemyBehaviour
{
    [SerializeField] private float fireRate, attackRate;

    Vector3 offset;
    float sqrDistance;

    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<PlayerInputHandler>().transform;
        Rb2d = GetComponent<Rigidbody2D>();
        HitBox = GetComponentInChildren<BoxCollider2D>();
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {   
        offset = Player.position - transform.position;
        sqrDistance = offset.sqrMagnitude;

        if ( sqrDistance > (Range * Range))
        {
            StopAllCoroutines();
            if (!IsRelaxed)
            {
                Coroutine = StartCoroutine(Relaxed());
                IsRelaxed = true;
                Debug.Log("Here");
            }
        }
        else if(!IsEngaged)
        {
            IsEngaged = true;
            Rb2d.velocity = Vector2.zero;
            transform.LookAt(Player.position);
            Engagement();
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

    public override void Engagement()
    {
        IsRelaxed = false;
        Coroutine = StartCoroutine(Attack(distant));
    }

    private IEnumerator Attack(bool distant) {
        do
        {
            PlayerInputHandler.hp = BasicAttack(BaseDamage, PlayerInputHandler.hp);
            yield return new WaitForSeconds(attackRate);
        } while (!distant);

        do
        {
            Debug.Log("Atirando");
            yield return new WaitForSeconds(fireRate);
        } while (distant);
    }
    private IEnumerator Relaxed()
    {
        while (true)
        {
            //yield return new WaitForSeconds(.3f);
            Debug.Log("Relax");
            Rb2d.velocity = new Vector2(Random.Range(0f, 2f), Random.Range(0f, 2f));
            yield return new WaitForSeconds(0.2f);
            Rb2d.velocity = Vector2.zero;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public Animator anim;
    public Rigidbody2D rb;

    public int health;
    public int attackDamage;
    public float moveSpeed;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void Update()
    {
        //all enemies always move towards player at all times and flip sprite based on walking direction
        if (transform.position.x < player.position.x)
        {
            //facing right
            transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (transform.position.x > player.position.x)
        {
            //facing left
            transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = false;
        }

        if(!GetComponent<BoxCollider2D>().IsTouching(player.GetChild(2).GetComponent<BoxCollider2D>()) && Vector3.Distance(transform.position, player.position) < 6 && !player.GetComponent<PlayerController>().dead)
        {
            //if player isn't dead and enemy isn't touching player, but player is in range of enemy, move towards player by setting walking bool

            anim.SetBool("walking", true);
        }
        else
        {
            anim.SetBool("walking", false);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.zero; //reset velocity so enemy doesn't slide around

        if (anim.GetBool("walking"))
        {
            Vector3 moveDir = (player.position - transform.position).normalized;
            rb.velocity = moveDir * moveSpeed * 10 * Time.deltaTime;
        }
    }

    public virtual void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "PlayerHitbox")
        {
            Attack(collision.transform.parent.gameObject);
        }
    }

    public virtual void Attack(GameObject player)
    {
        player.GetComponent<PlayerController>().Damaged(attackDamage); //damage player based on attack damage
        var knockbackDir = transform.position - player.transform.position;
        player.transform.position -= knockbackDir.normalized * 0.3f;
    }

    public virtual void Damaged(int damageAmount)
    {
        health -= damageAmount;

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

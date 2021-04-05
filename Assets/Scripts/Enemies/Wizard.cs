using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Enemy
{
    [SerializeField] private GameObject magicBall;

    private float attackCooldown = 0;

    public override void Update()
    {
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

        if (!player.GetComponent<PlayerController>().dead && Vector3.Distance(transform.position, player.position) < 6)
        {
            if (agroDelay <= 0)
            {
                agroDelay = 0;
                if (Vector3.Distance(transform.position, player.position) > 2.5f)
                {
                    //if not in range, move towards player
                    anim.SetBool("walking", true);
                }
                else
                {
                    //if in range, attack player
                    Attack(player.gameObject);
                    anim.SetBool("walking", false);
                }
            }
            else
            {
                //else decrease agroDelay
                agroDelay -= Time.deltaTime;
            }
        }
        else
        {
            anim.SetBool("walking", false);
        }

        if (knockedBack)
        {
            knockbackTimer -= Time.deltaTime;

            if (knockbackTimer <= 0)
            {
                knockbackTimer = 0.1f;
                knockedBack = false;
            }
        }

        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
    }

    public override void OnTriggerStay2D(Collider2D collision)
    {
        //this will be left blank since wizard doesn't attack when touching player
    }

    public override void Attack(GameObject player)
    {
        //wizard enemy checks if cooldown is 0 before attacking
        if (attackCooldown <= 0)
        {
            //instead of directly attacking player, wizard spawns magic projectile and sends it at player's current position
            var newMagicBall = Instantiate(magicBall, transform.position, Quaternion.identity);
            newMagicBall.GetComponent<MagicBall>().targetPosition = player.transform.position;
            newMagicBall.GetComponent<MagicBall>().attackDamage = attackDamage;

            SoundEffectManager.instance.WizardShootSFX();

            //if attack is successful, attack cooldown is reset
            attackCooldown = 2f;
        }
    }
}

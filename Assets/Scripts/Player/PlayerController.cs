using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject HUD;
    [SerializeField] private Image healthBarFill;
    [SerializeField] private GameObject keyLight;
    [SerializeField] private ParticleSystem damageParticles;

    [Header("Player Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private int attackDamage;

    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Animator anim;

    private bool canMove = true;
    public int health = 100;

    public bool hasEscapeKey = false; //whether player has collected escape key drop and can open final escape door
    public bool dead = false;
    public bool won = false;

    public bool knockedBack = false;
    private float knockbackTimer = 0.1f;

    private void Start()
    {
        sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canMove)
        {
            //when player attacks
            canMove = false;
            anim.SetTrigger("attack");

            //first setup location of where hit check will occur
            Vector3 hitCheckPos;
            if (sr.flipX) { hitCheckPos = transform.position + new Vector3(0.3f, 0, 0); }
            else { hitCheckPos = transform.position + new Vector3(-0.3f, 0, 0); }

            //next check for enemies at location and loop through them to damage each one
            Collider2D[] hits = Physics2D.OverlapCircleAll(hitCheckPos, 0.2f);
            foreach(Collider2D hit in hits)
            {
                if(hit.tag == "EnemyHitbox")
                {
                    hit.GetComponent<Enemy>().Damaged(attackDamage);

                    ScreenShake.screenShakeInstance.ShakeScreen(0.3f, 0.05f, 7); //shake screen when enemy is hit
                    var knockbackDir = transform.position - hit.transform.position;

                    hit.GetComponent<Rigidbody2D>().AddForce(-knockbackDir.normalized * 150);
                    hit.GetComponent<Enemy>().knockedBack = true;
                }
            }
        }

        healthBarFill.fillAmount = health / 100f;

        if (knockedBack)
        {
            knockbackTimer -= Time.deltaTime;

            if (knockbackTimer <= 0)
            {
                knockbackTimer = 0.1f;
                knockedBack = false;
            }
        }
    }

    public void Damaged(int damageAmount)
    {
        health -= damageAmount;
        ScreenShake.screenShakeInstance.ShakeScreen(0.35f, 0.07f, 7); //shake screen when damage is taken

        if (health <= 0)
        {
            health = 0;

            HUD.GetComponent<Animator>().SetTrigger("youDied");

            dead = true;
            canMove = false;

            anim.SetBool("walking", false);
            anim.SetTrigger("dead");
            SoundEffectManager.instance.PlayerDeathSFX();
        }
        else
        {
            SoundEffectManager.instance.DamageSFX();
            damageParticles.Play();
        }
    }

    //this is animation event for letting player move after attacking
    private void DoneAttacking()
    {
        if (!dead)
        {
            canMove = true;
        }
    }

    private void FixedUpdate()
    {
        //rb.velocity = Vector2.zero; //reset velocity so player doesn't slide around

        if (canMove)
        {
            float moveX = Input.GetAxis("Horizontal") * moveSpeed * 10 * Time.deltaTime;
            float moveY = Input.GetAxis("Vertical") * moveSpeed * 10 * Time.deltaTime;

            if(new Vector2(moveX, moveY) != Vector2.zero)
            {
                //player is moving

                anim.SetBool("walking", true);

                if (!knockedBack)
                {
                    rb.velocity = new Vector2(moveX, moveY);
                }

                if(moveX > 0)
                {
                    sr.flipX = true;
                }
                else if(moveX < 0)
                {
                    sr.flipX = false;
                }
            }
            else
            {
                //player isn't moving

                anim.SetBool("walking", false);
                rb.velocity = Vector2.MoveTowards(rb.velocity, Vector2.zero, Time.deltaTime * 4);
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Key":
                Destroy(collision.gameObject); //delete key
                keyLight.SetActive(true); //turn on key light for player
                hasEscapeKey = true; //set bool since player has key now
                SoundEffectManager.instance.KeyCollectSFX();
                break;
            case "Escape":
                if (hasEscapeKey)
                {
                    HUD.GetComponent<Animator>().SetTrigger("youWon");
                    dead = true; //mark player as dead so enemies stop following
                    canMove = false;
                    won = true;
                    anim.SetBool("walking", false);
                    SoundEffectManager.instance.WinSFX();
                }
                break;
            default:
                break;
        }
    }
}

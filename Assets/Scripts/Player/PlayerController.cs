using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject keyLight;
    [SerializeField] private float moveSpeed;

    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Animator anim;

    private bool canMove = true; //<--this may be public in the future so other scripts can stop player movement

    public bool hasEscapeKey = false; //whether player has collected escape key drop and can open final escape door

    private void Start()
    {
        sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //when player attacks

            canMove = false;
            anim.SetTrigger("attack");
        }
    }

    //this is animation event for letting player move after attacking
    private void DoneAttacking()
    {
        canMove = true;
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.zero; //reset velocity so player doesn't slide around

        if (canMove)
        {
            float moveX = Input.GetAxis("Horizontal") * moveSpeed * 10 * Time.deltaTime;
            float moveY = Input.GetAxis("Vertical") * moveSpeed * 10 * Time.deltaTime;

            if(new Vector2(moveX, moveY) != Vector2.zero)
            {
                //player is moving

                anim.SetBool("walking", true);
                rb.velocity = new Vector2(moveX, moveY);

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
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Key")
        {
            Destroy(collision.gameObject); //delete key
            keyLight.SetActive(true); //turn on key light for player
            hasEscapeKey = true; //set bool since player has key now
        }
    }
}

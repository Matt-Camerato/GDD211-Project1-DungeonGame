using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;

    public int health;
    public int attackDamage;
    public float moveSpeed;

    public virtual void Update()
    {
        //all enemies always move towards player at all times and flip sprite based on walking direction
        if (transform.position.x < player.position.x)
        {
            //facing right
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (transform.position.x > player.position.x)
        {
            //facing left
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }
}

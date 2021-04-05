using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPad : MonoBehaviour
{
    private float healCooldown = 0.3f;

    private void Update()
    {
        if (healCooldown > 0)
        {
            healCooldown -= Time.deltaTime;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && healCooldown <= 0 && !collision.transform.parent.GetComponent<PlayerController>().dead)
        {
            var pc = collision.transform.parent.GetComponent<PlayerController>();
            pc.health += 2;
            healCooldown = 0.3f;
            if(pc.health > 100)
            {
                pc.health = 100;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghoul : Enemy
{
    private float attackCooldown = 0;

    public override void Update()
    {
        base.Update(); //<--enemy moves toward player

        //additionaly, ghoul has to handle attack cooldown
        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
    }

    public override void Attack(GameObject player)
    {
        //ghoul checks if cooldown is 0 before attacking
        if (attackCooldown <= 0)
        {
            base.Attack(player); //<--attacks player with given attack damage

            //if attack is successful, attack cooldown is reset
            attackCooldown = 1.5f;
        }
    }
}

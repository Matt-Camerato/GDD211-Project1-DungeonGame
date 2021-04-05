using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thral : Enemy
{
    public override void Attack(GameObject player)
    {
        base.Attack(player);

        Destroy(gameObject); //Thral enemy dies upon attacking
        SoundEffectManager.instance.EnemyKilledSFX();
    }
}

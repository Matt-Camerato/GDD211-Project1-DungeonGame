using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour
{
    public Vector3 targetPosition;
    public int attackDamage;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime); //<--ball moves towards target position when spawned
        if (transform.position == targetPosition)
        {
            Destroy(gameObject); //if ball gets to target position without hitting player, it is destroyed
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerHitbox")
        {
            collision.transform.parent.GetComponent<PlayerController>().Damaged(attackDamage);
            var knockbackDir = transform.position - collision.transform.parent.position;
            collision.transform.parent.GetComponent<Rigidbody2D>().AddForce(-knockbackDir.normalized * 100);
            collision.transform.parent.GetComponent<PlayerController>().knockedBack = true;

            Destroy(gameObject);
        }
    }
}

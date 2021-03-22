using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private bool canMove = true; //<--this may be public in the future so other scripts can stop player movement

    private void Update()
    {
        if (canMove)
        {
            float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
            float moveY = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

            transform.position += new Vector3(moveX, moveY, 0);
        }
    }
}

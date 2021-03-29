using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private char doorDirection;

    public bool locked = true;

    public void UnlockDoor()
    {
        locked = false;
        transform.GetChild(0).gameObject.SetActive(false); //turn off locked door collider
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !locked)
        {
            switch (doorDirection)
            {
                case 'N':
                    collision.transform.parent.position += new Vector3(0, 7.7f, 0);
                    break;
                case 'E':
                    collision.transform.parent.position += new Vector3(6.7f, 0, 0);
                    break;
                case 'S':
                    collision.transform.parent.position += new Vector3(0, -7.7f, 0);
                    break;
                case 'W':
                    collision.transform.parent.position += new Vector3(-6.7f, 0, 0);
                    break;
                default:
                    Debug.LogError("Error: doorDirection char is not set to one of the four cardinal directions");
                    break;
            }
        }
    }
}

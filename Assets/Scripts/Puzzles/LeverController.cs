using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    private bool leverUp;
    private SpriteRenderer thisLever;
    public Sprite LeverUp;
    public Sprite LeverDown;
    // Start is called before the first frame update
    void Start()
    {
        leverUp = false;
        thisLever = gameObject.GetComponent<SpriteRenderer>();
        thisLever.sprite = LeverDown;
    }

    private void Update()
    {
        if (leverUp)
        {
            thisLever.sprite = LeverUp;
        }
        else
        {
            thisLever.sprite = LeverDown;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerCharacter"))
        {
            if (leverUp)
            {
                leverUp = false;
            }
            else
            {
                leverUp = true;
            }
        }
    }

    public bool getState()
    {
        return leverUp;
    }

    public void setState(bool given)
    {
        leverUp = given;
    }
}
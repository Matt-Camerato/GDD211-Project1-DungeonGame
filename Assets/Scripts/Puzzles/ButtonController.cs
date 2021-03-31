using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private bool depressed;
    private SpriteRenderer thisButton;
    public Sprite buttonIn;
    public Sprite buttonOut;
    
    void Start()
    {
        depressed = false;
        thisButton = gameObject.GetComponent<SpriteRenderer>();
        thisButton.sprite = buttonOut;
    }

    private void Update()
    {
        if(depressed)
        {
            thisButton.sprite = buttonIn;
        }
        else
        {
            thisButton.sprite = buttonOut;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerCharacter"))
        {
            if (depressed)
            {
                depressed = false;
            }
            else
            {
                depressed = true;
            }
        }
    }

    public bool getState()
    {
        return depressed;
    }

    public void setState(bool given)
    {
        depressed = given;
    }

    public void toggleState()
    {
        if (depressed)
        {
            depressed = false;
        }
        else
        {
            depressed = true;
        }
    }
}
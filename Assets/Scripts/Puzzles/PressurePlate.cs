using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    private bool depressed = false;
    private SpriteRenderer thisPlate;
    public Sprite plateIn;
    public Sprite plateOut;

    private void Update()
    {
        if (thisPlate != null)
        {
            if (depressed)
            {
                thisPlate.sprite = plateIn;
            }
            else
            {
                thisPlate.sprite = plateOut;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Plate"))
        {
            thisPlate = collision.gameObject.GetComponent<SpriteRenderer>();
            depressed = true;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

            SoundEffectManager.instance.PressurePlateSFX();
        }
    }

    public bool getState()
    {
        return depressed;
    }
}
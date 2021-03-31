using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    private bool depressed;
    private SpriteRenderer thisPlate;
    public Sprite plateIn;
    public Sprite plateOut;
    // Start is called before the first frame update
    void Start()
    {
        depressed = false;
    }

    private void Update()
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Plate"))
        {
            thisPlate = collision.gameObject.GetComponent<SpriteRenderer>();
            depressed = true;
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    public bool getState()
    {
        return depressed;
    }
}
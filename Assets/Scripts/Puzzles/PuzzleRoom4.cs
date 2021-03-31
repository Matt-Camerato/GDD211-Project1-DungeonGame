using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PuzzleRoom4 : MonoBehaviour
{
    public PressurePlate pp;
    public ButtonController button;
    public LeverController lever;
    public RoomManager rm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pp.getState() == true && lever.getState() == true && button.getState() == true)
        {
            rm.completeRoom();
            Debug.Log("room4");
        }
        else if (pp.getState() == false)
        {
            lever.setState(false);
            button.setState(false);
        }
        else if (lever.getState() == false && pp.getState() == true)
        {
            button.setState(false);
        }
    }
}

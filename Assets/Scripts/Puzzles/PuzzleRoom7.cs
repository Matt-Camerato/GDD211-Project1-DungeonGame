using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleRoom7 : MonoBehaviour
{
    public RoomManager rm;
    public ButtonController b1;
    public ButtonController b2;
    public ButtonController b3;
    public ButtonController b4;

    // Update is called once per frame
    void Update()
    {
        if(b1.getState() == true && b2.getState() == true && b3.getState() == false && b4.getState() == false)
        {
            rm.completeRoom();
        }
    }
}

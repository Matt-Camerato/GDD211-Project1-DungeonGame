using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleRoom9 : MonoBehaviour
{
    public RoomManager rm;
    public ButtonController b1;
    public ButtonController b2;
    public ButtonController b3;
    public ButtonController b4;
    public ButtonController b5;
    public ButtonController b6;
    public ButtonController b7;

    // Update is called once per frame
    void Update()
    {
        if (b1.getState() == true && b2.getState() == true && b3.getState() == true && b4.getState() == true && b5.getState() == true && b6.getState() == true && b7.getState() == true)
        {
            rm.completeRoom();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleRoom5 : MonoBehaviour
{
    public ButtonController b1;
    public ButtonController b2;
    public LeverController lever1;
    public LeverController lever2;
    public LeverController lever3;
    public RoomManager rm;

    // Update is called once per frame
    void Update()
    {
        if (lever1.getState() == false && lever2.getState() == false && lever3.getState() == true && b1.getState() == true && b2.getState() == true)
        {
            rm.completeRoom();
        }
    }
}

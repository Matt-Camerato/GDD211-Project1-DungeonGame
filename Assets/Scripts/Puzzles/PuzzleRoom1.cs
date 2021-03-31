using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleRoom1 : MonoBehaviour
{
    public ButtonController b1;
    public ButtonController b2;
    public ButtonController b3;
    public ButtonController b4;
    public RoomManager rm;

    // Update is called once per frame
    void Update()
    {
        if (b4.getState() == true && b3.getState() == true && b2.getState() == true && b1.getState() == true)
        {
            rm.completeRoom();
        }
        else if (b4.getState() == true && (b3.getState() == false || b2.getState() == false || b1.getState() == false))
        {
            b4.setState(false);
            b3.setState(false);
            b2.setState(false);
            b1.setState(false);
        }
        else if (b3.getState() == true && (b2.getState() == false || b1.getState() == false))
        {
            b4.setState(false);
            b3.setState(false);
            b2.setState(false);
            b1.setState(false);
        }
        else if (b2.getState() == true && (b1.getState() == false))
        {
            b4.setState(false);
            b3.setState(false);
            b2.setState(false);
            b1.setState(false);
        }
    }
}

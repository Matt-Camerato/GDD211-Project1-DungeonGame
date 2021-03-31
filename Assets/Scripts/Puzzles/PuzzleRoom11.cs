using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleRoom11 : MonoBehaviour
{
    public RoomManager rm;
    public ButtonController b1;
    public ButtonController b2;
    public ButtonController b3;
    public LeverController lever1;
    public LeverController lever2;
    public LeverController lever3;

    // Update is called once per frame
    void Update()
    {
        if(b1.getState() == true && b2.getState() == true && b3.getState() == true && lever1.getState() == true && lever2.getState() == true && lever3.getState() == true)
        {
            rm.completeRoom();
        }
        else if(b1.getState() == false || b2.getState() == false || b3.getState() == false)
        {
            lever1.setState(false);
            lever2.setState(false);
            lever3.setState(false);
        }
    }
}

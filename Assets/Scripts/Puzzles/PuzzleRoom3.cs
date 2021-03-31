using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleRoom3 : MonoBehaviour
{
    public RoomManager rm;
    public ButtonController b1;
    public ButtonController b2;
    public LeverController lever;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(lever.getState() == true && b1.getState() == true && b2.getState() == true)
        {
            rm.completeRoom();
        }
        else if(lever.getState() == true && (b1.getState() == false || b2.getState() == false))
        {
            lever.setState(false);
            b1.setState(false);
            b2.setState(false);
        }
        else if(b2.getState() == true && (lever.getState() == true || b1.getState() == false))
        {
            lever.setState(false);
            b1.setState(false);
            b2.setState(false);
        }
    }
}

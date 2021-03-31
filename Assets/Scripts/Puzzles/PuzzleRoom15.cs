using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleRoom15 : MonoBehaviour
{
    public RoomManager rm;
    public PressurePlate pp;
    public LeverController lever1;
    public LeverController lever2;

    // Update is called once per frame
    void Update()
    {
        if(pp.getState() == true && lever1.getState() == false && lever2.getState() == true)
        {
            rm.completeRoom();
        }
    }
}

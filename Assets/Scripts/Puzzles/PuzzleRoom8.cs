using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleRoom8 : MonoBehaviour
{
    public PressurePlate pp1;
    public PressurePlate pp2;
    public PressurePlate pp3;
    public RoomManager rm;

    // Update is called once per frame
    void Update()
    {
        if(pp1.getState() == true && pp2.getState() == true && pp3.getState() == true)
        {
            rm.completeRoom();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleRoom6 : MonoBehaviour
{
    public RoomManager rm;
    public PressurePlate pp1;
    public PressurePlate pp2;

    // Update is called once per frame
    void Update()
    {
        if(pp1.getState() == true && pp2.getState() == true)
        {
            rm.completeRoom();
        }
    }
}

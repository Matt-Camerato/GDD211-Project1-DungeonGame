using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PuzzleRoom2 : MonoBehaviour
{
    public PressurePlate pp;
    public RoomManager rm;

    // Update is called once per frame
    void Update()
    {
        if(pp.getState() == true)
        {
            rm.completeRoom();
        }
        else
        {

        }
    }
}

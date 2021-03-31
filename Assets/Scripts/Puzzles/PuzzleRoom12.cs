using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleRoom12 : MonoBehaviour
{
    public RoomManager rm;
    public PressurePlate pp;

    // Update is called once per frame
    void Update()
    {
        if(pp.getState() == true)
        {
            rm.completeRoom();
        }
    }
}

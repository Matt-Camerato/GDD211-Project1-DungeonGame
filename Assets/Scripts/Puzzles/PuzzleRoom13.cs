using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleRoom13 : MonoBehaviour
{
    public RoomManager rm;
    public LeverController lever1;
    public LeverController lever2;
    public LeverController lever3;
    public LeverController lever4;

    // Update is called once per frame
    void Update()
    {
        if(lever1.getState() == true && lever2.getState() == false && lever3.getState() == true && lever4.getState() == true)
        {
            rm.completeRoom();
        }
    }
}

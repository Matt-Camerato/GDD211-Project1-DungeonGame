using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private Door doorN;
    [SerializeField] private Door doorE;
    [SerializeField] private Door doorS;
    [SerializeField] private Door doorW;

    [SerializeField] private bool roomCompleted = false; //set this to true when main puzzle of room is solved
    
    private bool doorsUnlocked = false;

    private void Update()
    {
        if (roomCompleted && !doorsUnlocked)
        {
            //this runs once when room is completed and unlocks all doors in room as well as the doors they are connected to

            if(doorN != null && doorN.locked)
            {
                doorN.UnlockDoor(); //unlock north door
                UnlockConnectedDoor("DoorS", Vector2.up); //find connected south door and unlock it too
            }

            if (doorE != null && doorE.locked)
            {
                doorE.UnlockDoor(); //unlock east door
                UnlockConnectedDoor("DoorW", Vector2.right); //find connected west door and unlock it too
            }

            if (doorS != null && doorS.locked)
            {
                doorS.UnlockDoor(); //unlock south door
                UnlockConnectedDoor("DoorN", Vector2.down); //find connected north door and unlock it too
            }

            if (doorW != null && doorW.locked)
            {
                doorW.UnlockDoor(); //unlock west door
                UnlockConnectedDoor("DoorE", Vector2.left); //find connected east door and unlock it too
            }

            doorsUnlocked = true;
        }
    }

    //finds connected door and unlocks it too
    private void UnlockConnectedDoor(string connectedDoorName, Vector2 connectedDoorDir)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, connectedDoorDir, 10);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.name == connectedDoorName)
            {
                hit.collider.GetComponent<Door>().UnlockDoor();
                break;
            }
        }
    }
}

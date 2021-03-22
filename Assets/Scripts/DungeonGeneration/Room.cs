using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public Vector2 coords;
    public bool doorN, doorE, doorS, doorW;

    public Room(Vector2 coords)
    {
        this.coords = coords;
    }
}

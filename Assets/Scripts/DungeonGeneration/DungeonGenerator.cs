using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    [Header("Dungeon Settings")]
    [SerializeField] private Vector2 unitRoomDimensions; //this tells the unit size of all dungeon rooms so they can be instantiated at the proper locations based on their width and height

    [SerializeField] private Vector2 halfDungeonSize; //this is half the dimensions of the dungeon (full size needs to be an even number so just multiplies this vector by 2)
    [SerializeField] private int totalRooms; //this determines how many rooms the dungeon will be made of

    //these lists will be used to determine what room prefab should be instantiated based on door bools
    [Header("Dungeon Room Prefab Lists")] 
    [SerializeField] private List<GameObject> doorNRooms;
    [SerializeField] private List<GameObject> doorERooms;
    [SerializeField] private List<GameObject> doorSRooms;
    [SerializeField] private List<GameObject> doorWRooms;

    [Header("End Room Prefab Lists")]
    [SerializeField] private List<GameObject> keyRooms;
    [SerializeField] private List<GameObject> escapeRooms;

    private int halfDungeonSizeX, halfDungeonSizeY; //int versions of half dimensions of dungeon

    private Room[,] rooms; //2D array of rooms based on their coordinate position in dungeon
    private List<Vector2> takenPositions = new List<Vector2>(); //pretty much copy of room array but makes checking if room exists already in a given position a lot easier

    private bool keyRoomSpawned = false;
    private bool escapeRoomSpawned = false;

    private void Start()
    {
        //first check to make sure totalRooms amount is actually possible with given dungeon size, else set it to maximum amount of total rooms
        if (totalRooms >= (halfDungeonSize.x * 2) * (halfDungeonSize.y * 2)) 
        { 
            totalRooms = Mathf.RoundToInt((halfDungeonSize.x * 2) * (halfDungeonSize.y * 2)); //sets total rooms to maximum
        }

        //setup int versions of dungeon half dimensions
        halfDungeonSizeX = Mathf.RoundToInt(halfDungeonSize.x);
        halfDungeonSizeY = Mathf.RoundToInt(halfDungeonSize.y);

        CreateRooms();
        SetRoomDoors();
        CreateDungeon();
    }

    private void CreateRooms()
    {
        rooms = new Room[halfDungeonSizeX * 2, halfDungeonSizeY * 2]; //setup 2D room array with given dimensions

        rooms[halfDungeonSizeX, halfDungeonSizeY] = new Room(Vector2.zero); //create first room in middle of dungeon with coords (0,0)
        takenPositions.Insert(0, Vector2.zero); //add room to taken positions list

        //these floats are used to add variation to dungeon generation
        float randomCompare = 0.2f; //this float is compared to a random.value and determines whether the room will branch out or stay clumped with previous rooms
        float randomCompareStart = 0.2f;
        float randomCompareEnd = 0.01f;

        Vector2 checkPos = Vector2.zero; //checkPos variable is used to first check position of new room before actually creating it

        for (int i = 0; i < totalRooms - 1; i++)
        {
            checkPos = NewPosition(); //first, grab a new position that is next to an already existing room

            //next, setup random floats to be used for a more interesting dungeon shape
            float randomPerc = i / (totalRooms - 1);
            randomCompare = Mathf.Lerp(randomCompareStart, randomCompareEnd, randomPerc);
            
            //if new position is clumped with existing rooms, try and find a different new position that has less neighbors, therefore causing the dungeon to branch out
            //this has a higher chance of occuring towards the start of the loop and decreases over time
            if(NumberOfNeighbors(checkPos, takenPositions) > 1 && Random.value > randomCompare)
            {
                int iterations = 0; //this is to make sure it wont keep doing this forever and gives up after 100 attempts
                do
                {
                    checkPos = SelectiveNewPosition(); //use selective new position method this time to instead get a branching position rather than just a random one
                    iterations++;
                }
                while (NumberOfNeighbors(checkPos, takenPositions) > 1 && iterations < 100);
            }

            //once new room position is ideal, it is time to create the actual room
            rooms[(int)checkPos.x + halfDungeonSizeX, (int)checkPos.y + halfDungeonSizeY] = new Room(checkPos); //create new room in rooms array
            takenPositions.Insert(0, checkPos); //add room position to taken positions list
        }
    }

    private Vector2 NewPosition()
    {
        //these will be used for coords of new position
        int x = 0;
        int y = 0;
        Vector2 checkingPos = Vector2.zero;

        do
        {
            //first, get coords of random room that already exists and set x and y variables
            int index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
            x = (int)takenPositions[index].x;
            y = (int)takenPositions[index].y;

            //next, determine if new room will be up, down, left, or right from randomly selected room
            bool yAxis = Random.value < 0.5f;
            bool positive = Random.value < 0.5f;

            if (yAxis)
            {
                if (positive)
                {
                    y += 1; //if yAxis and positive, new room will be above randomly selected room
                }
                else
                {
                    y -= 1; //if yAxis but no positive, new room will be below randomly selected room
                }
            }
            else
            {
                if (positive)
                {
                    x += 1; //if not yAxis but positive, new room will be to the right of randomly selected room
                }
                else
                {
                    x -= 1; //if not yAxis and not positive, new room will be to the left of randomly selected room
                }
            }

            //lastly, set checkingPos variable so new position can be checked to see if room already exists
            checkingPos = new Vector2(x, y);
        }
        while (takenPositions.Contains(checkingPos) || x >= halfDungeonSizeX || x < -halfDungeonSizeX || y >= halfDungeonSizeY || y < -halfDungeonSizeY);
        //loop continues until new position coords are within dungeon dimensions and a room doesn't exist there yet

        return checkingPos; //return new position coords
    }

    //this method is very similar to regular NewPosition() method but with slight change to further encourage branching of dungeon
    private Vector2 SelectiveNewPosition()
    {
        //these will be used for coords of new position
        int x = 0;
        int y = 0;
        Vector2 checkingPos = Vector2.zero;

        //these are used for slight change in this method that allows branching
        int index = 0;
        int increment = 0;
        do
        {
            //instead of just getting a random room and creating a new room next to it, this loop will look specifically for a random room that doesn't have more than 1 neighbor
            //this makes sure that the new postition wont be clumped with a bunch of rooms that already exist and will instead branch away from the dungeon
            increment = 0;
            do
            {

                index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
                increment++;
            }
            while (NumberOfNeighbors(takenPositions[index], takenPositions) > 1 && increment < 100);

            //once new branching position is found, set x and y variables
            x = (int)takenPositions[index].x;
            y = (int)takenPositions[index].y;

            //next, determine if new room will be up, down, left, or right from randomly selected room
            bool yAxis = Random.value < 0.5f;
            bool positive = Random.value < 0.5f;

            if (yAxis)
            {
                if (positive)
                {
                    y += 1; //if yAxis and positive, new room will be above randomly selected room
                }
                else
                {
                    y -= 1; //if yAxis but no positive, new room will be below randomly selected room
                }
            }
            else
            {
                if (positive)
                {
                    x += 1; //if not yAxis but positive, new room will be to the right of randomly selected room
                }
                else
                {
                    x -= 1; //if not yAxis and not positive, new room will be to the left of randomly selected room
                }
            }

            //lastly, set checkingPos variable so new position can be checked to see if room already exists
            checkingPos = new Vector2(x, y);
        }
        while (takenPositions.Contains(checkingPos) || x >= halfDungeonSizeX || x < -halfDungeonSizeX || y >= halfDungeonSizeY || y < -halfDungeonSizeY);
        //loop continues until new position coords are within dungeon dimensions and a room doesn't exist there yet

        return checkingPos; //return new position coords
    }

    private int NumberOfNeighbors(Vector2 _checkingPos, List<Vector2> _takenPositions)
    {
        int neighbors = 0;

        //if a room exists to the north, south, east, or west, neighbors value is incremented
        if (_takenPositions.Contains(_checkingPos + Vector2.up)) { neighbors++; } //north
        if (_takenPositions.Contains(_checkingPos + Vector2.down)) { neighbors++; } //south
        if (_takenPositions.Contains(_checkingPos + Vector2.right)) { neighbors++; } //east
        if (_takenPositions.Contains(_checkingPos + Vector2.left)) { neighbors++; } //west

        return neighbors; //return value for number of neighbor rooms that already exist
    }

    private void SetRoomDoors()
    {
        //nested for loops go through every room in dungeon and set their door bools accordingly based on neighboring rooms
        for (int x = 0; x < (halfDungeonSizeX * 2); x++)
        {
            for (int y = 0; y < (halfDungeonSizeY * 2); y++)
            {
                if(rooms[x, y] == null) { continue; } //if there isn't a room at this location, continue to next location

                //check position directly below this room
                if (y - 1 < 0)
                {
                    rooms[x, y].doorS = false; //if position is out of map, room doesn't need south door
                }
                else
                {
                    rooms[x, y].doorS = rooms[x, y - 1] != null; //if room exists at position, south door is needed
                }

                //check position directly above this room
                if (y + 1 >= halfDungeonSizeY * 2)
                {
                    rooms[x, y].doorN = false; //if position is out of map, room doesn't need north door
                }
                else
                {
                    rooms[x, y].doorN = rooms[x, y + 1] != null; //if room exists at position, north door is needed
                }

                //check position directly left of this room
                if (x - 1 < 0)
                {
                    rooms[x, y].doorW = false; //if position is out of map, room doesn't need west door
                }
                else
                {
                    rooms[x, y].doorW = rooms[x - 1, y] != null; //if room exists at position, west door is needed
                }

                //check position directly right of this room
                if (x + 1 >= halfDungeonSizeX * 2)
                {
                    rooms[x, y].doorE = false; //if position is out of map, room doesn't need east door
                }
                else
                {
                    rooms[x, y].doorE = rooms[x + 1, y] != null; //if room exists at position, east door is needed
                }
            }
        }
    }

    private void CreateDungeon()
    {
        //loop through every room and instantiate prefab according to door bools at given location
        foreach(Room room in rooms)
        {
            if(room == null) { continue; } //if there isn't a room at this location, continue to next location

            //use room coords to determine position of instantiation based on actual dimensions of all rooms
            Vector2 roomPos = room.coords;
            roomPos.x *= unitRoomDimensions.x;
            roomPos.y *= unitRoomDimensions.y;

            int roomType = 0; //this variable determines if room will be normal or instead a key/escape room

            //check if end room and whether key and escape rooms have spawned already
            if (NumberOfNeighbors(room.coords, takenPositions) == 1)
            {
                if (Mathf.Abs(room.coords.x) >= halfDungeonSizeX - 2 || Mathf.Abs(room.coords.y) >= halfDungeonSizeY - 2) //make sure room isn't close to center of dungeon
                {
                    if (!keyRoomSpawned)
                    {
                        keyRoomSpawned = true;
                        roomType = 1;
                    }
                    else if (!escapeRoomSpawned)
                    {
                        escapeRoomSpawned = true;
                        roomType = 2;
                    }
                }
            }

            //get random room prefab from all possible rooms that has the given door configuration (and also is key/escape room if specified in roomType)
            GameObject currentRoomPrefab = GetRoomFromDoors(room.doorN, room.doorE, room.doorS, room.doorW, roomType);

            //lastly, instantiate the prefab at its calculated location
            GameObject newRoom = Instantiate(currentRoomPrefab, roomPos, Quaternion.identity);
        }
    }

    //this method returns a randomly selected room prefab based on which doors it should and shouldn't have
    private GameObject GetRoomFromDoors(bool doorN, bool doorE, bool doorS, bool doorW, int roomType)
    {
        //first, create a list of possible rooms and initialize it with all the room prefabs from one of the four prefab lists (doorNRooms, doorERooms, doorSRooms, or doorWRooms)
        List<GameObject> possibleRooms;
        if (doorN) { possibleRooms = new List<GameObject>(doorNRooms); }
        else if (doorE) { possibleRooms = new List<GameObject>(doorERooms); }
        else if (doorS) { possibleRooms = new List<GameObject>(doorSRooms); }
        else { possibleRooms = new List<GameObject>(doorWRooms); }

        //next, loop through a COPY of possibleRooms list and remove prefabs from ORIGINAL possibleRooms list that don't have correct doors or room type
        List<GameObject> possibleRoomsCopy = new List<GameObject>(possibleRooms);
        foreach(GameObject room in possibleRoomsCopy)
        {
            if (doorN)
            {
                if (!doorNRooms.Contains(room)) //if north door IS needed and room ISN'T on list of doorNRooms, remove it from possible rooms and continue to check next room
                {
                    possibleRooms.Remove(room);
                    continue;
                }
            }
            else
            {
                if (doorNRooms.Contains(room)) //if north door ISN't needed and room IS on list of doorNRooms, remove it from possible rooms and continue to check next room
                {
                    possibleRooms.Remove(room);
                    continue;
                }
            }

            if (doorE)
            {
                if (!doorERooms.Contains(room)) //if east door IS needed and room ISN'T on list of doorERooms, remove it from possible rooms and continue to check next room
                {
                    possibleRooms.Remove(room);
                    continue;
                }
            }
            else
            {
                if (doorERooms.Contains(room)) //if east door ISN't needed and room IS on list of doorERooms, remove it from possible rooms and continue to check next room
                {
                    possibleRooms.Remove(room);
                    continue;
                }
            }

            if (doorS)
            {
                if (!doorSRooms.Contains(room)) //if south door IS needed and room ISN'T on list of doorSRooms, remove it from possible rooms and continue to check next room
                {
                    possibleRooms.Remove(room);
                    continue;
                }
            }
            else
            {
                if (doorSRooms.Contains(room)) //if south door ISN't needed and room IS on list of doorSRooms, remove it from possible rooms and continue to check next room
                {
                    possibleRooms.Remove(room);
                    continue;
                }
            }

            if (doorW)
            {
                if (!doorWRooms.Contains(room)) //if west door IS needed and room ISN'T on list of doorWRooms, remove it from possible rooms and continue to check next room
                {
                    possibleRooms.Remove(room);
                    continue;
                }
            }
            else
            {
                if (doorWRooms.Contains(room)) //if west door ISN't needed and room IS on list of doorWRooms, remove it from possible rooms and continue to check next room
                {
                    possibleRooms.Remove(room);
                    continue;
                }
            }

            //check room type and get rid of rooms that don't match
            if (roomType == 0)
            {
                if(keyRooms.Contains(room) || escapeRooms.Contains(room))
                {
                    possibleRooms.Remove(room); //remove special rooms if just a normal room
                    continue;
                }
            }
            else if(roomType == 1)
            {
                if (!keyRooms.Contains(room))
                {
                    possibleRooms.Remove(room); //if this is the key room, remove any rooms that aren't key rooms
                    continue;
                }
            }
            else if(roomType == 2)
            {
                if (!escapeRooms.Contains(room))
                {
                    possibleRooms.Remove(room); //if this is this escape room, remove any rooms that aren't escape rooms
                    continue;
                }
            }
        }

        //now that possible rooms list is accurate, a random room is selected from it and returned
        return possibleRooms[Random.Range(0, possibleRooms.Count)];
    }
}

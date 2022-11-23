using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Room : MonoBehaviour
{
    [SerializeField] private Door topDoor;
    [SerializeField] private Door downDoor;
    [SerializeField] private Door rightDoor;
    [SerializeField] private Door leftDoor;

    private List<Door> _doors;


    public List<Door> Doors => _doors;
    private void Awake()
    {
        _doors.Add(topDoor);
        _doors.Add(downDoor);
        _doors.Add(rightDoor);
        _doors.Add(leftDoor);
    }
    
    public Coordinates _coordinates;
    
    public void SetupRoom(int x, int y)
    {
        _coordinates = new Coordinates(x, y);
    }
    
    public List<Door> EnableDoors(int amount)
    {
       return new List<Door>();
    }
}

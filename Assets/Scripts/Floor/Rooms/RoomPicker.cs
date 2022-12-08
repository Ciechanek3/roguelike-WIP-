using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using ObjectPooling;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Floor.Rooms
{
    public class RoomPicker : MonoBehaviour
    {
        [Header("Rooms setup:")] 
        [SerializeField] private Room startingRoom;
        [SerializeField] private List<Room> availableRooms;
        [Range(1, 10)]
        [SerializeField] private int poolingMultiplier;
        
        public Room StartingRoom => startingRoom;

        private ObjectPool<Room> _roomPool;

        private void Awake()
        {
            _roomPool = new ObjectPool<Room>(availableRooms, poolingMultiplier);
        }

        private Room GetRandomRoomFromList(DoorType doorType, List<Room> possibleRooms)
        {
            Room randomRoom = _roomPool.TakeElementFromPool(() => possibleRooms.Where(r => r.CheckIfDoorExist(doorType)).ToList());
            return randomRoom;
        }

        public Room GetProperRoomRandomly(List<Coordinates> coordinatesList, int coordinatesIndex)
        {
            int x = coordinatesList[coordinatesIndex].X;
            int y = coordinatesList[coordinatesIndex].Y;

            List<DoorType> doorTypes = new List<DoorType>();
            
            for (int i = 0; i < coordinatesList.Count; i++)
            {
                if (coordinatesList[i].X != x  && coordinatesList[i].Y != y)
                {
                    continue;
                }
                if (coordinatesList[i].X - x == 1)
                {
                    doorTypes.Add(DoorType.Right);
                }
                else if (coordinatesList[i].X - x == -1)
                {
                    doorTypes.Add(DoorType.Left);
                }
                else if (coordinatesList[i].Y - y == 1)
                {
                    doorTypes.Add(DoorType.Top);
                }
                else if (coordinatesList[i].Y - y == -1)
                {
                    doorTypes.Add(DoorType.Down);
                }
            }
            
            Room room = GetRandomRoomWithMatchingDoors(doorTypes);
            return room;
        }
        
        private Room GetRandomRoomWithMatchingDoors(List<DoorType> doorTypes)
        {
            List<Room> possibleRooms = _roomPool.ObjectsInPool;
            Room room = GetRandomRoomFromList(doorTypes[0], possibleRooms);

            for (int i = 1; i < doorTypes.Count; i++)
            {
                if (room.CheckIfDoorExist(doorTypes[i]) == false)
                {
                    _roomPool.MakePool(1);
                    _roomPool.GetElementToPool(room);
                    room = GetRandomRoomWithMatchingDoors(doorTypes);
                    i = doorTypes.Count;
                }
            }

            return room;
        }
    }
}

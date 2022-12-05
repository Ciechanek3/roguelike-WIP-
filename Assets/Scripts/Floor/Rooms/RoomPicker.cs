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

        private Room GetRandomRoomWithMatchingDoors(List<DoorType> doorTypes)
        {
            if (_roomPool.ObjectsInPool.Count == 0)
            {
                _roomPool.MakePool(1);
            }
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

        private Room GetRandomRoomFromList(DoorType doorType, List<Room> possibleRooms)
        {
            List<Room> rooms = possibleRooms.Where(r => r.CheckIfDoorExist(doorType)).ToList();
            Room randomRoom = rooms[Random.Range(0, rooms.Count)];
            _roomPool.TakeElementFromPool(randomRoom);
            return randomRoom;
        }
        
        private List<DoorType> CompareNeighborRoomCoordinates(Coordinates nei, Coordinates cur, List<DoorType> dTypes)
        {
            if (nei.X > cur.X)
            {
                dTypes.Add(DoorType.Right);
            }
            else if (nei.X < cur.X)
            {
                dTypes.Add(DoorType.Left);
            }

            if (nei.Y > cur.Y)
            {
                dTypes.Add(DoorType.Top);
            }
            else if (nei.Y < cur.Y)
            {
                dTypes.Add(DoorType.Down);
            }

            dTypes = dTypes.Distinct().ToList();
            return dTypes;
        }
        
        public Room GetProperRoomRandomly(Coordinates prevRoom, Coordinates currRoom, [CanBeNull] Coordinates nextRoom)
        {
            List<DoorType> doorTypes = new List<DoorType>();

            if (prevRoom != null)
            {
                doorTypes = CompareNeighborRoomCoordinates(prevRoom, currRoom, doorTypes);
            }
            
            if (nextRoom != null)
            {
                doorTypes = CompareNeighborRoomCoordinates(nextRoom, currRoom, doorTypes);
            }

            Room room = GetRandomRoomWithMatchingDoors(doorTypes);

            return room;
        }
    }
}

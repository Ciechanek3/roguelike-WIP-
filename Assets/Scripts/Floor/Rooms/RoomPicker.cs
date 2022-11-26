using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Floor.Rooms
{
    public class RoomPicker : MonoBehaviour
    {
        [Header("Rooms setup:")] 
        [SerializeField] private Room startingRoom;
        [SerializeField] private List<Room> availableRooms;

        public Room StartingRoom => startingRoom;

        private Room GetRandomRoomFromList(DoorType doorType, List<Room> possibleRooms)
        {
            List<Room> rooms = possibleRooms.Where(r => r.CheckIfDoorExist(doorType)).ToList();
            return rooms[Random.Range(0, rooms.Count)];
        }

        private Room GetRandomRoomWithMatchingDoors(List<DoorType> doorTypes, [CanBeNull] List<Room> possibleRooms)
        {
            if (possibleRooms == null)
            {
                possibleRooms = availableRooms;
            }
            Room room = GetRandomRoomFromList(doorTypes[0], possibleRooms);

            for (int i = 1; i < doorTypes.Count; i++)
            {
                if (room.CheckIfDoorExist(doorTypes[i]) == false)
                {
                    possibleRooms.Remove(room);
                    if (possibleRooms.Count == 0) return null;
                    room = GetRandomRoomWithMatchingDoors(doorTypes, possibleRooms);
                    i = doorTypes.Count;
                };
            }

            return room;
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

            Room room = GetRandomRoomWithMatchingDoors(doorTypes, null);

            return room;
        }
    }
}

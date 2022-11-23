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
        
        private readonly List<Room> _rightDoorRooms = new List<Room>();
        private readonly List<Room> _leftDoorRooms = new List<Room>();
        private readonly List<Room> _topDoorRooms = new List<Room>();
        private readonly List<Room> _downDoorRooms = new List<Room>();

        private void Awake()
        {
            for (int i = 0; i < availableRooms.Count; i++)
            {
                for (int j = 0; j < availableRooms[i].Doors.Count; j++)
                {
                    if (CheckIfRoomShouldBeAdded(availableRooms[i].Doors[j].DoorType, availableRooms[i], _rightDoorRooms, DoorType.Right))
                    {
                        continue;
                    }

                    if (CheckIfRoomShouldBeAdded(availableRooms[i].Doors[j].DoorType, availableRooms[i], _leftDoorRooms, DoorType.Left))
                    {
                        continue;
                    }

                    if (CheckIfRoomShouldBeAdded(availableRooms[i].Doors[j].DoorType, availableRooms[i], _topDoorRooms, DoorType.Top))
                    {
                        continue;
                    }

                    CheckIfRoomShouldBeAdded(availableRooms[i].Doors[j].DoorType, availableRooms[i], _downDoorRooms, DoorType.Down);
                }
            }
        }
        
        private bool CheckIfRoomShouldBeAdded(DoorType doorType, Room room, List<Room> rooms, DoorType neededDoorType)
        {
            if (doorType != neededDoorType) return false;
            rooms.Add(room);
            return true;
        }

        public Room GetRandomRoomWithMatchingDoors(List<DoorType> doorTypes)
        {
            Room room = GetRandomRoomWithSpecificDoor(doorTypes[0]);

            for (int i = 1; i < doorTypes.Count; i++)
            {
                
                if (room.Doors.Contains(d => d.DoorType == doorTypes[i])) ;
            }
        }

        private Room GetRandomRoomWithSpecificDoor(DoorType doorType)
        {
            Room room;
            switch (doorType)
            {
                case DoorType.Top:
                {
                    room = GetRandomRoomFromList(_topDoorRooms);
                    break;
                }
                case DoorType.Down:
                {
                    room = GetRandomRoomFromList(_downDoorRooms);
                    break;
                }
                case DoorType.Left:
                {
                    room = GetRandomRoomFromList(_leftDoorRooms);
                    break;
                }
                case DoorType.Right:
                {
                    room = GetRandomRoomFromList(_rightDoorRooms);
                    break;
                }
                default:
                {
                    room = GetRandomRoomFromList(_rightDoorRooms);
                    break;
                }
            }

            return room;
        }
        
        private Room GetRandomRoomFromList(List<Room> listOfRooms)
        {
            return listOfRooms[Random.Range(0, listOfRooms.Count)];
        }

        public Room SetupStartingRoom(int roomsEnabled)
        {
            Room room = Instantiate(startingRoom);
            SetupRoom(room, 0, 0);
            return room;
        }
        
        public void SetupRoom(Room room, int x, int y)
        {
            room.SetupRoom(x, y);
        }

        public Room FilterRooms(Coordinates prevRoom, Coordinates currRoom, [CanBeNull] Coordinates nextRoom)
        {
            List<DoorType> doorTypes = new List<DoorType>();

            doorTypes = CompareNeighborRoomCoordinates(prevRoom, currRoom, doorTypes);

            if (nextRoom != null)
            {
                doorTypes = CompareNeighborRoomCoordinates(nextRoom, currRoom, doorTypes);
            }
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

        private Room GetRoomWithProperDoors(List<DoorType> dTypes)
        {
            for (int i = 0; i < dTypes.Count; i++)
            {
                
            }
        }
    }
}

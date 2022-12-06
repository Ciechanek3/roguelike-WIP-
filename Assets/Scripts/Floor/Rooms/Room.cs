using System.Collections.Generic;
using System.Linq;
using ObjectPooling;
using Settings;
using UnityEngine;
using UnityEngine.Serialization;

namespace Floor.Rooms
{
    public class Room : MonoBehaviour, IPool
    {
        [SerializeField] private List<Door> doors;

        public int index, x, y;
        
        public void Setup()
        {
            gameObject.SetActive(true);
        }
        
        public void PlaceOnScene(List<Coordinates> coordinatesList, int coordinatesIndex)
        {
            gameObject.transform.position = new Vector3(
                coordinatesList[coordinatesIndex].X * GameSettings.Instance.XRoomSize, 
                0, 
                coordinatesList[coordinatesIndex].Y * GameSettings.Instance.YRoomSize);
            
            SetupDoors(coordinatesList, coordinatesIndex);
            
            index = coordinatesIndex;
            x = coordinatesList[coordinatesIndex].X;
            y = coordinatesList[coordinatesIndex].Y;
        }

        private void SetupDoors(List<Coordinates> coordinatesList, int coordinatesIndex)
        {
            int x = coordinatesList[coordinatesIndex].X;
            int y = coordinatesList[coordinatesIndex].Y;
            for (int i = 0; i < coordinatesList.Count; i++)
            {
                if (coordinatesList[i].X != x && coordinatesList[i].Y != y)
                {
                    continue;
                }
                if (coordinatesList[i].X - x == 1)
                {
                    SetupDoor(DoorType.Right);
                }
                else if (coordinatesList[i].X - x == -1)
                {
                    SetupDoor(DoorType.Left);
                }
                else if (coordinatesList[i].Y - y == 1)
                {
                    SetupDoor(DoorType.Top);
                }
                else if (coordinatesList[i].Y - y == -1)
                {
                    SetupDoor(DoorType.Down);
                }
            }
        }
        
        private void SetupDoor(DoorType doorType)
        {
            Door door = doors.FirstOrDefault(d => d.DoorType == doorType);
            if (door != null)
            {
                door.Disable();
            }
        }

        public bool CheckIfDoorExist(DoorType doorType)
        {
            return doors.Any(t => t.DoorType == doorType);
        }
    }
}

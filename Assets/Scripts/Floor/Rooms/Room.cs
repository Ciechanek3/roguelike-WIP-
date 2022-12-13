using System.Collections.Generic;
using System.Linq;
using ObjectPooling;
using Settings;
using UnityEngine;
using UnityEngine.AI;

namespace Floor.Rooms
{
    public class Room : MonoBehaviour, IPool
    {
        [SerializeField] private List<Door> doors;
        [SerializeField] private NavMeshSurface surface;

        private Coordinates _coordinates;

        public void PlaceOnScene(List<Coordinates> coordinatesList, int coordinatesIndex)
        {
            gameObject.transform.position = new Vector3(
                coordinatesList[coordinatesIndex].X * GameSettings.Instance.XRoomSize, 
                0, 
                coordinatesList[coordinatesIndex].Y * GameSettings.Instance.YRoomSize);
            
            _coordinates = new Coordinates(coordinatesList[coordinatesIndex].X, coordinatesList[coordinatesIndex].Y);
            SetupDoors(coordinatesList);
            BuildNavMesh();
        }

        private void SetupDoors(List<Coordinates> coordinatesList)
        {
            for (int i = 0; i < coordinatesList.Count; i++)
            {
                if (coordinatesList[i].X != _coordinates.X  && coordinatesList[i].Y != _coordinates.Y)
                {
                    continue;
                }
                if (coordinatesList[i].X - _coordinates.X == 1)
                {
                    OpenDoors(DoorType.Right);
                }
                else if (coordinatesList[i].X - _coordinates.X == -1)
                {
                    OpenDoors(DoorType.Left);
                }
                else if (coordinatesList[i].Y - _coordinates.Y == 1)
                {
                    OpenDoors(DoorType.Top);
                }
                else if (coordinatesList[i].Y - _coordinates.Y == -1)
                {
                    OpenDoors(DoorType.Down);
                }
            }
        }
        
        private void OpenDoors(DoorType doorType)
        {
            Door door = doors.FirstOrDefault(d => d.DoorType == doorType);
            if (door != null)
            {
                door.Open();
            }
        }

        private void BuildNavMesh()
        {
            surface.BuildNavMesh();
        }
        
        public bool CheckIfDoorExist(DoorType doorType)
        {
            return doors.Any(t => t.DoorType == doorType);
        }
    }
}

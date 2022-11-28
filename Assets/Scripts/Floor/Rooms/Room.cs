using System.Collections.Generic;
using Settings;
using UnityEngine;

namespace Floor.Rooms
{
    public class Room : MonoBehaviour
    {
        [SerializeField] private List<Door> _doors;

        public void SetupRoom(Coordinates coordinates, int roomNumber)
        {
            gameObject.transform.position = new Vector3(coordinates.X * GameSettings.Instance.XRoomSize, 
                                                        0, 
                                                        coordinates.Y * GameSettings.Instance.YRoomSize);
        }
    
        public bool CheckIfDoorExist(DoorType doorType)
        {
            for (int i = 0; i < _doors.Count; i++)
            {
                if (_doors[i].DoorType == doorType)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

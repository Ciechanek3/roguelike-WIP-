using System.Collections.Generic;
using System.Linq;
using Floor.Rooms;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Floor
{
    public class FloorGenerator : MonoBehaviour
    {
        #region Variables

        [SerializeField] private RoomPicker roomPicker;
         
        [SerializeField] private int numberOfRooms;
        [SerializeField] private List<SpecialRoom> specialRooms;

        private int _currentX;
        private int _currentY;

        #endregion
        #region SetupData
        
        private void Start()
        {
            CreateFloor();
        }
        #endregion

        private void CreateFloor()
        {
            List<Coordinates> _takenCoordinates = CreateFloorCoordinates();
            SetupRoomsBasedOnCoordinates(_takenCoordinates);
        }

        private void SetupRoomsBasedOnCoordinates(List<Coordinates> coordinatesList)
        {
            Room startingRoom = Instantiate(roomPicker.StartingRoom);
            Room room;

            for (int i = 1; i < coordinatesList.Count; i++)
            {
                room = roomPicker.GetProperRoomRandomly(coordinatesList, i);
                room.PlaceOnScene(coordinatesList, i);
            }
            
            startingRoom.PlaceOnScene(coordinatesList, 0);
        }

        private List<Coordinates> CreateFloorCoordinates()
        {
            List<Coordinates> coordinatesList = new List<Coordinates>();
            _currentX = 0;
            _currentY = 0;
            coordinatesList.Add(new Coordinates(_currentX, _currentX));
            
            for (int i = 1; i < numberOfRooms; i++)
            {
                int x = Random.Range(0, 3);
                if (x == 0)
                {
                    _currentX += 1;
                }
                else if (x == 1)
                {
                    _currentX -= 1;
                }
                else if (x == 2)
                {
                    int y = Random.Range(0, 3);
                    
                    if (y == 0)
                    {
                        _currentY += 1;
                    }
                    else if (y == 1)
                    {
                        _currentY -= 1;
                    }
                }

                Coordinates coordinates = new Coordinates(_currentX, _currentY);

                if (coordinatesList.Any(c => c.X == _currentX && c.Y == _currentY))
                {
                    i--;
                    continue;
                }
                coordinatesList.Add(coordinates);
            }

            return coordinatesList;
        }

    }
}

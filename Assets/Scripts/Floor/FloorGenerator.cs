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
        private readonly List<Coordinates> _takenCoordinates = new List<Coordinates>();

        #endregion
        #region SetupData
        
        private void Start()
        {
            CreateFloor();
        }
        #endregion

        private void CreateFloor()
        {
            CreateFloorCoordinates();

            Room startingRoom = Instantiate(roomPicker.StartingRoom);
            Room room;

            for (int i = 1; i < _takenCoordinates.Count; i++)
            {
                room = roomPicker.GetProperRoomRandomly(_takenCoordinates, i);
                room.PlaceOnScene(_takenCoordinates, i);
            }
            
            startingRoom.PlaceOnScene(_takenCoordinates, 0);
        }

        private void CreateFloorCoordinates()
        {
            _currentX = 0;
            _currentY = 0;
            _takenCoordinates.Add(new Coordinates(_currentX, _currentX));
            
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

                if (_takenCoordinates.Any(c => c.X == _currentX && c.Y == _currentY))
                {
                    i--;
                    continue;
                }
                _takenCoordinates.Add(coordinates);
            }
        }

    }
}

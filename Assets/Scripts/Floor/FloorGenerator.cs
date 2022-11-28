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

        [SerializeField] private RoomPicker _roomPicker;
         
        [SerializeField] private int numberOfRooms;
        [SerializeField] private List<SpecialRoom> specialRooms;

        private int _currentX;
        private int _currentY;
        private List<Coordinates> _takenCoordinates = new List<Coordinates>();

        #endregion
        #region SetupData
        
        private void Start()
        {
            CreateFloor();
        }
        #endregion

        public void CreateFloor()
        {
            CreateFloorCoordinates();
            Room room = Instantiate(_roomPicker.StartingRoom);
            room.SetupRoom(_takenCoordinates[0], 0);

            for (int i = 1; i < _takenCoordinates.Count; i++)
            {
                room = Instantiate(_roomPicker.GetProperRoomRandomly(
                    _takenCoordinates[i - 1], 
                    _takenCoordinates[i], 
                    i + 1 < _takenCoordinates.Count ? _takenCoordinates[i + 1] : null));
                room.SetupRoom(_takenCoordinates[i], i);
            }
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

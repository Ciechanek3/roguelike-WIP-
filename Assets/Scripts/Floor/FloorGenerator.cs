using System.Collections.Generic;
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
        [SerializeField] private int xGrid;
        [SerializeField] private int yGrid;

        
        private int _roomNumberLeft;

        private int _currentX;
        private int _currentY;
        private List<Coordinates> _takenCoordinates;
        private List<Room> _rooms;

        #endregion
        #region SetupData
    
        private void Awake()
        {
            _roomNumberLeft = numberOfRooms;
            CreateFloorCoordinates();
        }

        private void Start()
        {
            CreateFloor();
        }
        #endregion

        public void CreateFloor()
        {
            int randomNumber = Random.Range(1, 5);
            _roomPicker.SetupStartingRoom(randomNumber);
            _roomNumberLeft--;


            for (int i = 0; i < _takenCoordinates.Count; i++)
            {
                _roomPicker.SetupRoom();
            }
            /*List<Door> openedDoors = _roomPicker.SetupStartingRoom(randomNumber);
            _roomNumberLeft -= randomNumber;

            for (int i = 0; i < openedDoors.Count; i++)
            {
                if (_roomNumberLeft == 0)
                {
                    return;
                }
                Room randomRoom = _roomPicker.GetRandomElementFromOppositeDoor(openedDoors[i].DoorType);
                Instantiate(randomRoom);
                _roomNumberLeft--;
            }*/
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
                    int y = Random.Range(0, 2);
                    
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
                
                if (_takenCoordinates.Contains(coordinates))
                {
                    i--;
                    continue;
                }
                _takenCoordinates.Add(coordinates);
            }
        }

    }
}


using System;
using Random = UnityEngine.Random;

namespace Floor.Rooms
{
    [Serializable]
    public class SpecialRoom
    {
        public Room Room;
        public int Chances;

        public bool CheckIfRoomShouldSpawn()
        {
            int random = Random.Range(0, 100);
            if (random >= Chances)
            {
                return false;
            }
            return true;
        }
    }
}

using UnityEngine;

namespace Settings
{
    public class GameSettings : MonoBehaviour
    {
        [SerializeField] private int xRoomSize;
        [SerializeField] private int yRoomSize;

        public int XRoomSize => xRoomSize;
        public int YRoomSize => yRoomSize;
        public static GameSettings Instance { get; private set; }
    
        private void Awake()
        {
            if (Instance != null && Instance != this) 
            { 
                Destroy(this); 
            } 
            else 
            { 
                Instance = this; 
            }
        }
    }
}

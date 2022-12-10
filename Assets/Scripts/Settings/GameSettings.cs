using System;
using UnityEngine;

namespace Settings
{
    public class GameSettings : MonoBehaviour
    {
        [Header ("Room size:")]
        [SerializeField] private int xRoomSize;
        [SerializeField] private int yRoomSize;

        [SerializeField] private GameObject playerPrefab;
        
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

        private void Start()
        {
            playerPrefab.transform.position = new Vector3(xRoomSize / 2, 0, -yRoomSize / 2);
        }
    }
}

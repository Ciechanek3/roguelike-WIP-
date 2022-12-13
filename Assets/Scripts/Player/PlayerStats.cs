using UnityEngine;

namespace Player
{
    public class PlayerStats : MonoBehaviour
    {
        private int maxHealth;
        private float dmgMultiplier = 1;
        private float msMultiplier = 1;
        private int currentLvl;
        private int currentXP;

        public static PlayerStats Instance { get; private set; }
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

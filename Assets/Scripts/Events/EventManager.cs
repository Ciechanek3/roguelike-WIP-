using System;
using UnityEngine;

namespace Events
{
    public class EventManager : MonoBehaviour
    {
        public static event Action OnReloadInputEvent;
        public static event Action OnShootInputEvent;
        public static event Action<int,int> OnShootEvent;

        public static void ReloadInputEvent()
        {
            OnReloadInputEvent?.Invoke();
        }
    
        public static void ShootInputEvent()
        {
            OnShootInputEvent?.Invoke();
        }

        public static void UpdateAmmo(int cur, int max)
        {
            OnShootEvent?.Invoke(cur, max);
        }
    }
}

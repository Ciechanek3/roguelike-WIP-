using System;
using UnityEngine;

namespace Events
{
    public class EventManager : MonoBehaviour
    {
        public static event Action ReloadInputEvent;
        public static event Action ShootInputEvent;

        public static void OnReloadInputEvent()
        {
            ReloadInputEvent?.Invoke();
        }
    
        public static void OnShootInputEvent()
        {
            Debug.Log("xd");
            ShootInputEvent?.Invoke();
        }
    }
}

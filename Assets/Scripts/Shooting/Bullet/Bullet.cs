using System;
using ObjectPooling;
using UnityEngine;

namespace Shooting.Bullet
{
    public class Bullet : MonoBehaviour, IPool
    {
        [SerializeField] private Rigidbody rb;

        public event Action Collided;
        
        public void MoveTo(Vector3 destination)
        {
            transform.LookAt(destination);
            rb.AddForce(transform.forward * 1000);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Collided?.Invoke();
        }
    }
    
}

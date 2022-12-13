using System;
using ObjectPooling;
using UnityEngine;

namespace Shooting.Bullet
{
    public class Bullet : MonoBehaviour, IPool
    {
        [SerializeField] private Rigidbody rb;

        private float damage;
        
        public event Action Collided;
        
        public void Setup(Vector3 destination, float multiplier, float dmg)
        {
            damage = dmg;
            transform.LookAt(destination);
            rb.AddForce(transform.forward * 1000 * multiplier);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Collided?.Invoke();
        }
    }
    
}

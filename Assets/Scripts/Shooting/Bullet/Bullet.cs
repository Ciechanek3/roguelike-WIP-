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

        public void ResetBullet()
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
            rb.velocity = Vector3.zero;
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            Collided?.Invoke();
        }
    }
    
}

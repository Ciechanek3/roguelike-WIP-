using System;
using Events;
using ObjectPooling;
using Settings;
using UnityEngine;

namespace Shooting.Weapons
{
    public abstract class WeaponBase : MonoBehaviour
    {
        [Header("Weapon elements")]
        [SerializeField] protected Bullet.Bullet bulletPrefab;
        [SerializeField] protected Transform barrel;
        [Header("Weapon stats")]
        [SerializeField] protected int ammoInMagazine;
        [SerializeField] protected float shootCooldown;
        [SerializeField] protected float bulletSpeedMultiplier;
        [SerializeField] protected float damage;
        

        protected int CurrentAmmoInMagazine;
        private ObjectPool<Bullet.Bullet> _bulletsPool;
        private Animator animator;
        private float lastAttackedTimer = -1;

        private void Start()
        {
            animator = GetComponent<Animator>();
            _bulletsPool = new ObjectPool<Bullet.Bullet>(bulletPrefab, ammoInMagazine);
            CurrentAmmoInMagazine = ammoInMagazine;
            EventManager.UpdateAmmo(CurrentAmmoInMagazine, ammoInMagazine);
        }

        private void OnEnable()
        {
            EventManager.OnShootInputEvent += OnShoot;
            EventManager.OnReloadInputEvent += Reload;
        }

        private void OnDisable()
        {
            EventManager.OnShootInputEvent -= OnShoot;
            EventManager.OnReloadInputEvent -= Reload;
        }

        protected void OnShoot()
        {
            if (Time.time <= lastAttackedTimer + shootCooldown)
            {
                return;
            }
            if (animator.GetBool("Reloading"))
            {
                return;
            }
            
            RaycastHit rHit;
            Transform cameraTransform = GameSettings.Instance.MainCamera.transform;
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out rHit));
            {
                if (CurrentAmmoInMagazine < 0)
                {
                    return;
                }
                CurrentAmmoInMagazine--;
                
                Bullet.Bullet currentBullet = _bulletsPool.TakeElementFromPool();
                currentBullet.transform.position = barrel.transform.position;
                currentBullet.Setup(rHit.point, bulletSpeedMultiplier, damage);
                EventManager.UpdateAmmo(CurrentAmmoInMagazine, ammoInMagazine);
                lastAttackedTimer = Time.time;
                currentBullet.Collided += () =>
                {
                    _bulletsPool.GetElementToPool(currentBullet);
                };
                animator.Play("Shoot");
            }
        }

        protected void Reload()
        {
            animator.SetBool("Reloading", true);
        }

        public virtual void OnReloadFinished()
        {
            animator.SetBool("Reloading", false);
        }
    }
}

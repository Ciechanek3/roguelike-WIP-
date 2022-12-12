using DG.Tweening;
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
        [SerializeField] protected int attacksPerSecond;
        [SerializeField] protected int reloadTime;
        [SerializeField] protected int bulletSpeed;
        [SerializeField] protected int damage;
        

        protected int CurrentAmmoInMagazine;
        protected ObjectPool<Bullet.Bullet> BulletsPool;

        private void Start()
        {
            BulletsPool = new ObjectPool<Bullet.Bullet>(bulletPrefab, ammoInMagazine);
            CurrentAmmoInMagazine = ammoInMagazine;
        }

        private void OnEnable()
        {
            EventManager.ShootInputEvent += Shoot;
        }

        private void OnDisable()
        {
            EventManager.ShootInputEvent -= Shoot;
        }

        protected void Shoot()
        {
            RaycastHit rHit;
            Transform cameraTransform = GameSettings.Instance.MainCamera.transform;
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out rHit));
            {
                if (CurrentAmmoInMagazine < 0)
                {
                    return;
                }
                
                CurrentAmmoInMagazine--;
                Bullet.Bullet currentBullet = BulletsPool.TakeElementFromPool();
                currentBullet.transform.position = barrel.transform.position;
                currentBullet.MoveTo(rHit.point);
                currentBullet.Collided += () =>
                {
                    BulletsPool.GetElementToPool(currentBullet);
                };
            }
        }

        protected abstract void Reload();
    }
}

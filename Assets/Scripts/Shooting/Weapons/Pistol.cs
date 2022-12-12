namespace Shooting.Weapons
{
    public class Pistol : WeaponBase
    {
        protected override void Reload()
        {
            CurrentAmmoInMagazine = ammoInMagazine;
        }
    }
}

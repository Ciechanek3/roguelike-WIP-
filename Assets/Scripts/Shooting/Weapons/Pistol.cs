namespace Shooting.Weapons
{
    public class Pistol : WeaponBase
    {
        public override void OnReloadFinished()
        {
            base.OnReloadFinished();
            CurrentAmmoInMagazine = ammoInMagazine;
        }
    }
}

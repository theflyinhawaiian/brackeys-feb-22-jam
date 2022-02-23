namespace Assets.Scripts.Weapons
{
    class ProjectileEnemyWeapon : SimpleProjectileWeapon
    {
        public ProjectileEnemyWeapon()
        {
            target = TargetType.Player;
            cooldown = 3f;
        }
    }
}

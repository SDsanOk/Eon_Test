public class GameConstants
{
    #region Time Constants

    public const float InitialInvulnerabilityTime = 3;
    public const float MinimumTimeBetweenBonuses = 15;
    public const float MaximumTimeBetweenBonuses = 60;
    public const float TankRespawnCooldown = 2;
    public const int DefaultShootCooldown = 1;
    public const float AttackEffectDuration = 10;

    #endregion

    public const int HpValueAfterPickingUpArmorBonus = 2;
    public const float BulletSpeedAfterPickingUpAttackBonus = DefaultBulletSpeed * 2;

    public const float DefaultBulletSpeed = 0.1f;
    public const int DefaultBulletDamage = 1;
    public const float DefaultMovementSpeed = 5f;
    public const float DefaultBulletSpawnOffset = 0.6f;
    public const float BonusSpawnCheckOccupancyRadius = 0.4f;
}
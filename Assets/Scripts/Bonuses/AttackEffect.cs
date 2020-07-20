using UnityEngine;

public class AttackEffect<T> : IEffect<T> where T: MonoBehaviour, IBonusTarget
{
    private float _effectDuration = GameConstants.AttackEffectDuration;
    public void ApplyEffect(IBonusTarget bonusTarget)
    {
        var gameObject = (bonusTarget as T)?.gameObject;
        if (gameObject != null && gameObject.TryGetComponent(out ShootController shootController))
        {
            shootController.SetBuletSpeed(GameConstants.BulletSpeedAfterPickingUpAttackBonus);

            TimeManagerFactory.GetTimeManager().ExecuteAfterCertainTime(_effectDuration, () =>
            {
                shootController.SetBuletSpeed(GameConstants.DefaultBulletSpeed);
            });
        }
    }
}
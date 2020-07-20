using UnityEngine;

public class ArmorEffect<T> : IEffect<T> where T: MonoBehaviour, IBonusTarget
{
    public void ApplyEffect(IBonusTarget bonusTarget)
    {
        var gameObject = (bonusTarget as T)?.gameObject;
        if (gameObject != null && gameObject.TryGetComponent(out Hp hp))
        {
            hp.SetHp(GameConstants.HpValueAfterPickingUpArmorBonus);
        }
    }
}
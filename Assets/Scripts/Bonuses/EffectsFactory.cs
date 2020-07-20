using System;
using UnityEngine;

public static class EffectsFactory
{
    public static IEffect<T> GetEffect<T>(BonusType bonusType) where T : MonoBehaviour, IBonusTarget
    {
        switch (bonusType)
        {
            case BonusType.Attack:
                return new AttackEffect<T>();
            case BonusType.Armor:
                return new ArmorEffect<T>();
            default:
                throw new ArgumentOutOfRangeException(nameof(bonusType), bonusType, null);
        }
    }
}
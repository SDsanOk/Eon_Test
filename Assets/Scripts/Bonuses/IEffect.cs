using UnityEngine;

public interface IEffect<T> where T: MonoBehaviour, IBonusTarget
{
    void ApplyEffect(IBonusTarget bonusTarget);
}
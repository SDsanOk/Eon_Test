using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField]
    private BonusType _bonusType;

    private Animator _animator;
    // Use this for initialization
    void Awake()
    {
        _animator = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out IBonusTarget bonusTarget))
        {
            ApplyEffect(bonusTarget);

            Destroy(this.gameObject);
        }
    }

    public void SetBonusType(BonusType bonusType)
    {
        this._bonusType = bonusType;
        _animator.SetInteger("BonusType", (int)this._bonusType);
    }

    public void ApplyEffect(IBonusTarget bonusTarget)
    {
        var effect = EffectsFactory.GetEffect<Tank>(this._bonusType);
        effect.ApplyEffect(bonusTarget);
    }
}

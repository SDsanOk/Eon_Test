using UnityEngine;

public class Hp : MonoBehaviour
{
    public delegate void OnDestroyDelegate(GameObject gameObject);

    public event OnDestroyDelegate OnDestroy;

    [SerializeField]
    private int healthPoints;

    [SerializeField]
    private bool _isInvulnerable;

    [SerializeField]
    private bool _shouldBeDestroyed;

    private PresentationController _presentationController;
    public bool IsInvulnerable => _isInvulnerable;

    // Start is called before the first frame update
    void Start()
    {
        _presentationController = gameObject.GetComponent<PresentationController>();
    }

    // Update is called once per frame
    void Update()
    {
        _presentationController?.SetShieldPresentation(healthPoints == GameConstants.HpValueAfterPickingUpArmorBonus);
    }

    public void SetInvulnerability(bool input)
    {
        _isInvulnerable = input;
    }

    public void SetHp(int hp)
    {
        healthPoints = hp;
    }

    public void TakeDamage(int damage)
    {
        if (!IsInvulnerable)
        {
            healthPoints -= damage;
        }

        if (healthPoints <= 0)
        {
            OnDestroy?.Invoke(this.gameObject);

            if (_shouldBeDestroyed)
            {
                Destroy(this.gameObject);
            }
        }
    }
}

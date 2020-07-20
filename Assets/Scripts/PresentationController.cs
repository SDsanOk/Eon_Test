using System.Collections.Generic;
using UnityEngine;

public class PresentationController : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;
    private Animator _animator;
    [SerializeField]
    private Animator _shieldAnimator;

    void Awake()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _animator = gameObject.GetComponent<Animator>();
    }

    public void Init(int playerNumber)
    {
        if (GameConstants.PlayerNumberColorsMapping.TryGetValue(playerNumber, out var color))
        {
            _spriteRenderer.color = color;
        }
    }

    public void UpdateDirectionPresentation(Vector2 direction)
    {
        _animator.SetFloat("x", direction.x);
        _animator.SetFloat("y", direction.y);
    }

    public void SetShieldPresentation(bool shield)
    {
        _shieldAnimator.SetBool("shield", shield);
    }
}

using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 _direction;
    private Rigidbody2D _rigidbody2D;

    private float speed;
    private int _damage = GameConstants.DefaultBulletDamage;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        _rigidbody2D.MovePosition(_rigidbody2D.position + _direction * speed);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var hp = collision.gameObject.GetComponent<Hp>();
        hp?.TakeDamage(_damage);

        Destroy(this.gameObject);
    }

    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }
    
    public void SetDirection(Vector2 directionVector2)
    {
        _direction = directionVector2;
        var angle = (Math.Acos(_direction.x) * 180 / Math.PI) + (Math.Asin(_direction.y) * 180 / Math.PI);
        if (_direction.x == 0) angle = angle - 90;
        transform.Rotate(new Vector3(0,0, (float) angle));
    }
}

using UnityEngine;

public class TankMotor : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    public void MoveWithVelocity(Vector2 velocity)
    {
        _rigidbody2D.MovePosition(_rigidbody2D.position + velocity * Time.fixedDeltaTime);
    }
}

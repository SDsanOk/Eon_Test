using UnityEngine;

public class MovementController : MonoBehaviour
{
    private float speed = GameConstants.DefaultMovementSpeed;

    private TankMotor _tankMotor;

    void Start()
    {
        _tankMotor = GetComponent<TankMotor>();
    }

    public void Move(Vector2 velocity)
    {
        _tankMotor?.MoveWithVelocity(velocity * speed);
    }
}

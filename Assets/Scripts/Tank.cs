using UnityEngine;

public class Tank : MonoBehaviour, IBonusTarget
{
    public int PlayerNumber;

    private string HorizontalAxisName => $"HorizontalP{PlayerNumber}";
    private string VerticalAxisName => $"VerticalP{PlayerNumber}";
    private string FireAxisName => $"FireP{PlayerNumber}";

    private MovementController _movementController;
    private ShootController _shootController;
    private PresentationController _presentationController;
    private Vector2 _shootDirection;

    void Start()
    {
        _movementController = GetComponent<MovementController>();
        _shootController = GetComponent<ShootController>();
        _presentationController = GetComponent<PresentationController>();

        _presentationController.Init(PlayerNumber);
    }
    
    void Update()
    {
        if (Input.GetAxisRaw(FireAxisName) != 0)
        {
            Shoot(_shootDirection);
        }
    }

    void FixedUpdate()
    {
        var horizontalAxisInput = Input.GetAxisRaw(HorizontalAxisName);
        var verticalAxisInput = Input.GetAxisRaw(VerticalAxisName);

        var velocity = new Vector2(horizontalAxisInput, verticalAxisInput);
        velocity.x = velocity.y != 0 ? 0 : velocity.x;
        velocity.y = velocity.x != 0 ? 0 : velocity.y;

        ResolveDirection(velocity);

        _movementController.Move(velocity);
    }

    private void ResolveDirection(Vector2 velocity)
    {
        if (velocity.x != 0)
        {
            _shootDirection = velocity.x > 0 ? Vector2.right : Vector2.left;
        }

        if (velocity.y != 0)
        {
            _shootDirection = velocity.y > 0 ? Vector2.up : Vector2.down;
        }

        _presentationController.UpdateDirectionPresentation(_shootDirection);
    }

    private void Shoot(Vector2 _direction)
    {
        _shootController.Shoot(_direction);
    }
}

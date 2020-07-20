using UnityEngine;

public class ShootController : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletPrefab;
    [SerializeField] private float _shootCooldown = GameConstants.DefaultShootCooldown;
    private float _bulletSpeed = GameConstants.DefaultBulletSpeed;
    private bool _canShoot;
    private TimeManager _timeManager;

    // Start is called before the first frame update
    void Start()
    {
        _timeManager = TimeManagerFactory.GetTimeManager();
        _canShoot = true;
    }

    public void SetBuletSpeed(float bulletSpeed)
    {
        _bulletSpeed = bulletSpeed;
    }

    public void Shoot(Vector2 direction)
    {
        if (_canShoot)
        {
            var bulletPosition = new Vector2(this.transform.position.x, this.transform.position.y) + (GameConstants.DefaultBulletSpawnOffset * direction);
            var bulletPrefab = Instantiate(_bulletPrefab, bulletPosition,Quaternion.identity);
            var bulletComponent = bulletPrefab.GetComponent<Bullet>();
            bulletComponent.SetDirection(direction);
            bulletComponent.SetSpeed(_bulletSpeed);

            _canShoot = false;

            _timeManager.ExecuteAfterCertainTime(_shootCooldown, () => _canShoot = true);
        }
    }
}

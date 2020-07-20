using UnityEngine;

public class BonusManager : MonoBehaviour
{
    [SerializeField] private GameObject BottomLeftCornerOfMap;
    [SerializeField] private GameObject TopRightCornerOfMap;
    [SerializeField] private GameObject BonusPrefab;

    private TimeManager timeManager;

    void Start()
    {
        timeManager = TimeManagerFactory.GetTimeManager();

        SpawnBonus();
    }

    private void SpawnBonus()
    {
        timeManager.ExecuteAfterCertainTime(Random.Range(GameConstants.MinimumTimeBetweenBonuses, GameConstants.MaximumTimeBetweenBonuses), () =>
        {
            Debug.Log("Bonus spawned");

            Vector2 potentialSpawnPoint;

            do
            {
                potentialSpawnPoint = GetRandomPointInsideMap();
            } while (!IsPointEligibleForSpawningBonus(potentialSpawnPoint));

            var bonusComponent = SpawnBonusPrefab(potentialSpawnPoint);
            var bonusType = (BonusType)Random.Range((int)BonusType.Attack, (int)BonusType.Armor+1);
            bonusComponent.SetBonusType(bonusType);

            SpawnBonus();
       });
    }

    private bool IsPointEligibleForSpawningBonus(Vector2 point)
    {
        var overlappingCollider = Physics2D.OverlapCircle(point, GameConstants.BonusSpawnCheckOccupancyRadius);
        return overlappingCollider == null;
    }

    private Bonus SpawnBonusPrefab(Vector2 position)
    {
        var bonusGameObject = Instantiate(BonusPrefab, position, Quaternion.identity);
        var bonusComponent = bonusGameObject.GetComponent<Bonus>();

        return bonusComponent;
    }

    private Vector2 GetRandomPointInsideMap()
    {
        return new Vector2(
            Random.Range(BottomLeftCornerOfMap.transform.position.x, TopRightCornerOfMap.transform.position.x),
            Random.Range(BottomLeftCornerOfMap.transform.position.y, TopRightCornerOfMap.transform.position.y)
            );
    }
}

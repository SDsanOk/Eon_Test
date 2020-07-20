using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] spawnPointObjects;

    [SerializeField] private GameObject[] eaglesGameObjects;

    private TimeManager timeManager;
    private IEnumerable<SpawnPoint> _spawnPoints;
    private Dictionary<int, Tank> _tanksDictionary;
    [SerializeField]
    private GameObject tankPrefab;

    void Start()
    {
        timeManager = TimeManagerFactory.GetTimeManager();
        timeManager.FlushScheduledActions();

        InitiateEagles();

        InitialTankSpawn();
    }

    private void InitiateEagles()
    {
        foreach (var eagleGameObject in eaglesGameObjects)
        {
            SubscribeOnEagleDestroyEvent(eagleGameObject.GetComponent<Eagle>());
        }
    }

    private void InitialTankSpawn()
    {
        _tanksDictionary = new Dictionary<int, Tank>();
        _spawnPoints = spawnPointObjects.Select(_ => _.GetComponent<SpawnPoint>());

        foreach (var spawnPoint in _spawnPoints)
        {
            var playerNUmber = spawnPoint.PlayerNumber;
            _tanksDictionary.Add(playerNUmber, SpawnTank(playerNUmber));
        }
    }

    private Tank SpawnTank(int playerNumber)
    {
        var specificSpawnPoint = _spawnPoints.First(_ => _.PlayerNumber.Equals(playerNumber));
        var tankGameObject = Instantiate(tankPrefab, specificSpawnPoint.transform.position, Quaternion.identity);
        var tank = tankGameObject.GetComponent<Tank>();
        tank.PlayerNumber = playerNumber;

        SetInitialInvulnerability(tankGameObject.GetComponent<Hp>());

        SubscribeOnTankDestroyEvent(tank);

        return tank;
    }

    private void SetInitialInvulnerability(Hp tankHp)
    {
        tankHp.SetInvulnerability(true);
        timeManager.ExecuteAfterCertainTime(GameConstants.InitialInvulnerabilityTime, () =>
        {
            tankHp.SetInvulnerability(false);
        });
    }

    private void SubscribeOnDestroyEvent<T>(T _gameObject, Hp.OnDestroyDelegate destroyDelegate) where T: MonoBehaviour
    {
        var hpComponent = _gameObject.gameObject.GetComponent<Hp>();
        if (hpComponent != null)
        {
            hpComponent.OnDestroy += destroyDelegate;
        }
    }

    private void SubscribeOnEagleDestroyEvent(Eagle eagle)
    {
        SubscribeOnDestroyEvent(eagle, Eagle_OnOnDestroy);
    }

    private void SubscribeOnTankDestroyEvent(Tank tank)
    {
        SubscribeOnDestroyEvent(tank, Tank_OnDestroy);
    }

    private void Tank_OnDestroy(GameObject _gameObject)
    {
        var tank = _gameObject.GetComponent<Tank>();
        var playerNumber = tank.PlayerNumber;
        timeManager?.ExecuteAfterCertainTime(GameConstants.TankRespawnCooldown, () =>
        {
            _tanksDictionary[playerNumber] = SpawnTank(playerNumber);
        });
    }

    private void Eagle_OnOnDestroy(GameObject _gameobject)
    {
        var looser = _gameobject.GetComponent<Eagle>().PlayerNumber;
        Debug.Log($"Player {looser} has lost! Gz to winner.");

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

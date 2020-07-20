using UnityEngine;

public class TimeManagerUpdater : MonoBehaviour
{
    private TimeManager _timeManager;

    void Start()
    {
        _timeManager = TimeManagerFactory.GetTimeManager();
    }

    void Update()
    {
        _timeManager.Tick(Time.deltaTime);
    }
}

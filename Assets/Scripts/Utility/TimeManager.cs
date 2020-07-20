using System;
using System.Collections.Generic;
using System.Linq;

public class TimeManager
{
    private readonly Dictionary<Action, float> _scheduledActions = new Dictionary<Action, float>();

    public void Tick(float time)
    {
        var keys = _scheduledActions.Keys.ToList();

        foreach (var key in keys)
        {
            _scheduledActions[key] -= time;
        }

        CheckExpiredActions();
    }

    private void CheckExpiredActions()
    {
        var expiredActions = new List<Action>();
        foreach (var scheduledAction in _scheduledActions)
        {
            if (scheduledAction.Value <= 0)
            {
                expiredActions.Add(scheduledAction.Key);
            }
        }

        foreach (var expiredAction in expiredActions)
        {
            _scheduledActions.Remove(expiredAction);
            expiredAction.Invoke();
        }
    }

    public void ExecuteAfterCertainTime(float time, Action action)
    {
        _scheduledActions.Add(action, time);
    }
}

using System;

public class TimeManagerFactory
{
    private static readonly Lazy<TimeManager> Lazy = new Lazy<TimeManager>(() => new TimeManager());
    public static TimeManager GetTimeManager()
    {
        return Lazy.Value;
    }
}
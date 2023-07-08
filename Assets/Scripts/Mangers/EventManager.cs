using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    public delegate void HourIncreaseEvent();
    public static event HourIncreaseEvent OnHourIncreaseEvent;
    
    public delegate void MinuteIncreaseEvent();
    public static event MinuteIncreaseEvent OnMinuteIncreaseEvent;
    
    public delegate void DayIncreaseEvent();
    public static event DayIncreaseEvent OnDayIncreaseEvent;
    
    
    public static void TriggerHourIncreaseEvent()
    {
        if (OnHourIncreaseEvent != null)
            OnHourIncreaseEvent();
        else
            Debug.LogWarning("No subscribers for HourIncrease event");
    }
    
    public static void TriggerMinuteIncreaseEvent()
    {
        if (OnMinuteIncreaseEvent != null)
            OnMinuteIncreaseEvent();
        else
            Debug.LogWarning("No subscribers for MinuteIncrease event");
    }
    
    public static void TriggerDayIncreaseEvent()
    {
        if (OnDayIncreaseEvent != null)
            OnDayIncreaseEvent();
        else
            Debug.LogWarning("No subscribers for DayIncrease event");
    }

    private void OnDisable()
    {
        OnHourIncreaseEvent = null;
        OnMinuteIncreaseEvent = null;
        OnDayIncreaseEvent = null;
    }
}

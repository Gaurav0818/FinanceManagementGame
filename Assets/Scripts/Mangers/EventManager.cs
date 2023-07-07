using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    public delegate void HourIncreaseEvent();
    public static event HourIncreaseEvent OnHourIncreaseEvent;
    
    public static void TriggerHourIncreaseEvent()
    {
        if (OnHourIncreaseEvent != null)
            OnHourIncreaseEvent();
        else
            Debug.LogWarning("No subscribers for HourIncrease event");
    }

    private void OnDisable()
    {
        OnHourIncreaseEvent = null;
    }
}

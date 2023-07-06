using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineManager : Singleton<TimelineManager>
{
    [System.Serializable]
    public enum DayType
    {
        WeekDay,
        WeekEnd
    }

    [System.Serializable]
    public struct DayEntry
    {
        public DayType typeOfDay;
        public List<Scenario> scenarioList;
    }

    public List<DayEntry> days;

}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public struct ScenarioData
    {
        public Scenario scenario;
        public int StartTime;
    }

    [System.Serializable]
    public struct DayEntry
    {
        public int dayNumber;
        public DayType typeOfDay;
        public List<ScenarioData> scenarioList;
    }
    
    [System.Serializable]
    public enum GameMode
    {
        WeekBased,
        MonthBased
    }
    
    public GameMode gameMode;
    public List<DayEntry> days;

    private void Awake()
    {
        if(gameMode == GameMode.WeekBased)
            InitializeDays(7);
        else
            InitializeDays(30);
    }

    
    private void InitializeDays(int dayCount)
    {
        for (int i = 0; i < dayCount; i++)
        {
            DayEntry day = new DayEntry();
            day.dayNumber = i+1;
            
            // statement to find 7n or 7n -1 day of the week
            // i % 7 will give day of the week from 0 to 6
            // 7 - i % 7 will be smaller then or equal to 2 for weekends and greater then 1 for weekdays 

            if (7 - i % 7 <= 2)
                day.typeOfDay = DayType.WeekEnd;
            else
                day.typeOfDay = DayType.WeekDay;
            
            days.Add(day);
        }
    }
    
}

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
    
    private DayEntry m_CurrentDay;
    private int m_TimeInMin;
    private int m_TimeInHour;
    [SerializeField] private int m_StartTimeOfDay = 8;
    [SerializeField] private int m_EndTimeOfDay = 20;
    [SerializeField] private int m_MinInEachHr = 60;
    
    private void Awake()
    {
        if(gameMode == GameMode.WeekBased)
            InitializeDays(7);
        
        else if(gameMode == GameMode.MonthBased)
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
        
        SetCurrentDay(days[0]);
        StartCoroutine(StartTimer());
        StartCoroutine(StartDay());
    }

    private void SetCurrentDay(DayEntry day)
    {
        m_CurrentDay = day;
    }

    public  DayEntry GetCurrentDay()
    {
        return m_CurrentDay;
    }

    private IEnumerator StartTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            m_TimeInMin++;
            if (m_TimeInMin >= m_MinInEachHr)
            {
                m_TimeInMin = 0;
                m_TimeInHour++;
                HourChanged();
            }
        }
        yield return null;
    }

    private IEnumerator StartDay()
    {
        m_TimeInMin = 0;
        m_TimeInHour = m_StartTimeOfDay;
        while (true)
        {
            yield return new WaitForSeconds(1);
            if(m_TimeInHour >= m_EndTimeOfDay)
                break;
        }
        StartNextDay();
        yield return null;
    }
    
    private void StartNextDay()
    {
        int index = days.IndexOf(m_CurrentDay);
        if (index + 1 < days.Count)
        {
            SetCurrentDay(days[index + 1]);
            StartCoroutine(StartDay());
        }
    }

    public void SkipHour()
    {
        m_TimeInHour++;
        HourChanged();
    }

    private void HourChanged()
    {
        EventManager.TriggerHourIncreaseEvent();
    }

    public int GetCurrentTimeInHr()
    {
        return m_TimeInHour;
    }
    
}

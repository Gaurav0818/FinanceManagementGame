using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class TimelineManager : Singleton<TimelineManager>
{
    [System.Serializable]
    public enum DayType
    {
        WeekDay,
        WeekEnd,
    }
    
    [System.Serializable]
    public enum MandatoryScenarioDayType
    {
        WeekDay,
        WeekEnd,
        Both
    }
    
    [System.Serializable]
    public struct MandatoryScenario
    {
        public MandatoryScenarioDayType type;
        public ScenarioData scenario;
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
    
    
    public List<MandatoryScenario> mandatoryScenario;
    
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
            
            day.scenarioList = AddMandatoryScenario(day.typeOfDay);
            
            days.Add(day);
        }
        
        SetCurrentDay(days[0]);
        
        StartDay();
        
        StartCoroutine(StartTimer());
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

    private void StartDay()
    {
        m_TimeInMin = 0;
        m_TimeInHour = m_StartTimeOfDay;
        
        m_CurrentDay.scenarioList.Sort((a, b) => a.StartTime.CompareTo(b.StartTime));
        
        m_TimeInHour = m_CurrentDay.scenarioList[0].StartTime;
        
        ScenarioManger.Instance.StartScenario(m_CurrentDay.scenarioList[0]);
        
    }
    
    private void StartNextDay()
    {
        int index = days.IndexOf(m_CurrentDay);
        if (index + 1 < days.Count)
        {
            SetCurrentDay(days[index + 1]);
            StartDay();
        }
    }

    public void SkipHour()
    {
        m_TimeInHour++;
        if(m_TimeInHour >= m_EndTimeOfDay)
            StartNextDay();
        else 
            HourChanged();
    }

    private void HourChanged()
    {
        EventManager.TriggerHourIncreaseEvent();
        
        if(m_TimeInHour >= m_EndTimeOfDay)
            StartNextDay();
    }

    public int GetCurrentTimeInHr()
    {
        return m_TimeInHour;
    }

    private List<ScenarioData> AddMandatoryScenario(DayType type)
    {
        List<ScenarioData> scenarioList = new List<ScenarioData>();
        MandatoryScenarioDayType dayType;
        if(type == DayType.WeekDay)
            dayType = MandatoryScenarioDayType.WeekDay;
        else
            dayType = MandatoryScenarioDayType.WeekEnd;
            
        foreach (var scenario in mandatoryScenario)
        {
            if (scenario.type == MandatoryScenarioDayType.Both || scenario.type == dayType)
                scenarioList.Add(scenario.scenario);
        }

        return scenarioList;
    }
    
}

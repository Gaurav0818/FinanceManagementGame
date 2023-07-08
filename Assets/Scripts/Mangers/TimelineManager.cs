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
    public enum DayTypeInWhichScenarioCanBeUsed
    {
        WeekDay,
        WeekEnd,
        Both
    }
    
    [System.Serializable]
    public struct MandatoryScenario
    {
        public DayTypeInWhichScenarioCanBeUsed type;
        public ScenarioData scenario;
    }

    [System.Serializable]
     public struct ScenarioData
    {
        public Scenario scenario;
        public int StartTime;
        public bool isDone;
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
        EventManager.OnStartGameEvent += StartGameEvent;
    }
    
    public void StartGameEvent()
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
            yield return new WaitForSeconds(m_MinInEachHr/60f);
            m_TimeInMin++;
            EventManager.TriggerMinuteIncreaseEvent();

            if (m_TimeInMin >= 60)
            {
                m_TimeInMin = 0;
                m_TimeInHour++;
                HourChanged();
            }
        }
    }

    private void StartDay()
    {
        m_TimeInMin = 0;
        m_TimeInHour = m_StartTimeOfDay;
        
        m_CurrentDay.scenarioList.Sort((a, b) => a.StartTime.CompareTo(b.StartTime));

        m_TimeInHour = m_CurrentDay.scenarioList[0].StartTime;
        
        ScenarioManger.Instance.StartScenario(m_CurrentDay.scenarioList[0]);
        
    }
    
    public void StartNextDay()
    {
        int index = days.IndexOf(m_CurrentDay);
        if (index + 1 < days.Count)
        {
            SetCurrentDay(days[index + 1]);
            EventManager.TriggerDayIncreaseEvent();
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
    
    public int GetCurrentTimeInMin()
    {
        return m_TimeInMin;
    }

    private List<ScenarioData> AddMandatoryScenario(DayType type)
    {
        List<ScenarioData> scenarioList = new List<ScenarioData>();
        DayTypeInWhichScenarioCanBeUsed canBeUsed;
        if(type == DayType.WeekDay)
            canBeUsed = DayTypeInWhichScenarioCanBeUsed.WeekDay;
        else
            canBeUsed = DayTypeInWhichScenarioCanBeUsed.WeekEnd;
            
        foreach (var scenario in mandatoryScenario)
        {
            if (scenario.type == DayTypeInWhichScenarioCanBeUsed.Both || scenario.type == canBeUsed)
                scenarioList.Add(scenario.scenario);
        }
        
        return scenarioList;
    }

    public string GetCurrentTimeInString()
    {
        return m_TimeInHour.ToString("00") + ": " + m_TimeInMin.ToString("00");
    }

    public List<int> GetAllPossibleDays(DayTypeInWhichScenarioCanBeUsed type)
    {
        List<int> possibleDays = new List<int>();
        DayTypeInWhichScenarioCanBeUsed dayType;

        for (int i = m_CurrentDay.dayNumber-1; i < days.Count; i++)
        {
            if(DayType.WeekDay == days[i].typeOfDay)
                dayType = DayTypeInWhichScenarioCanBeUsed.WeekDay;
            else
                dayType = DayTypeInWhichScenarioCanBeUsed.WeekEnd;
            
            if (type == DayTypeInWhichScenarioCanBeUsed.Both || type == dayType)
                possibleDays.Add(days[i].dayNumber);
        }

        return possibleDays;
    }

    public List<int> GetFreeTimeOfDay(int dayNumber,int durationOfFreeTime)
    {
        DayEntry day = days[dayNumber-1];
        
        List<ScenarioData> scenarioList = day.scenarioList;

        List<int> freeTime = new List<int>();
        
        for(int i = m_StartTimeOfDay; i < m_EndTimeOfDay; i++)
        {
            freeTime.Add(i);
        }
        
        foreach (var scenario in scenarioList)
        {
            for (int i = scenario.StartTime; i < scenario.StartTime + (int)scenario.scenario.GetDuration(); i++)
            {
                freeTime.Remove(i);
            }
        }

        List<int> newFreeTime = new List<int>();
        
        for(int i = 0; i< freeTime.Count; i++)
        {
            newFreeTime.Add(freeTime[i]);
        }
        
        for(int i = 0; i< freeTime.Count; i++)
        {
            for(int j = freeTime[i]; j < freeTime[i] + durationOfFreeTime; j++)
            {
                if (!freeTime.Contains(j))
                {
                    newFreeTime.Remove(freeTime[i]);
                    break;
                }
            }
        }

        return newFreeTime;
    }
    
    public void AddScenarioToDay(int dayNumber,int time ,Scenario scenario)
    {
        ScenarioData scenarioData = new ScenarioData();
        scenarioData.scenario = scenario;
        scenarioData.StartTime = time;
        
        days[dayNumber-1].scenarioList.Add(scenarioData);
        m_CurrentDay.scenarioList.Sort((a, b) => a.StartTime.CompareTo(b.StartTime));
    }

    public void MarkScenarioAsDone(ScenarioData scenario)
    {
        for (int i = 0; i < m_CurrentDay.scenarioList.Count; i++)
        {
            if (m_CurrentDay.scenarioList[i].StartTime == scenario.StartTime)
            {
                ScenarioData modifiedEntity = m_CurrentDay.scenarioList[i];
                modifiedEntity.isDone = true;
                m_CurrentDay.scenarioList[i] = modifiedEntity;
                break; 
            }
        }
    }
    
    
    
}

using System;
using System.Net.Mime;
using System.Xml;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : Singleton<UiManager>
{
    [Header("Currency And Satisfaction score")] 
    [SerializeField] private TextMeshProUGUI m_CurrencyAmountTextUI;
    [SerializeField] private TextMeshProUGUI m_SatisfactionScoreTextUI;
    [SerializeField] private FloatingIndicator m_FloatingIndicatorCurrency;
    [SerializeField] private FloatingIndicator m_FloatingIndicatorSatisfactionScore;
    
    [Header("Day and Time")]
    [SerializeField] private TextMeshProUGUI m_ClockTextUI;
    [SerializeField] private TextMeshProUGUI m_DayIndeicatorTextUI;
    
    [Header("Day and Time Selection Part")]
    [SerializeField] private GameObject m_DaySelectionPanel;
    [SerializeField] private GameObject m_DaySelectionGrid;
    [SerializeField] private GameObject m_DaySelectionPrefab;
    private ObjectPool m_DaySelectionPool;
    
    [SerializeField] private GameObject m_TimeSelectionPanel;
    [SerializeField] private GameObject m_TimeSelectionGrid;
    [SerializeField] private GameObject m_TimeSelectionPrefab;
    private ObjectPool m_TimeSelectionPool;
    
    [Header("Calender")]
    [SerializeField] private GameObject m_CalenderGrid;
    [SerializeField] private GameObject m_CalenderDayPrefab;
    private ObjectPool m_CalenderDayPool;
    
    [Header("ScenarioList")]
    [SerializeField] private GameObject m_ScenarioListGrid;
    [SerializeField] private GameObject m_ScenarioListEntryPrefab;
    private ObjectPool m_ScenarioDayPool;
    [SerializeField] private Image ScenarioImage;
    
    [Header("Obsatcle")]
    [SerializeField] private GameObject m_ObstaclePanel;
    [SerializeField] private TextMeshProUGUI m_QuestionText;
    [SerializeField] private TextMeshProUGUI m_AnsTrueText;
    [SerializeField] private TextMeshProUGUI m_AnsFalseText;


    private void Awake()
    {
        EventManager.OnMinuteIncreaseEvent += MinuteIncreaseEvent;
        EventManager.OnHourIncreaseEvent += HourIncreaseEvent;
        EventManager.OnDayIncreaseEvent += DayIncreaseEvent;
        EventManager.OnStartGameEvent += StartGameEvent;
    }

    private void StartGameEvent()
    {
        m_CalenderDayPool = m_CalenderGrid.AddComponent<ObjectPool>();
        m_ScenarioDayPool = m_ScenarioListGrid.AddComponent<ObjectPool>();
        m_TimeSelectionPool = m_TimeSelectionGrid.AddComponent<ObjectPool>();
        m_DaySelectionPool = m_DaySelectionGrid.AddComponent<ObjectPool>();

        FillCalender();
        FillDailySchedule();
        FillDaySelection();
        FillTimeSelection();
        
        RefreshClock();
        RefreshDayIndicator();
        
        CloseObstacle();
        CloseDaySchedulePanel();
        CloseTimeSchedulePanel();
    }
    
    public void OpenObstacle(Obstacle obstacle)
    {
        m_QuestionText.text = obstacle.GetQuestion();
        m_AnsTrueText.text = obstacle.GetAnsTrue();
        m_AnsFalseText.text = obstacle.GetAnsFalse();
        
        m_ObstaclePanel.SetActive(true);
    }
    
    public void CloseObstacle()
    {
        m_ObstaclePanel.SetActive(false);
    }

    public void AnsSelectedTrue()
    {
        ObstacleManger.Instance.TrueSelected();
    }
    public void AnsSelectedFalse()
    {
        ObstacleManger.Instance.FalseSelected();
    }

    private void FillCalender()
    {
        m_CalenderDayPool.InitializePool(m_CalenderDayPrefab, m_CalenderGrid.transform, TimelineManager.Instance.days.Count);
        
        RefreshCalender();
    }
    
    private void FillDailySchedule()
    {
        m_ScenarioDayPool.InitializePool(m_ScenarioListEntryPrefab, m_ScenarioListGrid.transform, TimelineManager.Instance.days.Count);
        RefreshSchedule();
    }
    
    public void FillDaySelection()
    {
        m_DaySelectionPool.InitializePool(m_DaySelectionPrefab, m_DaySelectionGrid.transform, TimelineManager.Instance.days.Count);
        
        RefreshDaySelection();
    }

    public void FillTimeSelection()
    {
        m_TimeSelectionPool.InitializePool(m_TimeSelectionPrefab, m_TimeSelectionGrid.transform, 24);
        
        RefreshTimeSelection();
    }

    public void RefreshCalender()
    {
        m_CalenderDayPool.RefreshPool();
        
        foreach (var day in TimelineManager.Instance.days)
        {
            GameObject entity = m_CalenderDayPool.GetObject();
            TextMeshProUGUI  buttonText = entity.GetComponentInChildren<TextMeshProUGUI >();
            buttonText.text = day.dayNumber.ToString();

            if(day.typeOfDay == TimelineManager.DayType.WeekEnd)
                entity.GetComponent<Image>().color = Color.red;
            else
                entity.GetComponent<Image>().color = Color.white;
            
            if(TimelineManager.Instance.GetCurrentDay().dayNumber == day.dayNumber)
                entity.GetComponent<Image>().color = Color.green;
        }
    }

    public void RefreshSchedule()
    {
        if (m_ScenarioDayPool == null)
            return;
        m_ScenarioDayPool.RefreshPool();
        var defaultSize =  m_ScenarioListEntryPrefab.GetComponent<RectTransform>().sizeDelta;
        foreach (var scenario in TimelineManager.Instance.GetCurrentDay().scenarioList)
        {

            GameObject entity = m_ScenarioDayPool.GetObject();
            TextMeshProUGUI  buttonText = entity.GetComponentInChildren<TextMeshProUGUI >();
            buttonText.text = scenario.StartTime + ":00  -  " + (scenario.StartTime + scenario.scenario.GetDuration()) + ":00   for "  + scenario.scenario.name;
            entity.GetComponent<RectTransform>().sizeDelta = defaultSize;
            entity.GetComponent<RectTransform>().sizeDelta *= new Vector2(1, scenario.scenario.GetDuration());
            
            if(scenario.scenario == ScenarioManger.Instance.GetCurrentScenario())
                entity.GetComponent<Image>().color = Color.green;
            else
                entity.GetComponent<Image>().color = Color.white;

            if (scenario.isDone)
                entity.GetComponent<Image>().color *= new Color(0f, 0f, 0f, 0.5f);
        }
    }
    
    public void RefreshDaySelection()
    {
        m_DaySelectionPool.RefreshPool();
        
        foreach (var day in ObstacleManger.Instance.possibleDays)
        {
            GameObject entity = m_DaySelectionPool.GetObject();
            TextMeshProUGUI  buttonText = entity.GetComponentInChildren<TextMeshProUGUI >();
            buttonText.text = day.ToString();
            entity.GetComponent<DateEntry>().number = day;
            
            entity.GetComponent<Image>().color = Color.white;
        }
    }
    
    public void RefreshTimeSelection()
    {
        m_TimeSelectionPool.RefreshPool();
        
        foreach (var timeSlot in ObstacleManger.Instance.freeTimeOfDay)
        {
            GameObject entity = m_TimeSelectionPool.GetObject();
            TextMeshProUGUI  buttonText = entity.GetComponentInChildren<TextMeshProUGUI >();
            buttonText.text = timeSlot.ToString();
            entity.GetComponent<TimeEntry>().number = timeSlot;
            
            entity.GetComponent<Image>().color = Color.white;
        }
    }
    
    

    public void SetScenarioImage()
    {
        ScenarioImage.sprite = ScenarioManger.Instance.GetCurrentScenario().m_ScenarioImage;
    }
    
    
    

    private void MinuteIncreaseEvent()
    {
        RefreshClock();
    }    
    private void HourIncreaseEvent()
    {
        RefreshSchedule();
    }
    private void DayIncreaseEvent()
    {
        RefreshCalender();
        RefreshDayIndicator();
    }
    
    
    
    private void RefreshClock()
    {
        m_ClockTextUI.text = TimelineManager.Instance.GetCurrentTimeInString();
    }    
    private void RefreshDayIndicator()
    {
        m_DayIndeicatorTextUI.text =  " Day :" + TimelineManager.Instance.GetCurrentDay().dayNumber.ToString("00");
    }
    
    
    
    public void OpenDaySchedulePanel()
    {
        RefreshDaySelection();
        m_DaySelectionPanel.SetActive(true);
    }
    
    private void CloseDaySchedulePanel()
    {
        m_DaySelectionPanel.SetActive(false);
    }
    
    
    
    public void OpenTimeSchedulePanel()
    {
        RefreshTimeSelection();
        m_TimeSelectionPanel.SetActive(true);
    }
    
    private void CloseTimeSchedulePanel()
    {
        m_TimeSelectionPanel.SetActive(false);
    }
    
    
    
    public void SelectedDaySchedule(int day)
    {
        CloseDaySchedulePanel();
        ObstacleManger.Instance.SelectedDaySchedule(day);
    }
    public void SelectedTimeSchedule(int time)
    {
        CloseTimeSchedulePanel();
        RefreshSchedule();
        ObstacleManger.Instance.SelectedTimeSchedule(time);
    }
    
    public void RefreshCurrencyAmount()
    {
        m_CurrencyAmountTextUI.text = "Currency :" + ScoreManger.Instance.GetCurrency().ToString("0000");
    }
    
    public void RefreshSatisfactionScore()
    {
        m_SatisfactionScoreTextUI.text = "SatisfactionScore :" + ScoreManger.Instance.GetSatisfactionScore().ToString("000");
    }
    
    public void FloatingUIForCurrency(string text, Color color)
    {
        m_FloatingIndicatorCurrency.ShowFloatingText(text, color);
    }
    
    public void FloatingUIForSatisfactionScore(string text, Color color)
    {
        m_FloatingIndicatorSatisfactionScore.ShowFloatingText(text, color);
    }
}

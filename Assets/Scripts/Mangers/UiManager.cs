using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : Singleton<UiManager>
{
    
    [Header("Calender")]
    [SerializeField] private GameObject m_CalenderGrid;
    [SerializeField] private GameObject m_CalenderDayPrefab;
    private ObjectPool m_CalenderDayPool;
    
    [Header("ScenarioList")]
    [SerializeField] private GameObject m_ScenarioListGrid;
    [SerializeField] private GameObject m_ScenarioListEntryPrefab;
    private ObjectPool m_ScenarioDayPool;
    [SerializeField] private Image ScenarioImage;
    
    private void Start()
    {
        m_CalenderDayPool = m_CalenderGrid.AddComponent<ObjectPool>();
        m_ScenarioDayPool = m_ScenarioListGrid.AddComponent<ObjectPool>();
        FillCalender();
        FillDailySchedule();
    }

    private void FillCalender()
    {
        m_CalenderDayPool.InitializePool(m_CalenderDayPrefab, m_CalenderGrid.transform, TimelineManager.Instance.days.Count);
        
        RefreshCalender();
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
            
        }
    }
    
    
    private void FillDailySchedule()
    {
        m_ScenarioDayPool.InitializePool(m_ScenarioListEntryPrefab, m_ScenarioListGrid.transform, TimelineManager.Instance.days.Count);
        
        RefreshSchedule();
    }
    
    public void RefreshSchedule()
    {
        m_ScenarioDayPool.RefreshPool();
        
        foreach (var scenario in TimelineManager.Instance.GetCurrentDay().scenarioList)
        {

            GameObject entity = m_ScenarioDayPool.GetObject();
            TextMeshProUGUI  buttonText = entity.GetComponentInChildren<TextMeshProUGUI >();
            buttonText.text = scenario.StartTime + ":00  -  " + (scenario.StartTime + scenario.scenario.GetDuration()) + ":00   for "  + scenario.scenario.name;

            entity.GetComponent<RectTransform>().sizeDelta *= new Vector2(1, scenario.scenario.GetDuration());
            
        }
    }

    public void SetScenarioImage()
    {
        ScenarioImage.sprite = ScenarioManger.Instance.GetCurrentScenario().m_ScenarioImage;
    }
}

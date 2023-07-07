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
    
    
    private void Start()
    {
        m_CalenderDayPool = new ObjectPool();
        FillCalender();
       // FillDailySchedule();
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
        foreach (var scenario in TimelineManager.Instance.GetCurrentDay().scenarioList)
        {
            GameObject entity = Instantiate(m_ScenarioListEntryPrefab, m_ScenarioListGrid.transform);
            TextMeshProUGUI  buttonText = entity.GetComponentInChildren<TextMeshProUGUI >();
            buttonText.text = scenario.StartTime + ":00  -  " + (scenario.StartTime + scenario.scenario.GetDuration()) + ":00   for "  + scenario.scenario.name;

            var localScale = entity.transform.localScale;
            localScale = new Vector3(localScale.x, localScale.y * scenario.scenario.GetDuration(), localScale.z );
            entity.transform.localScale = localScale;
        }
    }
}

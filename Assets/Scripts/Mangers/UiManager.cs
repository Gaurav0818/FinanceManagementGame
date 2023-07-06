using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : Singleton<UiManager>
{
    
    [SerializeField] private GameObject m_CalenderGrid;
    [SerializeField] private GameObject m_CalenderDayPrefab;
    private void Start()
    {
        FillCalender();
    }

    private void FillCalender()
    {
        foreach (var day in TimelineManager.Instance.days)
        {
            GameObject entity = Instantiate(m_CalenderDayPrefab, m_CalenderGrid.transform);
            TextMeshProUGUI  buttonText = entity.GetComponentInChildren<TextMeshProUGUI >();
            buttonText.text = day.dayNumber.ToString();
            
            if(day.typeOfDay == TimelineManager.DayType.WeekEnd)
                entity.GetComponent<Image>().color = Color.red;
            
        }
    }
}

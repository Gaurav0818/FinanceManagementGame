using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeEntry : MonoBehaviour
{
    public int number;

    public void SelectAnswer()
    {
        UiManager.Instance.SelectedTimeSchedule(number);
    }
    
}

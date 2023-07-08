using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateEntry : MonoBehaviour
{
    public int number;

    public void SelectAnswer()
    {
        UiManager.Instance.SelectedDaySchedule(number);
    }
}

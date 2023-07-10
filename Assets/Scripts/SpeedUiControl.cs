using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeedUiControl : MonoBehaviour
{

    public TextMeshProUGUI text;

    public void DecreaseSpeed()
    {
        TimelineManager.Instance.DecrementTimeScale();
        RefreshSpeed();
    }
    
    public void IncreaseSpeed()
    {
        TimelineManager.Instance.IncrementTimeScale();
        RefreshSpeed();
    }
    
    private void RefreshSpeed()
    {
        text.text = TimelineManager.Instance.GetCurrentTimeScale().ToString("0.00");
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManger : Singleton<ScoreManger>
{

    #region CurrencyScore
    
    [Header("CurrencyScore")]
    [SerializeField] private int m_CurrencyAmount;
    public int GetCurrency()
    {
        return m_CurrencyAmount;
    }

    public void AddCurrency(int score)
    {
        m_CurrencyAmount += score;
        UiManager.Instance.RefreshCurrencyAmount();
        UiManager.Instance.FloatingUIForCurrency("+"+score.ToString(), Color.green);
    }
    
    public void DecreaseCurrency(int score)
    {
        m_CurrencyAmount -= score;
        UiManager.Instance.RefreshCurrencyAmount();
        UiManager.Instance.FloatingUIForCurrency("-"+score.ToString(), Color.red);
    }

    #endregion

    #region SatisfactionScore
    
    [Header("SatisfactionScore")]
    [SerializeField] private int m_SatisfactionScore;
    public int GetSatisfactionScore()
    {
        return m_SatisfactionScore;
    }

    public void AddSatisfactionScore(int score)
    {
        m_SatisfactionScore += score;
        UiManager.Instance.RefreshSatisfactionScore();
        UiManager.Instance.FloatingUIForSatisfactionScore("+"+score.ToString(), Color.green);

    }
    
    public void DecreaseSatisfactionScore(int score)
    {
        m_SatisfactionScore -= score;
        UiManager.Instance.RefreshSatisfactionScore();
        UiManager.Instance.FloatingUIForSatisfactionScore("-"+score.ToString(), Color.red);
    }
    
    #endregion
    
}

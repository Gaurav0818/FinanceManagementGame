using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
    
    public void DecreaseCurrency(int score)
    {
        m_CurrencyAmount -= score;
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
    }
    
    public void DecreaseSatisfactionScore(int score)
    {
        m_SatisfactionScore -= score;
    }
    
    #endregion
    
}

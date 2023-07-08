using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingIndicator : MonoBehaviour
{
    public float floatSpeed = 1f;   
    public float duration = 2f;     

    public TextMeshProUGUI textComponent;
    private RectTransform re;
    
    private Vector2 startPosition;

    private void Awake()
    {
        re = textComponent.GetComponent<RectTransform>();
        startPosition = re.anchoredPosition;
        
        textComponent.gameObject.SetActive(false);
    }

    public void ShowFloatingText(string text, Color color)
    {
        re.anchoredPosition = startPosition;
        textComponent.text = text;
        textComponent.color = color;
        
        textComponent.gameObject.SetActive(true);
        StartCoroutine(FloatUpwards());
    }

    private IEnumerator FloatUpwards()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            re.anchoredPosition += new Vector2(0, 50 * floatSpeed * Time.deltaTime);

            elapsedTime += Time.deltaTime;

            yield return null;
        }
        
        textComponent.text = string.Empty;
        textComponent.gameObject.SetActive(false);
    }
}

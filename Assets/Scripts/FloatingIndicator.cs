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

    private void Awake()
    {
        textComponent.gameObject.SetActive(false);
    }

    public void ShowFloatingText(string text, Vector3 startPosition, Color color)
    {
        transform.position = startPosition;
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
            RectTransform re = textComponent.GetComponent<RectTransform>();
            Vector2 newPosition = re.position + Vector3.forward * floatSpeed * Time.deltaTime;
            transform.position = newPosition;

            elapsedTime += Time.deltaTime;

            yield return null;
        }
        
        textComponent.text = string.Empty;
        textComponent.gameObject.SetActive(false);
    }
}

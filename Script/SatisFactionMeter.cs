using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatisFactionMeter : MonoBehaviour
{

    public float satisFaction = 100f;
    private float multiplier = 5.65f;
    public RectTransform rectTransform;
    private float currentTopScale;

    void Start()
    {
        currentTopScale = calcTopScale();
    }

    void Update()
    {
        float targetTopScale = calcTopScale();
        currentTopScale = Mathf.Lerp(currentTopScale, targetTopScale, Time.deltaTime * 5f); // Change 5f to adjust the speed
        rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, -currentTopScale);
    }

    public void UpdateSatisFaction(float newSatisfaction)
    {
        satisFaction = newSatisfaction;
    }

    private float calcTopScale()
    {
        return (100f - satisFaction) * multiplier;
    }
}

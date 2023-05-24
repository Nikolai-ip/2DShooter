using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    protected Slider slider;
    protected float maxValue;
    protected float minValue;
    [SerializeField] protected float setValueDelay;
    private void Start()
    {
        var entity = GetComponentInParent<Entity>();
        slider = GetComponent<Slider>();
        maxValue = entity.Health;
        slider.maxValue = maxValue;
        slider.minValue = 0;
        slider.value = maxValue;
        entity.OnHealthchanged += SmoothSetSliderValue;
    }
    protected void SetSliderValue(float value)
    {
        slider.value = value;
    }
    protected void SmoothSetSliderValue(float value)
    {
        StopAllCoroutines();
        StartCoroutine(SmothSetValue(value));
    }
    protected IEnumerator SmothSetValue(float value)
    {
        float time = 0;
        float originSliderValue = slider.value;
        float dValue = value - originSliderValue;
        while (Compare(slider.value, value, Mathf.Sign(dValue)))
        {
            time += Time.deltaTime;
            slider.value += time * (dValue / setValueDelay);
            yield return null;
        }
        slider.value = value;
    }
    protected bool Compare(float sliderValue, float setValue, float sign)
    {
        if (sign == 1)
        {
            return MathF.Round(slider.value, 1) < MathF.Round(setValue, 1);
        }
        return MathF.Round(slider.value, 1) > MathF.Round(setValue, 1);
    }
    private void OnDestroy()
    {
        Destroy(transform.parent.gameObject);

    }
}

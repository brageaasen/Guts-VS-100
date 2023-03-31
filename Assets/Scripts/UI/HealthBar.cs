using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] private CanvasGroup healthBarUIGroup;

    public Slider slider;
    public Gradient gradient;
    public Image fill;

    
    
    void Update() {
        if (slider.value == slider.maxValue || slider.value == slider.minValue) {
            healthBarUIGroup.alpha = 0;
        }
        else { 
            healthBarUIGroup.alpha = 1;
        }
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }
    public void SetHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

}

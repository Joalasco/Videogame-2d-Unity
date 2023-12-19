using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    
    public void setMaxHealth(int health)
    {
        slider.value = health;
        slider.maxValue = health;
    }
    public void SetHeatlh(int health)
    {
        slider.value = health;
    }
}

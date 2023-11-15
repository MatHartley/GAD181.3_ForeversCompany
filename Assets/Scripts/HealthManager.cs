using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthManager : MonoBehaviour
{
    public TextMeshProUGUI valueText;
    [Range(0,100)]
    public float currentHealth = 100;
    private float healthDegenRate = 1.0f;
    private float healthRegenRate = 3.0f;
    public Slider rightSlider;
    public Slider leftSlider;

    public void OnSliderChanged(float value)
    {
        valueText.text = value.ToString();
    }

    public void DecreaseHealth()
    {
        if (currentHealth > 0)
        {
            currentHealth -= (healthDegenRate * Time.deltaTime);
            leftSlider.value = currentHealth;
            rightSlider.value = currentHealth;
            valueText.text = currentHealth.ToString("F0");
        }
    }

    public void IncreaseHealth()
    {
        if (currentHealth < 100)
        {
            currentHealth += (healthRegenRate * Time.deltaTime);
            leftSlider.value = currentHealth;
            rightSlider.value = currentHealth;
            valueText.text = currentHealth.ToString("F0");
        }
    }
}

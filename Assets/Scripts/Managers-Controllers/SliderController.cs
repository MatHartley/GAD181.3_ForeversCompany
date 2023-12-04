using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderController : MonoBehaviour
{
    public TextMeshProUGUI valueText;

    public void OnSliderChanged (float value)
    {
        valueText.text = value.ToString();
    }
}

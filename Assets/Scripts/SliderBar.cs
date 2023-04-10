using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    public Slider slider;

    public void SetStatusValue(int value)
    {
        slider.value = value;
    }
}
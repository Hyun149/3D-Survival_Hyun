using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 스태미나 수치를 시각적으로 표시하는 월드 UI 바
/// </summary>
public class StaminaBarUI : MonoBehaviour
{
    [SerializeField] private Slider staminaSlider;
    [SerializeField] private StaminaSystem staminaSystem;
  
    private void Start()
    {
        staminaSlider.maxValue = staminaSystem.MaxStamina;
        staminaSlider.value = staminaSystem.CurrentStamina;
        staminaSystem.OnStaminaChanged += UpdateBar;
    }

    private void UpdateBar(float current, float max)
    {
        staminaSlider.value = current;
    }
}

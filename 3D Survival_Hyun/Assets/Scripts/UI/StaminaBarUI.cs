using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���¹̳� ��ġ�� �ð������� ǥ���ϴ� ���� UI ��
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

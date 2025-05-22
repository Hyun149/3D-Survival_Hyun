using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���¹̳� ��ġ�� �ð������� ǥ���ϴ� ���� UI ��
/// Slider ������Ʈ�� ���� �ǽð����� ���¹̳� ��ġ�� ������Ʈ��
/// </summary>
public class StaminaBarUI : MonoBehaviour
{
    [SerializeField] private Slider staminaSlider;
    [SerializeField] private StaminaSystem staminaSystem;
  
    /// <summary>
    /// ���� �� �ʱⰪ ���� �� ���¹̳� ���� �̺�Ʈ ����
    /// </summary>
    private void Start()
    {
        staminaSlider.maxValue = staminaSystem.MaxStamina;
        staminaSlider.value = staminaSystem.CurrentStamina;
        staminaSystem.OnStaminaChanged += UpdateBar;
    }

    /// <summary>
    /// ���¹̳��� ����� ������ �����̴� ���� �����Ͽ� UI�� �ݿ�
    /// </summary>
    /// <param name="current">���� ���¹̳�</param>
    /// <param name="max">�ִ� ���¹̳�</param>
    private void UpdateBar(float current, float max)
    {
        staminaSlider.value = current;
    }
}

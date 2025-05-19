using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���¹̳� ��ġ�� �ð������� ǥ���ϴ� ���� UI ��
/// </summary>
public class StaminaBarUI : MonoBehaviour
{
    [SerializeField] private Image staminaFill;
    [SerializeField] private StaminaSystem staminaSystem;

    private void Start()
    {
        staminaSystem.OnStaminaChanged += UpdateBar;
    }

    private void UpdateBar(float current, float max)
    {
        staminaFill.fillAmount = current / max;
    }
}

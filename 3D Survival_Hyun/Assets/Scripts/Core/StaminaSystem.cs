using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어의 스태미나 수치를 관리하는 시스템
/// </summary>
public class StaminaSystem : MonoBehaviour
{
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float regenRate = 10f;
    private float currentStamina;

    public event Action<float, float> OnStaminaChanged;

    private void Awake()
    {
        currentStamina = maxStamina;
    }

    void Update()
    {
        RegenerateStamina();
    }

    private void RegenerateStamina()
    {
        if (currentStamina < maxStamina)
        {
            currentStamina += regenRate * Time.deltaTime;
            currentStamina = Mathf.Min(currentStamina, maxStamina);
            OnStaminaChanged?.Invoke(currentStamina, maxStamina);
        }
    }

    public bool Consume(float amount)
    {
        if (currentStamina >= amount)
        {
            currentStamina -= amount;
            OnStaminaChanged?.Invoke(currentStamina, maxStamina);
            return true;
        }

        return false;
    }
}

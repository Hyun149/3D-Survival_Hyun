using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어의 스태미나 수치를 관리하는 시스템
/// 스태미나 소모 및 초당 자동 회복 처리와 UI 이벤트 연동을 담당
/// </summary>
public class StaminaSystem : MonoBehaviour
{
    [SerializeField] private float maxStamina;
    [SerializeField] private float regenRate;
    private float currentStamina;

    public event Action<float, float> OnStaminaChanged;
    public float MaxStamina => maxStamina;
    public float CurrentStamina => currentStamina;

    /// <summary>
    /// 초기화 시 현재 스태미나를 최대치로 설정
    /// </summary>
    private void Awake()
    {
        currentStamina = maxStamina;
    }

    /// <summary>
    /// 매 프레임마다 스태미나 자동 회복 처리
    /// </summary>
    void Update()
    {
        RegenerateStamina();
    }

    /// <summary>
    /// 스태미나가 최대치 미만일 경우 초당 regenRate 비율로 회복
    /// 회복 시 이벤트를 발생시켜 UI를 갱신할 수 있도록 함
    /// </summary>
    private void RegenerateStamina()
    {
        if (currentStamina < maxStamina)
        {
            currentStamina += regenRate * Time.deltaTime;
            currentStamina = Mathf.Min(currentStamina, maxStamina);
            OnStaminaChanged?.Invoke(currentStamina, maxStamina);
        }
    }

    /// <summary>
    /// 지정된 양의 스태미나를 소모
    /// 충분한 스태미나가 있을 경우 감소시키고 이벤트 발생
    /// </summary>
    /// <param name="amount">소모할 스태미나 양</param>
    /// <returns>소모 성공 여부 (true: 성공, false: 실패)</returns>
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 스태미나 수치를 시각적으로 표시하는 월드 UI 바
/// Slider 컴포넌트를 통해 실시간으로 스태미나 수치를 업데이트함
/// </summary>
public class StaminaBarUI : MonoBehaviour
{
    [SerializeField] private Slider staminaSlider;
    [SerializeField] private StaminaSystem staminaSystem;
  
    /// <summary>
    /// 시작 시 초기값 설정 및 스태미나 변경 이벤트 구독
    /// </summary>
    private void Start()
    {
        staminaSlider.maxValue = staminaSystem.MaxStamina;
        staminaSlider.value = staminaSystem.CurrentStamina;
        staminaSystem.OnStaminaChanged += UpdateBar;
    }

    /// <summary>
    /// 스태미나가 변경될 때마다 슬라이더 값을 갱신하여 UI에 반영
    /// </summary>
    /// <param name="current">현재 스태미나</param>
    /// <param name="max">최대 스태미나</param>
    private void UpdateBar(float current, float max)
    {
        staminaSlider.value = current;
    }
}

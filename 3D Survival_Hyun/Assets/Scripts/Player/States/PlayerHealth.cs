using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 플레이어의 체력 상태를 관리하고 UI와 연동하는 컴포넌트
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private Slider healthBar;

    private float currentHealth;

    /// <summary>
    /// 초기 체력 설정 및 체력바 초기화
    /// </summary>
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }

    /// <summary>
    /// 데미지를 받아 체력을 감소시키고 체력바를 갱신
    /// </summary>
    /// <param name="amount"></param>
    public void TakeDamage(float amount)
    {
        currentHealth = Mathf.Max(currentHealth - amount, 0);
        healthBar.value = currentHealth;
    }

    /// <summary>
    /// 외부에서 체력 회복 처리
    /// </summary>
    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        healthBar.value = currentHealth;
    }
}

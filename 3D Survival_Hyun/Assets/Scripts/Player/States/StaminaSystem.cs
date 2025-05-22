using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾��� ���¹̳� ��ġ�� �����ϴ� �ý���
/// ���¹̳� �Ҹ� �� �ʴ� �ڵ� ȸ�� ó���� UI �̺�Ʈ ������ ���
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
    /// �ʱ�ȭ �� ���� ���¹̳��� �ִ�ġ�� ����
    /// </summary>
    private void Awake()
    {
        currentStamina = maxStamina;
    }

    /// <summary>
    /// �� �����Ӹ��� ���¹̳� �ڵ� ȸ�� ó��
    /// </summary>
    void Update()
    {
        RegenerateStamina();
    }

    /// <summary>
    /// ���¹̳��� �ִ�ġ �̸��� ��� �ʴ� regenRate ������ ȸ��
    /// ȸ�� �� �̺�Ʈ�� �߻����� UI�� ������ �� �ֵ��� ��
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
    /// ������ ���� ���¹̳��� �Ҹ�
    /// ����� ���¹̳��� ���� ��� ���ҽ�Ű�� �̺�Ʈ �߻�
    /// </summary>
    /// <param name="amount">�Ҹ��� ���¹̳� ��</param>
    /// <returns>�Ҹ� ���� ���� (true: ����, false: ����)</returns>
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

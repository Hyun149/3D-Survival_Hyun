using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾��� ��� ��ġ�� �����ϰ�, ��ȭ�� ���� �� �̺�Ʈ�� �߻����� UI ��� �����ϴ� �ý���
/// �̱��� ������ ����Ͽ� ���� ������ �����ϵ��� �����
/// </summary>
public class GoldSystem : MonoBehaviour
{
    public static GoldSystem Instance { get; private set; }

    [SerializeField] private int startingGold = 0;

    private int currentGold;

    public event Action<int> OnGoldChanged;

    public int CurrentGold => currentGold;

    /// <summary>
    /// �̱��� �ν��Ͻ��� �����ϰ� �ʱ� ��尪�� ����
    /// </summary>
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        currentGold = startingGold;
    }

    /// <summary>
    /// ��带 �߰��ϰ�, ���� �̺�Ʈ�� �߻���Ŵ
    /// </summary>
    /// <param name="amount">�߰��� ����� ��</param>
    public void AddGold(int amount)
    {
        currentGold += amount;
        OnGoldChanged?.Invoke(currentGold);
    }

    /// <summary>
    /// ��带 �����ϰ�, ���� ���θ� ��ȯ��
    /// ���� �� ���� �̺�Ʈ�� �߻�
    /// </summary>
    /// <param name="amount">������ ����� ��</param>
    /// <returns>���� ���� ���� (true: ����, false: ����)</returns>
    public bool SpendGold(int amount)
    {
        if (currentGold < amount) return false;
        currentGold -= amount;
        OnGoldChanged?.Invoke(currentGold);
        return true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

/// <summary>
/// ���� ��� ��ġ�� UI�� ǥ���ϴ� ������Ʈ
/// </summary>
public class GoldUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldText;

    /// <summary>
    /// ���� �� GoldSystem�� �̺�Ʈ�� �����ϰ� �ʱ� ��� ǥ��
    /// </summary>
    void Start()
    {
        if (GoldSystem.Instance != null)
        {
            GoldSystem.Instance.OnGoldChanged += UpdateGoldDisplay;
            UpdateGoldDisplay(GoldSystem.Instance.CurrentGold);
        }
    }

    /// <summary>
    /// ��� ��ġ�� ����Ǿ��� �� UI �ؽ�Ʈ�� ����
    /// </summary>
    /// <param name="amount"></param>
    private void UpdateGoldDisplay(int amount)
    {
        goldText.text = $"{amount}G";
    }

    /// <summary>
    /// ������Ʈ �ı� �� �̺�Ʈ ���� ����
    /// </summary>
    private void OnDestroy()
    {
        if (GoldSystem.Instance != null)
        {
            GoldSystem.Instance.OnGoldChanged -= UpdateGoldDisplay;
        }
    }
}

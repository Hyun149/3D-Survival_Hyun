using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

/// <summary>
/// 현재 골드 수치를 UI에 표시하는 컴포넌트
/// </summary>
public class GoldUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldText;

    void Start()
    {
        if (GoldSystem.Instance != null)
        {
            GoldSystem.Instance.OnGoldChanged += UpdateGoldDisplay;
            UpdateGoldDisplay(GoldSystem.Instance.CurrentGold);
        }
    }

    private void UpdateGoldDisplay(int amount)
    {
        goldText.text = $"{amount}G";
    }

    private void OnDestroy()
    {
        if (GoldSystem.Instance != null)
        {
            GoldSystem.Instance.OnGoldChanged -= UpdateGoldDisplay;
        }
    }
}

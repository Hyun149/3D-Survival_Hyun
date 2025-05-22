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

    /// <summary>
    /// 시작 시 GoldSystem의 이벤트를 구독하고 초기 골드 표시
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
    /// 골드 수치가 변경되었을 때 UI 텍스트를 갱신
    /// </summary>
    /// <param name="amount"></param>
    private void UpdateGoldDisplay(int amount)
    {
        goldText.text = $"{amount}G";
    }

    /// <summary>
    /// 오브젝트 파괴 시 이벤트 구독 해제
    /// </summary>
    private void OnDestroy()
    {
        if (GoldSystem.Instance != null)
        {
            GoldSystem.Instance.OnGoldChanged -= UpdateGoldDisplay;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 게임 내 UI 전반을 관리하는 싱글톤 매니저 클래스
/// 조사 UI 및 장비 장착 프롬프트 등의 표시/숨김을 담당함
/// </summary>
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("조사 UI")]
    [SerializeField] private GameObject inspectPanel;
    [SerializeField] private TMP_Text objectNameText;
    [SerializeField] private TMP_Text descriptionText;

    [Header("장비 장착 프롬프트")]
    [SerializeField] private GameObject equipPromptUI;
    [SerializeField] private TMP_Text equipPromptText;

    /// <summary>
    /// 싱글톤 패턴 초기화
    /// </summary>
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    /// <summary>
    /// 조사 대상 오브젝트의 이름과 설명을 UI에 표시하고, 조사 패널을 활성화함
    /// </summary>
    /// <param name="name">오브젝트 이름</param>
    /// <param name="description">오브젝트 설명</param>
    public void ShowInspectInfo(string name, string description)
    {
        objectNameText.text = name;
        descriptionText.text = description;
        inspectPanel.SetActive(true);
    }

    /// <summary>
    /// 조사 UI 패널을 비활성화함
    /// </summary>
    public void HideInspectInfo()
    {
        inspectPanel.SetActive(false);
    }

    /// <summary>
    /// 특정 아이템 이름을 포함한 장비 장착 프롬프트를 표시함
    /// </summary>
    /// <param name="itemName"></param>
    public void ShowEquipPrompt(string itemName)
    {
        equipPromptText.text = $"[E] 장착 <b>{itemName}</b>";
        equipPromptUI.SetActive(true);
    }

    /// <summary>
    /// 장비 장착 프롬프트를 비활성화함
    /// </summary>
    public void HideEquipPrompt()
    {
        equipPromptUI.SetActive(false);
    }
}

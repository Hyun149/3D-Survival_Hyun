using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 게임 내 UI 전체를 관리하는 매니저 클래스
/// 현재는 조사(Inspect) UI에 대한 표시 및 숨김 기능을 담당함
/// </summary>
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("조사 UI")]
    [SerializeField] private GameObject inspectPanel;
    [SerializeField] private TMP_Text objectNameText;
    [SerializeField] private TMP_Text DescriptionText;

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
    /// 조사 대상 오브젝트의 이름과 설명을 UI에 표시하고 패널을 활성화
    /// </summary>
    /// <param name="name"></param>
    /// <param name="description"></param>
    public void ShowInspectInfo(string name, string description)
    {
        objectNameText.text = name;
        DescriptionText.text = description;
        inspectPanel.SetActive(true);
    }

    /// <summary>
    /// 조사 UI 패널을 숨김
    /// </summary>
    public void HideInspectInfo()
    {
        inspectPanel.SetActive(false);
    }
}

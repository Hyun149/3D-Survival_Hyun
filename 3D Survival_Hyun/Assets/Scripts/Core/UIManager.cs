using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���� �� UI ��ü�� �����ϴ� �Ŵ��� Ŭ����
/// ����� ����(Inspect) UI�� ���� ǥ�� �� ���� ����� �����
/// </summary>
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("���� UI")]
    [SerializeField] private GameObject inspectPanel;
    [SerializeField] private TMP_Text objectNameText;
    [SerializeField] private TMP_Text DescriptionText;

    [Header("��� ���� ������Ʈ")]
    [SerializeField] private GameObject equipPromptUI;
    [SerializeField] private TMP_Text equipPromptText;

    /// <summary>
    /// �̱��� ���� �ʱ�ȭ
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
    /// ���� ��� ������Ʈ�� �̸��� ������ UI�� ǥ���ϰ� �г��� Ȱ��ȭ
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
    /// ���� UI �г��� ����
    /// </summary>
    public void HideInspectInfo()
    {
        inspectPanel.SetActive(false);
    }

    public void ShowEquipPrompt(string itemName)
    {
        equipPromptText.text = $"[E] ���� <b>{itemName}</b>";
        equipPromptUI.SetActive(true);
    }

    public void HideEquipPrompt()
    {
        equipPromptUI.SetActive(false);
    }
}

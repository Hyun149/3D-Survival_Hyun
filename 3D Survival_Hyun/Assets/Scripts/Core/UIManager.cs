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
}

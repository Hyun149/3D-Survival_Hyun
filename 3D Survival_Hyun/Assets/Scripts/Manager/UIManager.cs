using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Á¶»ç UI")]
    [SerializeField] private GameObject inspectPanel;
    [SerializeField] private TMP_Text objectNameText;
    [SerializeField] private TMP_Text DescriptionText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void ShowInspectInfo(string name, string description)
    {
        objectNameText.text = name;
        DescriptionText.text = description;
        inspectPanel.SetActive(true);
    }

    public void HideInspectInfo()
    {
        inspectPanel.SetActive(false);
    }
}

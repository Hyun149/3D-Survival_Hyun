using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ������ ���¸� �����ϴ� �̱��� GameManager Ŭ����
/// ��� �ý��� �� ��ü �ý����� ���� ���� ����Ʈ ������ ����
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GoldSystem GoldSystem { get; private set; }

    /// <summary>
    /// �ν��Ͻ��� �ʱ�ȭ�ϰ�, ��� �ý����� ������
    /// �̹� �����ϴ� GameManager�� ���� ��� �ߺ� ����
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            GoldSystem = GetComponent<GoldSystem>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

using System;
using UnityEngine;

/// <summary>
/// ���� �÷��̾� ������Ʈ�� �����Ǿ�, ��Ʈ�ѷ� �� �۷ι� �Ŵ����� ����ϴ� ������ ����ϴ� Ŭ����
/// </summary>
public class Player : MonoBehaviour
{
    public PlayerController controller;

    /// <summary>
    /// ���� ���� �� CharacterManager�� �ڽ��� ����ϰ� PlayerController�� �Ҵ�
    /// </summary>
    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
    }
}

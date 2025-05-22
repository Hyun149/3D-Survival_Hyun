using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// �÷��̾� �Է� ó��, �̵�/ȸ�� ����, ���� ������ ���� ���� �����ϴ� ��Ʈ�ѷ� Ŭ����
/// �ֿ� ����� �� ���� ������Ʈ(PlayerLook, PlayerMovement ��)�� ������
/// </summary>
public class PlayerController : MonoBehaviour
{
    private PlayerLook look;
    private PlayerMovement movement;
    private PlayerFallDamage fallDamage;
    private PlayerInputHandler input;

    /// <summary>
    /// ���� ������Ʈ ���� �ʱ�ȭ 
    /// </summary>
    private void Awake()
    {
        look = GetComponent<PlayerLook>();
        movement = GetComponent<PlayerMovement>();
        fallDamage = GetComponent<PlayerFallDamage>();
        input = GetComponent<PlayerInputHandler>();
    }

    /// <summary>
    /// ���� ���� �� ���콺 Ŀ���� ��� ���·� ����
    /// </summary>
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// ���� ���� ó��: ���� ������ üũ
    /// </summary>
    private void FixedUpdate()
    {
        fallDamage.CheckFallDamage();
    }

    /// <summary>
    /// ī�޶� ȸ���� LateUpdate���� ó�� (���� ��鸲 ������)
    /// </summary>
    private void LateUpdate()
    {
        look.HandleLook();
    }

    /// <summary>
    /// ���콺 Ŀ�� ��� ���¸� ����ϰ�, ���� ��� ����ȭ
    /// </summary>
    /// <param name="toggle">true�� Ŀ�� ����, false�� ���</param>
    public void ToggleCursor(bool toggle)
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        look.ToggleLook(!toggle);
    }
}
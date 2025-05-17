using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// ���콺 �Է��� �޾� �÷��̾� �� ī�޶��� ȸ���� �����ϴ� Ŭ����
/// X�� ȸ���� ī�޶�(Pitch), Y�� ȸ���� �÷��̾�(Yaw)�� ó��
/// </summary>
public class PlayerLook : MonoBehaviour
{
    [Header("Look")]
    [SerializeField] private Transform cameraContainer;
    [SerializeField] private float minXLook = -60f;
    [SerializeField] private float maxXLook = 60f;
    [SerializeField] private float lookSensiotivity = 3f;

    private Vector2 mouseDelta;
    private float camCurXRot;
    private bool canLook = true;
    private PlayerInputHandler input;

    /// <summary>
    /// �Է� ������Ʈ �ʱ�ȭ
    /// </summary>
    private void Awake()
    {
        input = GetComponent<PlayerInputHandler>();
    }

    /// <summary>
    /// ���콺 �̵����� ���� ī�޶�� �÷��̾� ȸ���� ó��
    /// ī�޶�� X��(Pitch), �÷��̾�� Y��(Yaw) ȸ��
    /// </summary>
    public void HandleLook()
    {
        Vector2 delta = input.LookInput;

        if (!canLook)
        {
            return;
        }

        camCurXRot += delta.y * lookSensiotivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, delta.x * lookSensiotivity, 0);
    }

    /// <summary>
    /// �þ� ȸ�� ���� ���θ� ����
    /// </summary>
    /// <param name="enabled"></param>
    public void ToggleLook(bool enabled)
    {
        canLook = enabled;
    }
}

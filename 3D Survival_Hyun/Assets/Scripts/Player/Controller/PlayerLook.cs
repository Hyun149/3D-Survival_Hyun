using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 마우스 입력을 받아 플레이어 및 카메라의 회전을 제어하는 클래스
/// X축 회전은 카메라(Pitch), Y축 회전은 플레이어(Yaw)로 처리
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
    /// 입력 컴포넌트 초기화
    /// </summary>
    private void Awake()
    {
        input = GetComponent<PlayerInputHandler>();
    }

    /// <summary>
    /// 마우스 이동값에 따라 카메라와 플레이어 회전을 처리
    /// 카메라는 X축(Pitch), 플레이어는 Y축(Yaw) 회전
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
    /// 시야 회전 가능 여부를 설정
    /// </summary>
    /// <param name="enabled"></param>
    public void ToggleLook(bool enabled)
    {
        canLook = enabled;
    }
}

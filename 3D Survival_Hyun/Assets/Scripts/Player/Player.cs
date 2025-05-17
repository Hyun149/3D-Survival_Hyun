using System;
using UnityEngine;

/// <summary>
/// 실제 플레이어 오브젝트에 부착되어, 컨트롤러 및 글로벌 매니저에 등록하는 역할을 담당하는 클래스
/// </summary>
public class Player : MonoBehaviour
{
    [SerializeField] private PlayerController controller;

    /// <summary>
    /// 게임 시작 시 CharacterManager에 자신을 등록하고 PlayerController를 할당
    /// </summary>
    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
    }
}

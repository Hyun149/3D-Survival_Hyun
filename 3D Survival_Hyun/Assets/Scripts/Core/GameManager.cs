using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 게임 전반의 상태를 관리하는 싱글톤 GameManager 클래스
/// 골드 시스템 등 전체 시스템의 전역 접근 포인트 역할을 수행
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GoldSystem GoldSystem { get; private set; }

    /// <summary>
    /// 인스턴스를 초기화하고, 골드 시스템을 설정함
    /// 이미 존재하는 GameManager가 있을 경우 중복 제거
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

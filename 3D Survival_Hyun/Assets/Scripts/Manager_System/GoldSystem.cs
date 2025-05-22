using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어의 골드 수치를 관리하고, 변화가 생길 때 이벤트를 발생시켜 UI 등과 연동하는 시스템
/// 싱글톤 패턴을 사용하여 전역 접근이 가능하도록 설계됨
/// </summary>
public class GoldSystem : MonoBehaviour
{
    public static GoldSystem Instance { get; private set; }

    [SerializeField] private int startingGold = 0;

    private int currentGold;

    public event Action<int> OnGoldChanged;

    public int CurrentGold => currentGold;

    /// <summary>
    /// 싱글톤 인스턴스를 설정하고 초기 골드값을 지정
    /// </summary>
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        currentGold = startingGold;
    }

    /// <summary>
    /// 골드를 추가하고, 변경 이벤트를 발생시킴
    /// </summary>
    /// <param name="amount">추가할 골드의 양</param>
    public void AddGold(int amount)
    {
        currentGold += amount;
        OnGoldChanged?.Invoke(currentGold);
    }

    /// <summary>
    /// 골드를 지출하고, 성공 여부를 반환함
    /// 성공 시 변경 이벤트도 발생
    /// </summary>
    /// <param name="amount">지출할 골드의 양</param>
    /// <returns>지출 성공 여부 (true: 성공, false: 실패)</returns>
    public bool SpendGold(int amount)
    {
        if (currentGold < amount) return false;
        currentGold -= amount;
        OnGoldChanged?.Invoke(currentGold);
        return true;
    }
}

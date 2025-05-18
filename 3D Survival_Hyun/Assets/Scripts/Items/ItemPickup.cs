using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 풀에서 생성된 아이템 오브젝트 (플레이어와 충돌 시 아이템 효과 적용 및 풀 반환)
/// </summary>
public class ItemPickup : MonoBehaviour, IPoolable
{
    private ObjectPool originPool;

    [SerializeField] private ItemData itemData;

    /// <summary>
    /// 풀 매니저로부터 주입되는 풀 객체 저장
    /// </summary>
    public void SetPool(ObjectPool pool)
    {
        originPool = pool;
    }

    /// <summary>
    /// 풀로 오브젝트 반환 처리
    /// </summary>
    public void ReturnToPool()
    {
        originPool?.Return(gameObject);
    }

    /// <summary>
    /// 플레이어와 충돌 시 아이템 효과 적용 및 풀 반환
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // 1. 플레이어의 ItemUseHandler 컴포넌트 찾기
        var handler = other.GetComponent<ItemUseHandler>();
        if (handler != null && itemData != null)
        {
            handler.UseItem(itemData);
        }

        // 2. 풀로 반환
        originPool?.Return(gameObject);
    }
}
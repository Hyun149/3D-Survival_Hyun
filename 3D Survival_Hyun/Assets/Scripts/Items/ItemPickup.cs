using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 풀에서 생성된 아이템 오브젝트
/// </summary>
public class ItemPickup : MonoBehaviour, IPoolable
{
    private ObjectPool originPool;

    [SerializeField] private ItemData itemData;

    public void SetPool(ObjectPool pool)
    {
        originPool = pool;
    }

    public void ReturnToPool()
    {
        originPool?.Return(gameObject);
    }

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
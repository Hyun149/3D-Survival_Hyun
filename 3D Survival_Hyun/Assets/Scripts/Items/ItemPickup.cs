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
    /// 플레이어와 충돌 시 아이템 효과 적용 및 5초 후 재활성화
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        var handler = other.GetComponent<ItemUseHandler>();
        if (handler != null && itemData != null)
        {
            handler.UseItem(itemData);
        }

        ItemRespawner.Instance.RespawnAfterDelay(this.gameObject, 5f);
    }
}
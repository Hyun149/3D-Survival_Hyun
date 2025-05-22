using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 풀에서 생성된 아이템 오브젝트
/// 플레이어와 충돌 시 아이템 효과를 적용하고 일정 시간 후 풀로 반환됨
/// </summary>
public class ItemPickup : MonoBehaviour, IPoolable
{
    private ObjectPool originPool;

    [SerializeField] private ItemData itemData;

    /// <summary>
    /// 풀 매니저로부터 주입되는 원본 풀 정보를 저장 (재활용 시 필요)
    /// </summary>
    public void SetPool(ObjectPool pool)
    {
        originPool = pool;
    }

    /// <summary>
    /// 플레이어가 이 오브젝트와 충돌하면 아이템 효과를 적용하고, 일정 시간 후 풀로 반환 요청을 수행
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
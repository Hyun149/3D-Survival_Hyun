using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ǯ���� ������ ������ ������Ʈ
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

        // 1. �÷��̾��� ItemUseHandler ������Ʈ ã��
        var handler = other.GetComponent<ItemUseHandler>();
        if (handler != null && itemData != null)
        {
            handler.UseItem(itemData);
        }

        // 2. Ǯ�� ��ȯ
        originPool?.Return(gameObject);
    }
}
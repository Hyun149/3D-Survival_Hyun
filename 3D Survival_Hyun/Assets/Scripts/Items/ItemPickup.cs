using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ǯ���� ������ ������ ������Ʈ (�÷��̾�� �浹 �� ������ ȿ�� ���� �� Ǯ ��ȯ)
/// </summary>
public class ItemPickup : MonoBehaviour, IPoolable
{
    private ObjectPool originPool;

    [SerializeField] private ItemData itemData;

    /// <summary>
    /// Ǯ �Ŵ����κ��� ���ԵǴ� Ǯ ��ü ����
    /// </summary>
    public void SetPool(ObjectPool pool)
    {
        originPool = pool;
    }

    /// <summary>
    /// Ǯ�� ������Ʈ ��ȯ ó��
    /// </summary>
    public void ReturnToPool()
    {
        originPool?.Return(gameObject);
    }

    /// <summary>
    /// �÷��̾�� �浹 �� ������ ȿ�� ���� �� Ǯ ��ȯ
    /// </summary>
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
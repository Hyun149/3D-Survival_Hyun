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
    /// �÷��̾�� �浹 �� ������ ȿ�� ���� �� 5�� �� ��Ȱ��ȭ
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
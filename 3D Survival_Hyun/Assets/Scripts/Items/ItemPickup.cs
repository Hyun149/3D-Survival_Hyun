using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ǯ���� ������ ������ ������Ʈ
/// �÷��̾�� �浹 �� ������ ȿ���� �����ϰ� ���� �ð� �� Ǯ�� ��ȯ��
/// </summary>
public class ItemPickup : MonoBehaviour, IPoolable
{
    private ObjectPool originPool;

    [SerializeField] private ItemData itemData;

    /// <summary>
    /// Ǯ �Ŵ����κ��� ���ԵǴ� ���� Ǯ ������ ���� (��Ȱ�� �� �ʿ�)
    /// </summary>
    public void SetPool(ObjectPool pool)
    {
        originPool = pool;
    }

    /// <summary>
    /// �÷��̾ �� ������Ʈ�� �浹�ϸ� ������ ȿ���� �����ϰ�, ���� �ð� �� Ǯ�� ��ȯ ��û�� ����
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
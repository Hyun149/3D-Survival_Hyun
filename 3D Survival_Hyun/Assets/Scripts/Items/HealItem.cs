using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : MonoBehaviour, IPoolable
{
    private ObjectPool originPool;

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

        if (other.CompareTag("Player"))
        {
            // TODO: ü�� ȸ�� ó��
            Debug.Log("[HealItem] Player�� �浹 - ȸ�� �� ��ȯ ó��");
            ReturnToPool();
        }
    }
}

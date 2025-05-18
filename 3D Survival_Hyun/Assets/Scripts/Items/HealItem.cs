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
            // TODO: 체력 회복 처리
            Debug.Log("[HealItem] Player와 충돌 - 회복 및 반환 처리");
            ReturnToPool();
        }
    }
}

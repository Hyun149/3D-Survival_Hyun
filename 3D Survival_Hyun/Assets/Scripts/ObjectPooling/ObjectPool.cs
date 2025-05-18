using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 특정 오브젝트 프리팹에 대한 풀을 관리하는 클래스
/// </summary>
public class ObjectPool
{
    private readonly GameObject prefab;
    private readonly Queue<GameObject> poolQueue = new();
    private readonly Transform parent;

    public PoolType PoolType { get; private set; }

    public ObjectPool(PoolType type, GameObject prefab, int initialSize, Transform parent = null)
    {
        this.PoolType = type;
        this.prefab = prefab;
        this.parent = parent;

        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = CreateNew();
            obj.SetActive(false);
            poolQueue.Enqueue(obj);
        }
    }

    public GameObject Get(Vector3 position)
    {
        GameObject obj = poolQueue.Count > 0 ? poolQueue.Dequeue() : CreateNew();
        obj.transform.position = position;
        obj.SetActive(true);

        var poolable = obj.GetComponent<IPoolable>();
        poolable?.SetPool(this);

        return obj;
    }

    public void Return(GameObject obj)
    {
        obj.SetActive(false);
        poolQueue.Enqueue(obj);
    }

    private GameObject CreateNew()
    {
        var obj = GameObject.Instantiate(prefab, parent);
        return obj;
    }
}

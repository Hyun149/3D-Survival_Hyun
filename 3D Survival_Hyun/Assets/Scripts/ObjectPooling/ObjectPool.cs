using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ư�� ������Ʈ �����տ� ���� Ǯ�� �����ϴ� Ŭ����
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

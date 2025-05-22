using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ư�� ������Ʈ �����տ� ���� Ǯ�� �����ϴ� Ŭ����
/// ������Ʈ�� ���� ���� �� �����Ͽ� ������ ����ȭ��
/// </summary>
public class ObjectPool
{
    private readonly GameObject prefab;
    private readonly Queue<GameObject> poolQueue = new();
    private readonly Transform parent;

    public PoolType PoolType { get; private set; }

    /// <summary>
    /// ������Ʈ Ǯ�� �ʱ�ȭ�ϰ� ������ ����ŭ ������Ʈ�� �̸� ����
    /// </summary>
    /// <param name="type">Ǯ Ÿ�� �ĺ���</param>
    /// <param name="prefab">Ǯ���� ������ ������</param>
    /// <param name="initialSize">�ʱ� ������ ������Ʈ ��</param>
    /// <param name="parent">������ ������Ʈ���� �θ� Transform</param>
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
    /// <summary>
    /// Ǯ���� ������Ʈ�� ���� ��ġ�� �����ϰ� ��ȯ
    /// IPoolable �������̽��� �ִٸ� Ǯ ������ ����
    /// </summary>
    /// <param name="position">������Ʈ�� ��ġ�� ���� ��ġ</param>
    /// <returns>Ȱ��ȭ�� GameObject �ν��Ͻ�</returns>
    public GameObject Get(Vector3 position)
    {
        GameObject obj = poolQueue.Count > 0 ? poolQueue.Dequeue() : CreateNew();
        obj.transform.position = position;
        obj.SetActive(true);

        var poolable = obj.GetComponent<IPoolable>();
        poolable?.SetPool(this);

        return obj;
    }

    /// <summary>
    /// ����� ���� ������Ʈ�� Ǯ�� ��ȯ�Ͽ� ��Ȱ��ȭ �� ��� ���·� ����
    /// </summary>
    /// <param name="obj">��ȯ�� ������Ʈ</param>
    public void Return(GameObject obj)
    {
        obj.SetActive(false);
        poolQueue.Enqueue(obj);
    }

    /// <summary>
    /// ���ο� ������Ʈ�� �����ϰ� �θ� ������ �� ��ȯ
    /// </summary>
    /// <returns>��Ȱ��ȭ�� �ű� GameObject �ν��Ͻ�</returns>
    private GameObject CreateNew()
    {
        var obj = GameObject.Instantiate(prefab, parent);
        return obj;
    }
}

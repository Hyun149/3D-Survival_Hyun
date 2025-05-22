using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� �� ������Ʈ Ǯ�� �߾ӿ��� �����ϴ� �Ŵ��� Ŭ����
/// �� PoolType�� ���� Ǯ�� �����ϰ�, ������Ʈ ������ ���� ���� ����ȭ�� ����
/// </summary>
public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    /// <summary>
    /// �� Ǯ�� ���� ���� (Ǯ Ÿ��, ������, �ʱ� ũ��)
    /// </summary>
    [System.Serializable]
    public class Pool
    {
        public PoolType type;
        public GameObject prefab;
        public int size;
    }

    [SerializeField] private List<Pool> pools;
    private Dictionary<PoolType, ObjectPool> poolDictionary = new();

    /// <summary>
    /// �̱��� �ν��Ͻ��� �����ϰ� Ǯ �ʱ�ȭ�� ����
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        InitializePools();
    }

    /// <summary>
    /// ������ Ǯ ����Ʈ(pools)�� ������� ������ ObjectPool �ν��Ͻ��� �����Ͽ� ���
    /// </summary>
    private void InitializePools()
    {
        foreach (var pool in pools)
        {
            ObjectPool objectPool = new(pool.type, pool.prefab, pool.size, this.transform);
            poolDictionary[pool.type] = objectPool;
        }
    }

    /// <summary>
    /// Ư�� Ǯ Ÿ�Կ��� ������Ʈ�� ������ ���� ��ġ�� Ȱ��ȭ
    /// </summary>
    /// <param name="type">��û�� Ǯ Ÿ��</param>
    /// <param name="position">������Ʈ�� ��ġ�� ��ġ</param>
    /// <returns>Ǯ���� ���� GameObject �ν��Ͻ�</returns>
    public GameObject GetFromPool(PoolType type, Vector3 position)
    {
        if (!poolDictionary.TryGetValue(type, out var pool))
        {
            Debug.LogError($"[PoolManager] {type} Ǯ�� ã�� �� �����ϴ�.");
            return null;
        }

        return pool.Get(position);
    }

    /// <summary>
    /// ����� ���� ������Ʈ�� �ش� Ǯ�� ��ȯ
    /// </summary>
    /// <param name="type">Ǯ Ÿ��</param>
    /// <param name="obj">��ȯ�� ������Ʈ</param>
    public void ReturnToPool(PoolType type, GameObject obj)
    {
        if (!poolDictionary.TryGetValue(type, out var pool))
        {
            Debug.LogError($"[PoolManager] {type} Ǯ�� ã�� �� �����ϴ�.");
            return;
        }

        pool.Return(obj);
    }

    /// <summary>
    /// Ư�� Ÿ���� ObjectPool �ν��Ͻ��� ���� �����ϰ� ���� �� ��ȯ
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public ObjectPool GetPool(PoolType type) => poolDictionary.GetValueOrDefault(type);
}

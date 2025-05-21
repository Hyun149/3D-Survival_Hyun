using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    [System.Serializable]
    public class Pool
    {
        public PoolType type;
        public GameObject prefab;
        public int size;
    }

    [SerializeField] private List<Pool> pools;
    private Dictionary<PoolType, ObjectPool> poolDictionary = new();

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

    private void InitializePools()
    {
        foreach (var pool in pools)
        {
            ObjectPool objectPool = new(pool.type, pool.prefab, pool.size, this.transform);
            poolDictionary[pool.type] = objectPool;
        }
    }

    public GameObject GetFromPool(PoolType type, Vector3 position)
    {
        if (!poolDictionary.TryGetValue(type, out var pool))
        {
            Debug.LogError($"[PoolManager] {type} 풀을 찾을 수 없습니다.");
            return null;
        }

        return pool.Get(position);
    }

    public void ReturnToPool(PoolType type, GameObject obj)
    {
        if (!poolDictionary.TryGetValue(type, out var pool))
        {
            Debug.LogError($"[PoolManager] {type} 풀을 찾을 수 없습니다.");
            return;
        }

        pool.Return(obj);
    }

    public ObjectPool GetPool(PoolType type) => poolDictionary.GetValueOrDefault(type);
}

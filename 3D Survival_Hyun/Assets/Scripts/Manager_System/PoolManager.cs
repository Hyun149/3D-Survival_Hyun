using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 게임 내 오브젝트 풀을 중앙에서 관리하는 매니저 클래스
/// 각 PoolType에 따른 풀을 생성하고, 오브젝트 재사용을 통해 성능 최적화를 지원
/// </summary>
public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    /// <summary>
    /// 각 풀의 설정 정보 (풀 타입, 프리팹, 초기 크기)
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
    /// 싱글톤 인스턴스를 설정하고 풀 초기화를 수행
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
    /// 설정된 풀 리스트(pools)를 기반으로 각각의 ObjectPool 인스턴스를 생성하여 등록
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
    /// 특정 풀 타입에서 오브젝트를 가져와 지정 위치에 활성화
    /// </summary>
    /// <param name="type">요청할 풀 타입</param>
    /// <param name="position">오브젝트가 배치될 위치</param>
    /// <returns>풀에서 꺼낸 GameObject 인스턴스</returns>
    public GameObject GetFromPool(PoolType type, Vector3 position)
    {
        if (!poolDictionary.TryGetValue(type, out var pool))
        {
            Debug.LogError($"[PoolManager] {type} 풀을 찾을 수 없습니다.");
            return null;
        }

        return pool.Get(position);
    }

    /// <summary>
    /// 사용이 끝난 오브젝트를 해당 풀로 반환
    /// </summary>
    /// <param name="type">풀 타입</param>
    /// <param name="obj">반환할 오브젝트</param>
    public void ReturnToPool(PoolType type, GameObject obj)
    {
        if (!poolDictionary.TryGetValue(type, out var pool))
        {
            Debug.LogError($"[PoolManager] {type} 풀을 찾을 수 없습니다.");
            return;
        }

        pool.Return(obj);
    }

    /// <summary>
    /// 특정 타입의 ObjectPool 인스턴스를 직접 참조하고 싶을 때 반환
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public ObjectPool GetPool(PoolType type) => poolDictionary.GetValueOrDefault(type);
}

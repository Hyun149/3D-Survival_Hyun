using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 특정 오브젝트 프리팹에 대한 풀을 관리하는 클래스
/// 오브젝트를 사전 생성 및 재사용하여 성능을 최적화함
/// </summary>
public class ObjectPool
{
    private readonly GameObject prefab;
    private readonly Queue<GameObject> poolQueue = new();
    private readonly Transform parent;

    public PoolType PoolType { get; private set; }

    /// <summary>
    /// 오브젝트 풀을 초기화하고 지정된 수만큼 오브젝트를 미리 생성
    /// </summary>
    /// <param name="type">풀 타입 식별자</param>
    /// <param name="prefab">풀에서 관리할 프리팹</param>
    /// <param name="initialSize">초기 생성할 오브젝트 수</param>
    /// <param name="parent">생성된 오브젝트들의 부모 Transform</param>
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
    /// 풀에서 오브젝트를 꺼내 위치를 지정하고 반환
    /// IPoolable 인터페이스가 있다면 풀 참조를 주입
    /// </summary>
    /// <param name="position">오브젝트를 배치할 월드 위치</param>
    /// <returns>활성화된 GameObject 인스턴스</returns>
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
    /// 사용이 끝난 오브젝트를 풀로 반환하여 비활성화 후 대기 상태로 돌림
    /// </summary>
    /// <param name="obj">반환할 오브젝트</param>
    public void Return(GameObject obj)
    {
        obj.SetActive(false);
        poolQueue.Enqueue(obj);
    }

    /// <summary>
    /// 새로운 오브젝트를 생성하고 부모를 설정한 후 반환
    /// </summary>
    /// <returns>비활성화된 신규 GameObject 인스턴스</returns>
    private GameObject CreateNew()
    {
        var obj = GameObject.Instantiate(prefab, parent);
        return obj;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 비활성화된 아이템을 일정 시간 후 다시 활성화시키는 전담 매니저
/// </summary>
public class ItemRespawner : MonoBehaviour
{
    public static ItemRespawner Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 지정된 게임 오브젝트를 비활성화한 뒤 일정 시간 후 다시 활성화함
    /// </summary>
    /// <param name="obj">재활성화할 대상</param>
    /// <param name="delay">비활성화 유지 시간 (초)</param>
    public void RespawnAfterDelay(GameObject obj, float delay)
    {
        StartCoroutine(RespawnRoutine(obj, delay));
    }

    /// <summary>
    /// 딜레이 후 오브젝트를 재활성화하는 코루틴
    /// </summary>
    /// <param name="obj">재활성화할 대상</param>
    /// <param name="delay">딜레이 시간</param>
    /// <returns></returns>
    private IEnumerator RespawnRoutine(GameObject obj, float delay)
    {
        obj.SetActive(false);
        yield return new WaitForSeconds(delay);
        obj.SetActive(true);
    }
}

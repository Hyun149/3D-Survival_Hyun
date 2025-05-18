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

    public void RespawnAfterDelay(GameObject obj, float delay)
    {
        StartCoroutine(RespawnRoutine(obj, delay));
    }

    private IEnumerator RespawnRoutine(GameObject obj, float delay)
    {
        obj.SetActive(false);
        yield return new WaitForSeconds(delay);
        obj.SetActive(true);
    }
}

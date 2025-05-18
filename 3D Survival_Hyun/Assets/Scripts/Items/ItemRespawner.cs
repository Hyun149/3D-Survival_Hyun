using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Ȱ��ȭ�� �������� ���� �ð� �� �ٽ� Ȱ��ȭ��Ű�� ���� �Ŵ���
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

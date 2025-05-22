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

    /// <summary>
    /// ������ ���� ������Ʈ�� ��Ȱ��ȭ�� �� ���� �ð� �� �ٽ� Ȱ��ȭ��
    /// </summary>
    /// <param name="obj">��Ȱ��ȭ�� ���</param>
    /// <param name="delay">��Ȱ��ȭ ���� �ð� (��)</param>
    public void RespawnAfterDelay(GameObject obj, float delay)
    {
        StartCoroutine(RespawnRoutine(obj, delay));
    }

    /// <summary>
    /// ������ �� ������Ʈ�� ��Ȱ��ȭ�ϴ� �ڷ�ƾ
    /// </summary>
    /// <param name="obj">��Ȱ��ȭ�� ���</param>
    /// <param name="delay">������ �ð�</param>
    /// <returns></returns>
    private IEnumerator RespawnRoutine(GameObject obj, float delay)
    {
        obj.SetActive(false);
        yield return new WaitForSeconds(delay);
        obj.SetActive(true);
    }
}

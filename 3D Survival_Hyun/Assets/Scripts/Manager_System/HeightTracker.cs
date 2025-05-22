using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾ ������ �ְ� ���̸� �����ϰ�,
/// ���̰� ���ŵ� ������ ���� ��带 �����ϴ� �ý���
/// </summary>
public class HeightTracker : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float goldPerMeter;

    private float highestY = 0f;

    /// <summary>
    /// �� �����Ӹ��� ���� ���̸� Ȯ���Ͽ�
    /// ���� �ְ� ���̺��� ��� �� ��带 ����
    /// </summary>
    void Update()
    {
        float currentY = player.position.y;
        if (currentY > highestY)
        {
            float gainedHeight = currentY - highestY;
            int goldToAdd = Mathf.FloorToInt(gainedHeight * goldPerMeter);

            if (goldToAdd > 0)
            {
                GameManager.Instance.GoldSystem.AddGold(goldToAdd);
                highestY = currentY;
            }
        }
    }
}

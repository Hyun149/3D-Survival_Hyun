using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾ ������ �ְ� ���̸� �����ϰ�,
/// ����� ������ ��带 �����ϴ� �ý���
/// </summary>
public class HeightTracker : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float goldPerMetar = 1f;

    private float highestY = 0f;

    void Update()
    {
        float currentY = player.position.y;
        if (currentY > highestY)
        {
            float gainedHeight = currentY - highestY;
            int goldToAdd = Mathf.FloorToInt(gainedHeight * goldPerMetar);

            if (goldToAdd > 0)
            {
                GameManager.Instance.GoldSystem.AddGold(goldToAdd);
                highestY = currentY;
            }
        }
    }
}

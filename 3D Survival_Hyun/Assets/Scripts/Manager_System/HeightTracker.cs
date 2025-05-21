using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어가 도달한 최고 높이를 추적하고,
/// 상승할 때마다 골드를 지급하는 시스템
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

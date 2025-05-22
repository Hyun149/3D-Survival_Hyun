using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어가 도달한 최고 높이를 추적하고,
/// 높이가 갱신될 때마다 보상 골드를 지급하는 시스템
/// </summary>
public class HeightTracker : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float goldPerMeter;

    private float highestY = 0f;

    /// <summary>
    /// 매 프레임마다 현재 높이를 확인하여
    /// 이전 최고 높이보다 상승 시 골드를 지급
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

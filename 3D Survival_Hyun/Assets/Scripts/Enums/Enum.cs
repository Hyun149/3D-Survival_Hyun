/// <summary>
/// 게임 내 아이템의 종류를 정의하는 열거형(enum)
/// 아이템 동작 방식이나 분류에 따라 달라짐
/// </summary>
public enum ItemType
{
    Consumable,
    Equipment,
    Quest
}

/// <summary>
/// 풀 종류를 분류하기 위한 열거형 (등록 및 구분용)
/// </summary>
public enum PoolType
{
    JumpPumkin,
    HealFish
}

/// <summary>
/// 아이템 효과 타입 정의
/// </summary>
public enum ItemEffectType
{
    None,
    JumpBoost,
    Heal
}

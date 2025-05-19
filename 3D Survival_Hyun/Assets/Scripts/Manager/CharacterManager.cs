/*using UnityEngine;

/// <summary>
/// 플레이어 캐릭터를 전역에서 관리하는 싱글톤 클래스
/// </summary>
public class CharacterManager : MonoBehaviour
{
    private static CharacterManager _instance;

    /// <summary>
    /// CharacterManager의 싱글톤 인스턴스 접근자
    /// </summary>
    public static CharacterManager Instance
    {
        get
        {
            if (_instance == null)
            {                _instance = new GameObject("CharacerManager").AddComponent<CharacterManager>();
            }
            return _instance;
        }
    }

    /// <summary>
    /// 현재 활성화된 플레이어 참조
    /// </summary>
    public Player Player
    {
        get { return _player; }
        set { _player = value; }
    }
    private Player _player;

    /// <summary>
    /// 싱글톤 인스턴스를 설정하고 중복된 매니저는 제거
    /// </summary>
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}*/

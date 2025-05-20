/*using UnityEngine;

/// <summary>
/// �÷��̾� ĳ���͸� �������� �����ϴ� �̱��� Ŭ����
/// </summary>
public class CharacterManager : MonoBehaviour
{
    private static CharacterManager _instance;

    /// <summary>
    /// CharacterManager�� �̱��� �ν��Ͻ� ������
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
    /// ���� Ȱ��ȭ�� �÷��̾� ����
    /// </summary>
    public Player Player
    {
        get { return _player; }
        set { _player = value; }
    }
    private Player _player;

    /// <summary>
    /// �̱��� �ν��Ͻ��� �����ϰ� �ߺ��� �Ŵ����� ����
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

/// <summary>
/// 풀로부터 주입받는 객체의 공통 인터페이스
/// </summary>
public interface IPoolable
{
    void SetPool(ObjectPool pool);
}
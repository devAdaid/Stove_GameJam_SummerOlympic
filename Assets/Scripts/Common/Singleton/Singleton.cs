/// <summary>
/// 일반 싱글턴입니다.
/// </summary>
/// <typeparam name="T">상속할 클래스의 타입을 넣습니다.</typeparam>
public class Singleton<T> where T : class, new()
{
    public static bool IsInitialized => _instance != null;

    public static T I
    {
        get
        {
            if (_instance == null)
            {
                _instance = new T();
            }
            return _instance;
        }
    }

    protected static T _instance = null;

    public virtual void EchoForCreate()
    {
        if (_instance == null)
        {
            _instance = new T();
        }
    }
}
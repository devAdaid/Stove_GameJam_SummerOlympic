using UnityEngine;

/// <summary>
/// MonoBehaviour을 상속받는 싱글턴입니다.
/// </summary>
/// <typeparam name="T">상속할 클래스의 타입을 넣습니다.</typeparam>
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static bool IsInitialized => _instance != null;

    public static T I
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    T prefab = Resources.Load<T>("Prefabs/Singleton/" + typeof(T).ToString());
                    if (prefab != null)
                    {
                        _instance = Instantiate(prefab) as T;
                        _instance.name = typeof(T).ToString();
                    }
                    else
                    {
                        _instance = new GameObject(typeof(T).ToString(), typeof(T)).GetComponent<T>();
                    }
                }
            }

            return _instance;
        }
    }

    protected static T _instance = null;

    public void EchoForCreate() { }

    protected virtual void OnApplicationQuit()
    {
        _instance = null;
    }
}
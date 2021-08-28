using UnityEngine;

/// <summary>
/// MonoBehaviour을 상속받으며 씬을 이동하여도 파괴되지 않는 싱글턴입니다.
/// </summary>
/// <typeparam name="T">상속할 클래스의 타입을 넣습니다.</typeparam>
public abstract class PersistentSingleton<T> : MonoSingleton<T> where T : MonoBehaviour
{
    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
        }
        else if (_instance != this)
        {
            DestroyImmediate(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
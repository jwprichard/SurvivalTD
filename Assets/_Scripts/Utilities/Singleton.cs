using UnityEngine;

/// <summary>
/// A static instance is similar to a singleton, but instead of destoying any new instances on load,
/// it overrides the current instance. This is handy for resetting the state and saves you doing it manually.
/// </summary>
public abstract class StaticInstance<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }
    protected virtual void Awake() => Instance = this as T;

    protected virtual void OnApplicationQuit()
    {
        Instance = null; ;
        Destroy(gameObject);
    }
}

/// <summary>
/// This transforms the static instance into a basic singleton. This will destroy any new version created,
/// leaving the original instance intact.
/// </summary>
public abstract class Singleton<T> : StaticInstance<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        base.Awake();
    }
}

/// <summary>
/// This is a persistant version of the singleton. This will survive through scene loads.
/// Perfect for system classes which require stateful, persistent data. Or auto sources
/// where the music plays through the loading screens.
/// </summary>
public abstract class PersistentSingleton<T> : Singleton<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
}

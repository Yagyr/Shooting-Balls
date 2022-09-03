using UnityEngine;

// TODO: Вынес повторяющийся код с синглтоном в отдельный класс. Пример использования в ScoreManager.cs
public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = (T) this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
using UnityEngine;

[System.Serializable]
// TODO: Переназови класс и старайся не использовать такие же имена как в системных библиотеках (уже есть класс с названием Task)
public struct Task
{
    public ItemType itemType;
    public int number;
    public int level;
}

public class Level : MonoBehaviour
{
    public int numberOfBalls = 50;
    public int maxCreatedBallLevel = 1;
    public Task[] tasks;

    public static Level Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}


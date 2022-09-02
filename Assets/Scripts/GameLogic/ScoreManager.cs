using System.Collections;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public Level level;
    public ScoreElement[] scoreElementPrefabs;
    public ScoreElement[] scoreElements;
    public Transform itemScoreParent;
    [SerializeField] private Camera _camera;

    public static ScoreManager Instance;

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

    private void Start()
    {
        scoreElements = new ScoreElement[level.tasks.Length];
        //Проходимся по всем задачам уровня
        for (int taskIndex = 0; taskIndex < level.tasks.Length; taskIndex++)
        {
            //Задача
            Task task = level.tasks[taskIndex];
            
            ItemType itemType = task.itemType;
            for (int i = 0; i < scoreElementPrefabs.Length; i++)
            {
                if (itemType == scoreElementPrefabs[i].itemType)
                {
                    ScoreElement newScoreElement = Instantiate(scoreElementPrefabs[i], itemScoreParent);
                    newScoreElement.Setup(task);

                    scoreElements[taskIndex] = newScoreElement;
                }
            }
        }
    }

    public bool AddScore(ItemType itemType, Vector3 position, int level = 0)
    {
        for (int i = 0; i < scoreElements.Length; i++)
        {
            if (scoreElements[i].itemType == itemType)
            {
                if (scoreElements[i].currentScore != 0)
                {
                    if (scoreElements[i].level == level)
                    {
                        StartCoroutine(AddScoreAnimation(scoreElements[i], position));
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private IEnumerator AddScoreAnimation(ScoreElement scoreElement, Vector3 position)
    {
        GameObject icon = Instantiate(scoreElement.flyingIconPrefab, position, Quaternion.identity);

        Vector3 a = position;
        Vector3 b = position + Vector3.back * 3f + Vector3.down * 3f;

        Vector3 screenPosition = new Vector3(scoreElement.iconTransform.position.x, scoreElement.iconTransform.position.y, -_camera.transform.position.z);

        Vector3 d = _camera.ScreenToWorldPoint(screenPosition);
        Vector3 c = d + Vector3.back * 3f;

        for (float t = 0f; t < 1f; t += Time.deltaTime)
        {
            icon.transform.position = Bezier.GetPoint(a, b, c, d, t);
            yield return null;
        }
        Destroy(icon.gameObject);
        scoreElement.AddOne();
        
        CheckWin();
    }

    public void CheckWin()
    {
        for (int i = 0; i < scoreElements.Length; i++)
        {
            if (scoreElements[i].currentScore != 0)
            {
                return;
            }
        }
        GameManager.Instance.Win();
        Debug.Log("Win");
    }
    
}

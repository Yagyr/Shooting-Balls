using System;
using System.Collections;
using UnityEngine;

public class ScoreManager : MonoSingleton<ScoreManager>
{
    // TODO: Если переменные не нужны снаружи кроме как в движке - делай их [SerializeField] private
    public Level level;
    public ScoreElement[] scoreElementPrefabs;
    public ScoreElement[] scoreElements;
    public Transform itemScoreParent;
    [SerializeField] private Camera _camera;

    // TODO: Унес отсюда логику синглтона в MonoSingleton.cs
    // TODO: В других местах сделай тоже самое
    
    private void Start()
    {
        scoreElements = new ScoreElement[level.tasks.Length];
        //Проходимся по всем задачам уровня
        for (int taskIndex = 0; taskIndex < level.tasks.Length; taskIndex++)
        {
            //Задача
            Task task = level.tasks[taskIndex];
            
            ItemType itemType = task.itemType;
            foreach (var scoreElement in scoreElementPrefabs)
            {
                if (itemType == scoreElement.itemType)
                {
                    ScoreElement newScoreElement = Instantiate(scoreElement, itemScoreParent);
                    newScoreElement.Setup(task);

                    scoreElements[taskIndex] = newScoreElement;
                }
            }
        }
    }

    public bool AddScore(ItemType itemType, Vector3 position, int level = 0)
    {
        foreach (var scoreElement in scoreElements)
        {
            if (scoreElement.itemType == itemType &&
                scoreElement.currentScore != 0 &&
                scoreElement.level == level)
            {
                StartCoroutine(AddScoreAnimation(scoreElement, position));
                return true;
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
            // TODO: Dotween бы зарешал эту тему. Но ты жесткий конечно.
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
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public Level level;
    public ScoreElement[] scoreElementPrefabs;
    public ScoreElement[] scoreElements;
    public Transform itemScoreParent;
    [SerializeField] private Camera _camera;

    public static ScoreManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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
                    newScoreElement.Setup(task.number);

                    scoreElements[taskIndex] = newScoreElement;
                }
            }
        }
    }
}

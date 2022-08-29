using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
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
}

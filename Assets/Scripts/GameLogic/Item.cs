using UnityEngine;

public enum ItemType
{
    Empty,
    Ball,
    Barrel,
    Box,
    Stone,
    Dynamit,
    Star
}

public class Item : MonoBehaviour
{
    public ItemType itemType;
}

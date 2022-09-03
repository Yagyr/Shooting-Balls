using UnityEngine;

public class Progress : MonoBehaviour
{
    // TODO: Здесь можно было бы хранить сразу класс ProgressData
    public int coins;
    public int level;
    
    public static Progress Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        Load();
    }

    public void SetLevel(int level)
    {
        this.level = level;
        Save();
    }

    public void AddCoins(int value)
    {
        coins += value;
        Save();
    }

    [ContextMenu("DeleteFile")]
    public void DeleteFile()
    {
        SaveSystem.DeleteData();
    }
    
    [ContextMenu("Save")]
    public void Save()
    {
        SaveSystem.Save(this);
    }

    [ContextMenu("Load")]
    public void Load()
    {
        ProgressData progressData = SaveSystem.Load();
        if (progressData != null)
        {
            coins = progressData.coins;
            level = progressData.level;
        }
        else
        {
            coins = 0;
            level = 1;
        }
    }
}

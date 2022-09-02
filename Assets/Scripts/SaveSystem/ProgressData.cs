[System.Serializable]
public class ProgressData 
{
    public int coins;
    public int level;

    public ProgressData(Progress progress)
    {
        coins = progress.coins;
        level = progress.level;
    }
}

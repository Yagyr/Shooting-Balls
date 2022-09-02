using UnityEngine;

public class Stone : PassiveItem
{
    [SerializeField] private GameObject _dieEffect;
    [Range(0, 2)] 
    [SerializeField] private int _level = 2;
    
    [SerializeField] private Transform _visualTransform;
    [SerializeField] private Stone _prefabStone;
    
    public override void OnAffect()
    {
        base.OnAffect();

        if (_level > 0)
        {
            for (int i = 0; i < 2; i++)
            {
                CreateChildRock(_level - 1);
            }
        }
        else
        {
            ScoreManager.Instance.AddScore(itemType, transform.position);
        }

        Die();
    }

    private void CreateChildRock(int level)
    {
        Stone newRock = Instantiate(_prefabStone, transform.position, Quaternion.identity);
        newRock.SetLevel(level);
    }

    private void SetLevel(int level)
    {
        _level = level;
        float scale = 1f;

        if (level == 2)
        {
            scale = 1f;
        }
        else if (level == 1)
        {
            scale = 0.7f;
        }
        else if (level == 0)
        {
            scale = 0.45f;
        }

        _visualTransform.localScale = Vector3.one * scale;
    }

    private void Die()
    {
        Instantiate(_dieEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

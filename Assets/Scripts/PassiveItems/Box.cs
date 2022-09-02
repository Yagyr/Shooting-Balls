using UnityEngine;

public class Box : PassiveItem
{
    [Range(0,2)]
    public int health = 2;
    [SerializeField] private GameObject[] _levels;
    [SerializeField] private GameObject _breakEffect;
    [SerializeField] private Animator _animator;

    private void Start()
    {
        SetHealth(health);
    }

    public override void OnAffect()
    {
        base.OnAffect();

        health -= 1;
        Instantiate(_breakEffect, transform.position, Quaternion.identity);
        _animator.SetTrigger("Shake");
        if (health <= 0)
        {
            Die();
        }
        else
        {
            SetHealth(health);
        }
    }

    private void SetHealth(int value)
    {
        for (int i = 0; i < _levels.Length; i++)
        {
            _levels[i].SetActive(i <= value);
        }
    }

    private void Die()
    {
        ScoreManager.Instance.AddScore(itemType, transform.position);
        Destroy(gameObject);
    }
}

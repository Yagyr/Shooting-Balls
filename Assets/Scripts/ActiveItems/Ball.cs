using UnityEngine;

public class Ball : ActiveItem
{
    [SerializeField] private BallSettings _ballSettings;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Transform _visualTransform;

    public override void SetLevel(int level)
    {
        base.SetLevel(level);
        _renderer.material = _ballSettings.ballMaterials[level];
        
        radius = Mathf.Lerp(0.4f, 0.7f, level / 10f);
        Vector3 ballScale = Vector3.one * radius * 2;
        _visualTransform.localScale = ballScale;
        _collider.radius = radius;
        _trigger.radius = radius + 0.1f;
        
        projection.Setup(_ballSettings.ballProjectionMaterials[level], _levelText.text, radius);

        if (ScoreManager.Instance.AddScore(itemType, transform.position, level))
        {
            Die();
        }
    }

    public override void DoEffect()
    {
        base.DoEffect();
        IncreaseLevel();
    }
}

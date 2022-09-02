using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreElement : MonoBehaviour
{
    public ItemType itemType;
    public Transform iconTransform;
    public GameObject flyingIconPrefab;
    public int currentScore;
    public int level = 0;

    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private AnimationCurve _animationCurve;
    
    public void AddOne()
    {
        currentScore--;
        if (currentScore < 0)
        {
            currentScore = 0;
        }

        _text.text = currentScore.ToString();
        StartCoroutine(AddAnimation());
    }

    public virtual void Setup(Task task)
    {
        currentScore = task.number;
        _text.text = currentScore.ToString();
    }

    private IEnumerator AddAnimation()
    {
        for (float t = 0; t < 1f; t += Time.deltaTime)
        {
            float scale = _animationCurve.Evaluate(t);
            iconTransform.localScale = Vector3.one * scale;
            yield return null;
        }
        iconTransform.localScale = Vector3.one;
    }
}

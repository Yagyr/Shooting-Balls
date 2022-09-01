using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreElement : MonoBehaviour
{
    public ItemType itemType;
    public int currentScore;
    public Transform iconTransform;
    public int level = 0;
    public GameObject flyingIconPrefab;

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
        //TODO: Проверить победу
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

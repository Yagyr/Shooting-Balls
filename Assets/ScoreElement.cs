using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreElement : MonoBehaviour
{
    public ItemType itemType;
    [SerializeField] private int _currentScore;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Transform _iconTransform;
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private int _level;
    public GameObject flyingIconPrefab;

    public void AddOne()
    {
        _currentScore--;
        if (_currentScore < 0)
        {
            _currentScore = 0;
        }

        _text.text = _currentScore.ToString();
        StartCoroutine(AddAnimation());
        //TODO: Проверить победу
    }

    public void Setup(int number)
    {
        _currentScore = number;
        _text.text = _currentScore.ToString();
    }

    private IEnumerator AddAnimation()
    {
        for (float t = 0; t < 1; t += Time.deltaTime * 1.8f)
        {
            float scale = _animationCurve.Evaluate(t);
            _iconTransform.localScale = Vector3.one * scale;
            yield return null;
        }
        _iconTransform.localScale = Vector3.one;
    }
}

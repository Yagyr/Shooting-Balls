using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Projection : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Text _text;
    [SerializeField] private Transform _visualTransform;

    public void Setup(Material material, string numberText, float radius)
    {
        _renderer.material = material;
        _text.text = numberText;
        _visualTransform.localScale = radius * Vector3.one * 2f;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
}

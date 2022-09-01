using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Creator : MonoBehaviour
{
    [SerializeField] private Transform _tube;
    [SerializeField] private Transform _spawner;
    [SerializeField] private Transform _rayTransform;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private ActiveItem _ballPrefab;
    [SerializeField] private TextMeshProUGUI _textBallLeft;

    private Coroutine _waitForLose;
    private ActiveItem _itemInTube;
    private ActiveItem _itemInSpawner;
    private int _ballsLeft;

    private void Start()
    {
        _ballsLeft = Level.Instance.numberOfBalls;
        UpdateBallsLeftText();
        
        CreateItemInTube();
        StartCoroutine(MoveToSpawner());
    }

    public void UpdateBallsLeftText()
    {
        _textBallLeft.text = _ballsLeft.ToString();
    }

    private void CreateItemInTube()
    {
        if (_ballsLeft == 0)
        {
            return;
        }
        //Назначаем случайный уровень шару
        int itemLevel = Random.Range(0, 5);
        _itemInTube = Instantiate(_ballPrefab, _tube.position, Quaternion.identity);
        _itemInTube.SetLevel(itemLevel);
        _itemInTube.SetupToTube();
        _ballsLeft--;
        UpdateBallsLeftText();
    }

    //Движение мяча из трубы до спавнера
    private IEnumerator MoveToSpawner()
    {
        _itemInTube.transform.parent = _spawner;
        for (float t = 0; t < 1f; t += Time.deltaTime / 0.5f)
        {
            _itemInTube.transform.position = Vector3.Lerp(_tube.position, _spawner.position, t);
            yield return null;
        }

        _itemInTube.transform.localPosition = Vector3.zero;
        _itemInSpawner = _itemInTube;
        _itemInSpawner.projection.Show();
        _rayTransform.gameObject.SetActive(true);
        _itemInTube = null;
        CreateItemInTube();
    }

    private void LateUpdate()
    {
        if (_itemInSpawner)
        {
            Ray ray = new Ray(_spawner.position, Vector3.down);
            RaycastHit hit;
            if (Physics.SphereCast(ray, _itemInSpawner.radius, out hit, 100, _layerMask, QueryTriggerInteraction.Ignore))
            {
                _rayTransform.localScale = new Vector3(_itemInSpawner.radius * 2, hit.distance, 1);
                _itemInSpawner.projection.SetPosition(_spawner.position + Vector3.down * hit.distance);
            }
            
            if (Input.GetMouseButtonUp(0))
            {
                Drop();
            }
        }
    }

    private void Drop()
    {
        _itemInSpawner.Drop();
        _itemInSpawner.projection.Hide();
        _itemInSpawner = null;
        _rayTransform.gameObject.SetActive(false);
        if (_itemInTube)
        {
            StartCoroutine(MoveToSpawner());
        }
        else
        {
            _waitForLose = StartCoroutine(WaitForLose());
            CollapseManager.Instance.onCollapse.AddListener(ResetLoseTimer);
            GameManager.Instance.onWin.AddListener(StopWaitToLose);
        }
    }

    private void ResetLoseTimer()
    {
        if (_waitForLose != null)
        {
            StopCoroutine(_waitForLose);
            _waitForLose = StartCoroutine(WaitForLose());
        }
    }

    private void StopWaitToLose()
    {
        if (_waitForLose != null)
        {
            StopCoroutine(_waitForLose);
        }
    }

    private IEnumerator WaitForLose()
    {
        for (float t = 0f; t < 5f; t += Time.deltaTime)
        {
            yield return null;
        }
        GameManager.Instance.Lose();
        Debug.Log("Your Lose");
    }
}

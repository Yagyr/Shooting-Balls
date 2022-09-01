using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinsText;
    [SerializeField] private TextMeshProUGUI _levelsText;
    [SerializeField] private Button _startButton;

    private void Start()
    {
        _coinsText.text = Progress.Instance.coins.ToString();
        _levelsText.text = Progress.Instance.level.ToString();
        _startButton.onClick.AddListener(StartLevel);
    }

    private void StartLevel()
    {
        SceneManager.LoadScene(Progress.Instance.level);
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinsText;
    [SerializeField] private TextMeshProUGUI _levelsText;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _restartButton;

    private void Start()
    {
        _coinsText.text = Progress.Instance.coins.ToString();
        _levelsText.text = Progress.Instance.level.ToString();
        _startButton.onClick.AddListener(StartLevel);
        _restartButton.onClick.AddListener(RestartGame);
    }

    private void StartLevel()
    {
        SceneManager.LoadScene(Progress.Instance.level);
    }

    private void RestartGame()
    {
        Progress.Instance.DeleteFile();
        Progress.Instance.Load();
        SceneManager.LoadScene(0);
    }
}

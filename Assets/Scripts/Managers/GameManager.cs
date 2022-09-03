using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _winObject;
    [SerializeField] private GameObject _loseObject;

    public static GameManager Instance;
    
    // TODO: UnityEvent лучше не использовать в этом контексте - лучше уж референснуть Spawner и Creator и выключить их через код.
    // А так сложнее читать код, становится непонятно, что там за логика происходит на onWin, пока не заглянешь в редактор.
    public UnityEvent onWin;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Win()
    {
        _winObject.SetActive(true);
        onWin.Invoke();
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        Progress.Instance.SetLevel(currentLevelIndex + 1);
        
        // TODO: Было бы круто где-то отображать то что я получил +50
        Progress.Instance.AddCoins(50);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(Progress.Instance.level);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Lose()
    {
        _loseObject.SetActive(true);
    }
}

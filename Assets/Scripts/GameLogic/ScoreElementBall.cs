using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreElementBall : ScoreElement
{
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private RawImage _ballImage;
    [SerializeField] private BallSettings _ballSettings;
    
    public override void Setup(Task task)
    {
        base.Setup(task);

        int number = (int)Mathf.Pow(2, task.level + 1);
        _levelText.text = number.ToString();
        _ballImage.color = _ballSettings.ballMaterials[task.level].color;
        level = task.level;
    }
}

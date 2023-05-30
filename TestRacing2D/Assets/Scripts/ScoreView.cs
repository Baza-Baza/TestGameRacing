using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private Color _defaultTextColor;
    [SerializeField]
    private Color _customizeTextColor;

    public void UpdateScore(int score, bool defaulTextColor)
    {
        _scoreText.text = "Score:\n" + score.ToString();

        _scoreText.color = defaulTextColor ? _defaultTextColor : _customizeTextColor;
    }
}

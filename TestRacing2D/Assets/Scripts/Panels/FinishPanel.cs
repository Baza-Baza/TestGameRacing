using System;
using TMPro;
using UnityEngine;

public class FinishPanel : MonoBehaviour
{
    [SerializeField]
    private AbstractBtn _restartBtn;
    [SerializeField]
    private AbstractBtn _exitBtn;
    [SerializeField]
    private TextMeshProUGUI _bestScoreText;
    [SerializeField]
    private TextMeshProUGUI _currentScoreText;

    public Action<int> ActionFinishPanel
    {
        get;
        set;
    }

    private void OnEnable()
    {
        _restartBtn.PressButton += RestartGame;
        _exitBtn.PressButton += ExitGame;
    }

    private void OnDisable()
    {
        _restartBtn.PressButton -= RestartGame;
        _exitBtn.PressButton -= ExitGame;
    }

    public void UpdateTexts(int bestScore, int currentScore)
    {
        _bestScoreText.text = "Best Score:\n" + bestScore.ToString();
        _currentScoreText.text = "Your Score:\n" + currentScore.ToString();
    }

    private void RestartGame(bool isDown)
    {
        ActionFinishPanel.Invoke(0);
    }

    private void ExitGame(bool isDown)
    {
        ActionFinishPanel.Invoke(1);
    }
}

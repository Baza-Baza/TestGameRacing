using System;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    [SerializeField]
    private AbstractBtn _restartBtn;
    [SerializeField]
    private AbstractBtn _optionsBtn;
    [SerializeField]
    private AbstractBtn _exitBtn;
    [SerializeField]
    private AbstractBtn _closePanelBtn;

    private void OnEnable()
    {
        _restartBtn.PressButton += RestartGame;
        _optionsBtn.PressButton += OpenOptions;
        _exitBtn.PressButton += ExitGame;
        _closePanelBtn.PressButton += ClosePanel;
    }

    private void OnDisable()
    {
        _restartBtn.PressButton -= RestartGame;
        _optionsBtn.PressButton -= OpenOptions;
        _exitBtn.PressButton -= ExitGame;
        _closePanelBtn.PressButton -= ClosePanel;
    }

    public Action<int> ActionPausePanel
    {
        get;
        set;
    }

    private void RestartGame(bool isDown)
    {
        ActionPausePanel.Invoke(0);
    }

    private void OpenOptions(bool isDown)
    {
        ActionPausePanel.Invoke(2);
    }

    private void ExitGame(bool isDown)
    {
        ActionPausePanel.Invoke(1);
    }

    private void ClosePanel(bool isDown)
    {
        ActionPausePanel.Invoke(3);
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneController : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField]
    private CanvasScaler _canvasScaler;
    [SerializeField]
    private UIScaler _uiScaler;
    [SerializeField]
    private TimerView _timerView;
    [SerializeField]
    private ScoreView _scoreView;
    [SerializeField]
    private BarView _barView;
    [Space(5)]
    [Header("Spawn Comntrollers")]
    [SerializeField]
    private SpawnerRoad _spawnerRoad;
    [SerializeField]
    private RoadZone _roadZone;
    [Space(5)]
    [Header("Transforms")]
    [SerializeField]
    private Transform _finishPointTransform;
    [Space(5)]
    [Header("Timers")]
    [SerializeField]
    private int _timerStart;
    [SerializeField]
    private int _timerPoliceCarStart;
    [SerializeField]
    private int _timerSlowdown;
    [SerializeField]
    private int _timerBonusAction;
    [Space(5)]
    [Header("Buttons")]
    [SerializeField]
    private AbstractBtn _leftMoveBtn;
    [SerializeField]
    private AbstractBtn _rightMoveBtn;
    [SerializeField]
    private AbstractBtn _gasPedalBtn;
    [SerializeField]
    private AbstractBtn _brackPedalBtn;
    [SerializeField]
    private AbstractBtn _pauseBtn;
    [Space(5)]
    [Header("Panels")]
    [SerializeField]
    private PausePanel _pausePanel;
    [SerializeField]
    private FinishPanel _finishPanel;

    private GameModel _model;

    private void OnEnable()
    {
        int x = Screen.width;
        int y = Screen.height;

        _canvasScaler.referenceResolution = new Vector2(x, y);
        _model = new GameModel(y, x);
        _spawnerRoad.StartSpawn(_model);
        _roadZone.SpawnCar(_model);
        _uiScaler.SetSizeUI(_model);
        _finishPointTransform.position = new Vector2(x / 2, -y);

        _roadZone.NotificationStartPosAction += ChangeSpeedRoad;
        _roadZone.CarTriggerWithObjectAction += Handletrigger;
        _roadZone.PoliceCarCrashed += SetTimerForCreatePC;

        _leftMoveBtn.PressButton += MoveCarLeft;
        _rightMoveBtn.PressButton += MoveCarRight;
        _gasPedalBtn.PressButton += MoveCarUp;
        _brackPedalBtn.PressButton += MoveCarDown;
        _pauseBtn.PressButton += OpenPausePanel;

        ChangeInteractbleMovementBtns(false);
        UpdateScore(true);
    }

    private void Start()
    {
        StartCoroutine(DelayStartGame());
        StartCoroutine(DelayPoliceCarCreate());
    }

    private void OnDisable()
    {
        _roadZone.DestroyCars();

        _roadZone.NotificationStartPosAction -= ChangeSpeedRoad;

        _leftMoveBtn.PressButton -= MoveCarLeft;
        _rightMoveBtn.PressButton -= MoveCarRight;
        _gasPedalBtn.PressButton -= MoveCarUp;
        _brackPedalBtn.PressButton -= MoveCarDown;
        _pauseBtn.PressButton -= OpenPausePanel;

        _roadZone.CarTriggerWithObjectAction -= Handletrigger;
        _roadZone.PoliceCarCrashed -= SetTimerForCreatePC;
        _finishPanel.ActionFinishPanel -= ActionPausePanel;
    }

    private void ChangeSpeedRoad()
    {
        _model.SpeedRoad = 400;
        SetSpeedRoad();
        ChangeInteractbleMovementBtns(true);
        _timerView.SetImage(0);
        _timerView.gameObject.SetActive(false);
    }

    private void SetSpeedRoad()
    {
        _spawnerRoad.ChangeSpeedRoads();
    }

    private void MoveCarLeft(bool isDown)
    {
        _roadZone.SetDirectionCar(-1, isDown);
        _uiScaler.RotateWheel(-1);
    }

    private void MoveCarRight(bool isDown)
    {
        _roadZone.SetDirectionCar(1, isDown);
        _uiScaler.RotateWheel(1);
    }

    private void MoveCarUp(bool isDown)
    {
        _model.SpeedRoad = isDown ? 1000 : 400;

        SetSpeedRoad();
        _roadZone.SetPosForPoliceCar(isDown);
    }

    private void MoveCarDown(bool isDown)
    {
        _model.SpeedRoad = isDown ? 200 : 400;

        SetSpeedRoad();
        _roadZone.SetPosForPoliceCar(false);
    }

    private void ChangeInteractbleMovementBtns(bool value)
    {
        _gasPedalBtn.ChangeInteractble(value);
        _brackPedalBtn.ChangeInteractble(value);
        _leftMoveBtn.ChangeInteractble(value);
        _rightMoveBtn.ChangeInteractble(value);
    }

    private void OpenPausePanel(bool isDown)
    {
        Time.timeScale = 0;
        _pausePanel.gameObject.SetActive(true);
        _pausePanel.ActionPausePanel += ActionPausePanel;
    }

    private void ActionPausePanel(int action)
    {
        switch (action)
        {
            case 0:
                RestartGame();
                break;
            case 1:
                ExitGame();
                break;
            case 2:
                OpenOptions();
                break;
            case 3:
                ClosePausePanel();
                break;
        }
    }

    private void RestartGame()
    {
        ClosePausePanel();
        SceneManager.LoadScene(0);
    }

    private void OpenOptions()
    {

    }

    private void ExitGame()
    {
        Application.Quit();
    }

    private void ClosePausePanel()
    {
        _pausePanel.gameObject.SetActive(false);
        _pausePanel.ActionPausePanel -= ActionPausePanel;
        Time.timeScale = 1;
    }

    private void Handletrigger(TriggersEnum triggersEnum)
    {
        switch (triggersEnum)
        {
            case TriggersEnum.Heart:
                UpdateHealth(0.25f);
                break;
            case TriggersEnum.Coin:
                AddPoints();
                break;
            case TriggersEnum.Block:
                if (_model.CanGetDamage)
                {
                    UpdateHealth(-_model.Health);
                }
                break;
            case TriggersEnum.Crack:
                if (_model.CanGetDamage)
                {
                    SetSlowDown();
                    UpdateHealth(-0.25f);
                }
                break;
            case TriggersEnum.Oil:
                if (_model.CanGetDamage)
                {
                    SetSlowDown();
                }
                break;
            case TriggersEnum.Magnet:
                SetMagnetState();
                break;
            case TriggersEnum.Shield:
                SetShieldState();
                break;
            case TriggersEnum.Nitro:
                SetNitroState();
                break;
            case TriggersEnum.Police:
                if (_model.CanGetDamage)
                {
                    UpdateHealth(-_model.Health);
                }
                break;
        }
    }

    private void AddPoints()
    {
        _model.CurrentScore = 10;

        if (_model.CurrentScore > _model.BestSrore)
        {
            _model.BestSrore = _model.CurrentScore;
            UpdateScore(false);
        }
        else
        {
            UpdateScore(true);
        }
    }

    private void UpdateHealth(float value)
    {
        _model.Health = value;
        _barView.UpdateHealthBar(_model.Health);

        if (_model.Health == 0)
        {
            OpenFinishPanel();
        }
    }

    private void UpdateScore(bool defaultColor)
    {
        _scoreView.UpdateScore(_model.CurrentScore, defaultColor);
    }

    private void OpenFinishPanel()
    {
        Time.timeScale = 0;
        _finishPanel.gameObject.SetActive(true);
        _finishPanel.UpdateTexts(_model.BestSrore, _model.CurrentScore);

        _finishPanel.ActionFinishPanel += ActionPausePanel;
    }

    private void SetTimerForCreatePC()
    {
        StartCoroutine(DelayPoliceCarCreate());
    }

    private void SetSlowDown()
    {
        if (_model.CanBeSlowdown)
        {
            StartCoroutine(DelaySlowdown());
        }
    }

    private void SetMagnetState()
    {
        if (_model.CanGetBonus)
        {
            _roadZone.SetStateForCar(State.Magnet);
            StartCoroutine(DelayMagnetBonus());
        }
    }

    private void SetNitroState()
    {
        if (_model.CanGetBonus)
        {
            _roadZone.SetStateForCar(State.Nitro);
            StartCoroutine(DelayNitroBonus());
        }
    }

    private void SetShieldState()
    {
        if (_model.CanGetBonus)
        {
            _roadZone.SetStateForCar(State.Shield);
            StartCoroutine(DelayShieldBonus());
        }
    }

    private void SetDefaultState()
    {
        _roadZone.SetStateForCar(State.Default);

    }

    private IEnumerator DelaySlowdown()
    {
        _gasPedalBtn.ChangeInteractble(false);
        _model.SpeedRoad = 400;
        SetSpeedRoad();

        yield return new WaitForSeconds(_timerSlowdown);

        _gasPedalBtn.ChangeInteractble(true);
        SetSpeedRoad();
    }

    private IEnumerator DelayPoliceCarCreate()
    {
        yield return new WaitForSeconds(_timerPoliceCarStart);

        _roadZone.InstantiatePoliceCar();
    }

    private IEnumerator DelayStartGame()
    {
        for (int i = _timerStart; i > -1; i--)
        {

            if (i == 0)
            {
                _spawnerRoad.StartMoveRoad();
                _roadZone.StartMoveCar();
                _timerView.SetImage(0);
            }
            else if (i == 1)
            {
                _timerView.gameObject.SetActive(true);
                _timerView.SetImage(1);
            }
            else if (i == 2)
            {
                _timerView.SetImage(2);
            }
            else if (i == 3)
            {
                _timerView.SetImage(3);
            }

            yield return new WaitForSeconds(1);
        }
    }

    private IEnumerator DelayMagnetBonus()
    {
        _barView.ActiveMagnetBar(true);
        _model.CanGetBonus = false;

        for (int i = _timerBonusAction; i > -1; i--)
        {
            _barView.UpdateMagnetBar();
            yield return new WaitForSeconds(1);
        }

        _barView.ActiveMagnetBar(false);
        SetDefaultState();
        _model.CanGetBonus = true;
    }

    private IEnumerator DelayNitroBonus()
    {
        _barView.ActiveNitroBar(true);
        _model.CanGetBonus = false;
        _model.CanBeSlowdown = false;

        for (int i = _timerBonusAction; i > -1; i--)
        {
            _barView.UpdateShieldBar();
            yield return new WaitForSeconds(1);
        }

        _barView.ActiveNitroBar(false);
        SetDefaultState();
        _model.CanGetBonus = true;
        _model.CanBeSlowdown = true;
    }

    private IEnumerator DelayShieldBonus()
    {
        _barView.ActiveShieldBar(true);
        _model.CanGetBonus = false;
        _model.CanGetDamage = false;

        for (int i = _timerBonusAction; i > -1; i--)
        {
            _barView.UpdateShieldBar();
            yield return new WaitForSeconds(1);
        }

        _barView.ActiveNitroBar(false);
        SetDefaultState();
        _model.CanGetBonus = true;
        _model.CanGetDamage = true;
    }
}
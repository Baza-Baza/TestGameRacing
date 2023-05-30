using UnityEngine;

public class GameModel
{
    private int _sizeScreenX;
    private int _sizeScreenY;
    private int _firstPoint;
    private int _secondPont;
    private int _thirdPoint;
    private int _speedRoad;
    private int _bestScore;
    private int _currentScore;
    private float _health;

    private bool _canGetBonus;
    private bool _canGetDamage;
    private bool _canBeSlowdown;

    private string _scoreKey = "ScoreKey";

    public int SizeScreenX
    {
        get
        {
            return _sizeScreenX;
        }
    }

    public int SizeScreenY
    {
        get
        {
            return _sizeScreenY;
        }
    }

    public int FirstPoint
    {
        get
        {
            return _firstPoint;
        }
    }

    public int SecondPoint
    {
        get
        {
            return _secondPont;
        }
    }

    public int ThirdPoint
    {
        get
        {
            return _thirdPoint;
        }
    }

    public int SpeedRoad
    {
        get
        {
            return _speedRoad;
        }
        set
        {
            _speedRoad = value;
        }
    }

    public int BestSrore
    {
        get
        {
            return _bestScore;
        }
        set
        {
            _bestScore = value;
            PlayerPrefs.SetInt(_scoreKey, _bestScore);
        }
    }

    public int CurrentScore
    {
        get
        {
            return _currentScore;
        }
        set
        {
            _currentScore += value;
        }
    }

    public float Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health += value;

            _health = Mathf.Clamp(_health, 0, 1);
        }
    }

    public bool CanGetBonus
    {
        get 
        {
            return _canGetBonus;
        }
        set
        {
            _canGetBonus = value;
        }
    }

    public bool CanGetDamage
    {
        get
        {
            return _canGetDamage;
        }
        set
        {
            _canGetDamage = value;
        }
    }

    public bool CanBeSlowdown
    {
        get
        {
            return _canBeSlowdown;
        }
        set
        {
            _canBeSlowdown = value;
        }
    }

    public GameModel(int screenSizeY, int screenSizeX)
    {
        _sizeScreenY = screenSizeY;
        _sizeScreenX = screenSizeX;
        _firstPoint = 0;
        _secondPont = screenSizeY;
        _thirdPoint = _secondPont * 2;
        _speedRoad = 1000;
        _currentScore = 0;
        _health = 1;
        _canGetBonus = true;
        _canGetDamage = true;
        _canBeSlowdown = true;

        if (PlayerPrefs.HasKey(_scoreKey))
        {
            _bestScore = PlayerPrefs.GetInt(_scoreKey);
        }
        else
        {
            _bestScore = 0;
        }
    }
}
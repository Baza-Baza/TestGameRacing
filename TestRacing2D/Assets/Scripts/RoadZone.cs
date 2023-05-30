using System;
using UnityEngine;

public class RoadZone : MonoBehaviour
{
    [SerializeField]
    private GameObject _carPrefab;
    [SerializeField]
    private GameObject _policeCarPrefab;

    private GameModel _gameModel;
    private GameObject _liveCarGO;
    private GameObject _livePoliceCar;
    private Car _car;
    private PoliceCar _policeCar;
    private CarStates _carStates;
    private CarInteraction _carInteraction;

    public Action NotificationStartPosAction
    {
        get;
        set;
    }

    public Action<TriggersEnum> CarTriggerWithObjectAction
    {
        get;
        set;
    }

    public Action PoliceCarCrashed
    {
        get;
        set;
    }

    public void SpawnCar(GameModel gameModel)
    {
        _gameModel = gameModel;

        InstantiateCar();
    }

    public void SetDirectionCar(int direction, bool isDown)
    {
        _car.MoveCarX(direction, isDown);

        if (_policeCar != null)
        {
            _policeCar.MovePoliceCarX(direction, isDown);
        }
    }

    public void StartMoveCar()
    {
        _car.SetStartStartPosition();
    }

    public void DestroyCars()
    {
        _car.ReachedStartPos -= NotificationCarReachedStartPos;
        _carInteraction.TriggerWithObject -= CarTriggerWithObject;
    }

    public void InstantiatePoliceCar()
    {
        _livePoliceCar = Instantiate(_policeCarPrefab, transform);
        _policeCar = _livePoliceCar.GetComponent<PoliceCar>();

        _policeCar.PoliceCarCrashed += NotificationPoliceCarCrashed;
        _policeCar.SetData(_car.PosX, _gameModel.SizeScreenY);

        bool defaultPos = _gameModel.SpeedRoad == 1000 ? true : false;

        SetPosForPoliceCar(defaultPos); 
    }

    public void SetPosForPoliceCar(bool defaultPos)
    {
        if (_policeCar != null)
        {
            if (defaultPos)
            {
                _policeCar.MovePoliceCarY(_gameModel.SizeScreenY / 4);
            }
            else
            {
                _policeCar.MovePoliceCarY(50);
            }
        }
    }

    public void SetStateForCar(State state)
    {
        _carStates.SetState(state);

        if (state == State.Nitro && _livePoliceCar != null)
        {
            _policeCar.MoveToStartPos();
        }
    }

    private void InstantiateCar()
    {
        _liveCarGO = Instantiate(_carPrefab, transform);

        _car = _liveCarGO.GetComponent<Car>();
        _carStates = _liveCarGO.GetComponent<CarStates>();
        _carInteraction = _liveCarGO.GetComponent<CarInteraction>();

        _car.SetData(_gameModel.SizeScreenX, _gameModel.SizeScreenY);
        _car.ReachedStartPos += NotificationCarReachedStartPos;

        SetStateForCar(State.Default);

        _carInteraction.TriggerWithObject += CarTriggerWithObject;
    }

    private void NotificationCarReachedStartPos()
    {
        NotificationStartPosAction.Invoke();
    }

    private void CarTriggerWithObject(TriggersEnum triggersEnum)
    {
        CarTriggerWithObjectAction.Invoke(triggersEnum);
    }

    private void NotificationPoliceCarCrashed()
    {
        _policeCar.PoliceCarCrashed -= NotificationPoliceCarCrashed;
        PoliceCarCrashed.Invoke();

        Destroy(_livePoliceCar);

        _policeCar = null;
        _livePoliceCar = null;
    }
}
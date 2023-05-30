using System.Collections.Generic;
using UnityEngine;

public class SpawnerRoad : MonoBehaviour
{
    [Header("RoadsPrefabs")]
    [SerializeField]
    private List<Road> _roads;
    [Space(5)]
    [Header("ListPointsSpawn")]
    [SerializeField]
    private List<int> _spawnPoints;
    [SerializeField]
    private List<Road> _liveRoads;
    private int _numberRoad;
    private GameModel _gameModel;

    public void StartSpawn(GameModel gameModel)
    {
        _gameModel = gameModel;
        _liveRoads = new List<Road>();

        _spawnPoints.Add(_gameModel.FirstPoint);
        _spawnPoints.Add(_gameModel.SecondPoint);
        _spawnPoints.Add(_gameModel.ThirdPoint);

        _numberRoad = 0;

        CreateRoad(3);
    }

    public void StartMoveRoad()
    {
        foreach (Road road in _liveRoads)
        {
            road.SetMoveRoad(true);
        }
    }

    public void ChangeSpeedRoads()
    {
        int speed = _gameModel.SpeedRoad;

        foreach (Road road in _liveRoads)
        {
            road.SpeedMove = speed;
        }
    }

    private void CreateRoad(int count)
    {
        if (count == 3)
        {
            for (int i = 0; i < count; i++)
            {
                InstatiateRoad(i, false);
            }
        }
        else
        {
            InstatiateRoad(2, true);
            _liveRoads[_liveRoads.Count - 1].SetMoveRoad(true);
        }
    }

    private void InstatiateRoad(int numberPoint, bool isMove)
    {
        GameObject roadGo = Instantiate(_roads[_numberRoad].gameObject, transform);
        Road road = roadGo.GetComponent<Road>();
        _liveRoads.Add(road);

        road.SetData(_spawnPoints[numberPoint], _gameModel.SizeScreenX, _gameModel.SizeScreenY);

        road.SpeedMove = _gameModel.SpeedRoad;
        road.TargetPos += DestroyRoad;

        _numberRoad = CheckNumberRoad();
    }

    private int CheckNumberRoad()
    {
        _numberRoad++;

        if (_numberRoad == _roads.Count)
        {
            return 1;
        }
        else
        {
            return _numberRoad;
        }
    }

    private void DestroyRoad()
    {
        _liveRoads[0].SetMoveRoad(false);
        _liveRoads[0].TargetPos -= DestroyRoad;
        Destroy(_liveRoads[0].gameObject);
        _liveRoads.RemoveAt(0);

        CreateRoad(1);
    }
}
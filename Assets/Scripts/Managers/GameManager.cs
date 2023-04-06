using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IUpdate, IEventListener
{
    [SerializeField]
    int _spawnTime;
    [SerializeField]
    int _numAsteroidsPerLevel;

    int _currLevel = 1;
    int _bigAsteroidsDestroyed;
    int _numberAsteroidsGoal;

    float _timer = 0;

    List<GameObject> _asteroids = new List<GameObject>();

    [SerializeField]
    GameObject _canvas;

    [SerializeField]
    GameObject _gameOverScreen;



    void OnDisable()
    {
        UpdateManager.Instance.updates.Remove(this);
        CancelEventListenerSubscriptions();
    }

    void OnDestroy()
    {
        CancelEventListenerSubscriptions();
    }

    void Start()
    {
        UpdateManager.Instance.updates.Add(this);
        OnEnableEventListenerSubscriptions();
    }

    public int BigAsteroidsDestroyed
    {
        get => _bigAsteroidsDestroyed;

        set
        {
            _bigAsteroidsDestroyed = value;

            if (_bigAsteroidsDestroyed >= _numberAsteroidsGoal)
            {
                _currLevel++;
                _numAsteroidsPerLevel *= _currLevel;
                _numberAsteroidsGoal = _numAsteroidsPerLevel;
                _bigAsteroidsDestroyed = 0;
            }
        }
    }


    public void IUpdate()
    {
        if (_asteroids.Count < _numAsteroidsPerLevel)
        {
            _timer += Time.deltaTime;

            if (_timer > _spawnTime)
            {
                EventManager.TriggerEvent(GenericEvents.SpawnAsteroid, new Hashtable()
                {
                    {DataEventHashtableParams.asteroids.ToString(),_asteroids }
                });

                _timer = 0;
            }

        }
    }


    public void GameOver(Hashtable data)
    {
        var obj = Instantiate(_gameOverScreen, _canvas.transform);
    }

    public void OnEnableEventListenerSubscriptions()
    {
        EventManager.StartListening(GenericEvents.GameOver, GameOver);
    }

    public void CancelEventListenerSubscriptions()
    {
        EventManager.StopListening(GenericEvents.GameOver, GameOver);
    }
}

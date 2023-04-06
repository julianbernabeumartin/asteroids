using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IUpdate, IEventListener
{
    [SerializeField]
    int _spawnTime;
    [SerializeField]
    int _numAsteroidsPerLevel;

    int _currLevel = 1;
    int _maxLevel = 10;
    int _bigAsteroidsDestroyed;
    int _numberAsteroidsGoal;


    float _timer = 0;

    [SerializeField]
    List<GameObject> _asteroids = new List<GameObject>();

    [SerializeField]
    GameObject _canvas;

    [SerializeField]
    GameObject _gameOverScreen;

    [SerializeField]
    Player _playerPrefab;

    public int BigAsteroidsDestroyed
    {
        get => _bigAsteroidsDestroyed;

        set
        {
            _bigAsteroidsDestroyed = value;

            if (_bigAsteroidsDestroyed >= _numberAsteroidsGoal)
            {
                if (_currLevel < _maxLevel)
                {
                    _currLevel++;
                    _numAsteroidsPerLevel *= _currLevel;

                }
                _numberAsteroidsGoal = _numAsteroidsPerLevel;
                _bigAsteroidsDestroyed = 0;
            }
        }
    }



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

    private void SetValues()
    {
        _spawnTime = 1;
        _numAsteroidsPerLevel = 2;
        _currLevel = 1;
        _maxLevel = 10;
        _bigAsteroidsDestroyed = 0;
        _numberAsteroidsGoal = _numAsteroidsPerLevel;
        _timer = 0;
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

    public void UpdateAsteroidsDestroyed(Hashtable data)
    {
        Collider2D asteroid = (Collider2D)data[DataEventHashtableParams.Collider.ToString()];
        _asteroids.Remove(asteroid.gameObject);
        BigAsteroidsDestroyed++;
    }

    public void GameOver(Hashtable data)
    {
        var obj = Instantiate(_gameOverScreen, _canvas.transform);
        StartCoroutine(RestartGame());
    }

    public IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void OnEnableEventListenerSubscriptions()
    {
        EventManager.StartListening(GenericEvents.GameOver, GameOver);
        EventManager.StartListening(GenericEvents.UpdateAsteroidsDestroyed, UpdateAsteroidsDestroyed);
    }

    public void CancelEventListenerSubscriptions()
    {
        EventManager.StopListening(GenericEvents.GameOver, GameOver);
        EventManager.StopListening(GenericEvents.UpdateAsteroidsDestroyed, UpdateAsteroidsDestroyed);
    }
}

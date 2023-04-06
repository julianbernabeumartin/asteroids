using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour, IEventListener
{
    // class used to spawn asteroids in the level

    #region VARIABLES
    [SerializeField]
    int _spawnTime;
    [SerializeField]
    int _limitAsteroidsOnScreen;
    [SerializeField]
    float _closeDistanceSpawn;

    float _timer;

    Vector2 _top;
    Vector2 _right;

    Camera _cam;

    Player _player;
    #endregion

    #region MONOBEHAVIOUR METHODS
    private void Awake()
    {
        _player = FindAnyObjectByType<Player>();
        _cam = Camera.main;
        SetLimits();
    }

    void Start()
    {
        OnEnableEventListenerSubscriptions();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(_top, Vector3.one / 2);
        Gizmos.DrawWireCube(_right, Vector3.one / 2);

    }
    #endregion

    #region CLASS METHODS
    void SetLimits()
    {
        _top = (Vector2)_cam.ScreenToWorldPoint(new Vector3(_cam.pixelWidth / 2, _cam.pixelHeight, _cam.nearClipPlane));
        _right = (Vector2)_cam.ScreenToWorldPoint(new Vector3(_cam.pixelWidth, _cam.pixelHeight / 2, _cam.nearClipPlane));

    }

    public void SpawnAsteroid(Hashtable data)
    {
        List<GameObject> asteroids = (List<GameObject>)data[DataEventHashtableParams.asteroids.ToString()];
        if (_player == null) return;

        var spawnPos = ValidSpawnPoint();
        var obj = PoolManager.AstroidBigPool.Spawn();
        //obj.transform.position = new Vector3(UnityEngine.Random.Range(-_right.x, _right.x), UnityEngine.Random.Range(-_top.y, _top.y), 0);
        obj.transform.position = spawnPos;
        asteroids.Add(obj);
    }

    Vector3 ValidSpawnPoint()
    {
        Vector3 tempPos = new Vector3(UnityEngine.Random.Range(-_right.x, _right.x), UnityEngine.Random.Range(-_top.y, _top.y), 0);

        Vector3 offset = _player.transform.position - tempPos;
        float sqrLen = offset.sqrMagnitude;

        if (sqrLen < _closeDistanceSpawn * _closeDistanceSpawn)
        {
            tempPos = ValidSpawnPoint();
        }

        return tempPos;
    }

    public void OnEnableEventListenerSubscriptions()
    {
        EventManager.StartListening(GenericEvents.SpawnAsteroid, SpawnAsteroid);
    }

    public void CancelEventListenerSubscriptions()
    {
        EventManager.StopListening(GenericEvents.SpawnAsteroid, SpawnAsteroid);
    }
    #endregion



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour, IEventListener
{
    [SerializeField]
    int _spawnTime;
    [SerializeField]
    int _limitAsteroidsOnScreen;

    float _timer;

    Vector2 _top;
    Vector2 _right;

    Camera _cam;



    private void Awake()
    {
        _cam = Camera.main;
        SetLimits();
    }

    void Start()
    {
        OnEnableEventListenerSubscriptions();
    }

    void SetLimits()
    {
        _top = (Vector2)_cam.ScreenToWorldPoint(new Vector3(_cam.pixelWidth / 2, _cam.pixelHeight, _cam.nearClipPlane));
        _right = (Vector2)_cam.ScreenToWorldPoint(new Vector3(_cam.pixelWidth, _cam.pixelHeight / 2, _cam.nearClipPlane));

    }



    public void SpawnAsteroid(Hashtable data)
    {
        List<GameObject> asteroids = (List<GameObject>)data[DataEventHashtableParams.asteroids.ToString()];

        var obj = PoolManager.AstroidBigPool.Spawn();
        obj.transform.position = new Vector3(UnityEngine.Random.Range(-_right.x, _right.x), UnityEngine.Random.Range(-_top.y, _top.y), 0);
        asteroids.Add(obj);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(_top, Vector3.one / 2);
        Gizmos.DrawWireCube(_right, Vector3.one / 2);

    }

    public void OnEnableEventListenerSubscriptions()
    {
        EventManager.StartListening(GenericEvents.SpawnAsteroid, SpawnAsteroid);
    }

    public void CancelEventListenerSubscriptions()
    {
        EventManager.StopListening(GenericEvents.SpawnAsteroid, SpawnAsteroid);
    }
}

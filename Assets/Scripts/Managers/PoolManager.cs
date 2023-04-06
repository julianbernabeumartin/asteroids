using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour, IEventListener
{
    [SerializeField]
    GameObject _shipBulletPrefab;

    [SerializeField]
    GameObject _asteroidBigPrefab;

    [SerializeField]
    GameObject _asteroidSmallPrefab;

    static PoolObject<GameObject> _shipBulletPool;
    static PoolObject<GameObject> _astroidBigPool;
    static PoolObject<GameObject> _astroidSmallPool;

    public static PoolObject<GameObject> ShipBulletPool { get => _shipBulletPool; set => _shipBulletPool = value; }
    public static PoolObject<GameObject> AstroidBigPool { get => _astroidBigPool; set => _astroidBigPool = value; }
    public static PoolObject<GameObject> AstroidSmallPool { get => _astroidSmallPool; set => _astroidSmallPool = value; }

    private void Start()
    {
        OnEnableEventListenerSubscriptions();
        _shipBulletPool = new PoolObject<GameObject>(10, _shipBulletPrefab, PoolSpawn, TurnOnObj, TurnOffObj);
        _astroidBigPool = new PoolObject<GameObject>(10, _asteroidBigPrefab, PoolSpawn, TurnOnObj, TurnOffObj);
        _astroidSmallPool = new PoolObject<GameObject>(10, _asteroidSmallPrefab, PoolSpawn, TurnOnObj, TurnOffObj);
    }

    void OnDisable()
    {
        CancelEventListenerSubscriptions();
    }

    public void TurnOffAllGameObjects(Hashtable data)
    {
        _shipBulletPool.DestroyAllObjects();
        _astroidBigPool.DestroyAllObjects();
        _astroidSmallPool.DestroyAllObjects();
    }

    public GameObject PoolSpawn(GameObject prefab)
    {
        var obj = Instantiate(prefab, this.transform);

        return obj;
    }

    public void TurnOnObj(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void TurnOffObj(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void OnEnableEventListenerSubscriptions()
    {
        EventManager.StartListening(GenericEvents.GameOver, TurnOffAllGameObjects);
    }

    public void CancelEventListenerSubscriptions()
    {
        EventManager.StopListening(GenericEvents.GameOver, TurnOffAllGameObjects);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField]
    GameObject _shipBulletPrefab;

    static PoolObject<GameObject> _shipBulletPool;

    public static PoolObject<GameObject> ShipBulletPool { get => _shipBulletPool; set => _shipBulletPool = value; }

    private void Start()
    {
        _shipBulletPool = new PoolObject<GameObject>(1, _shipBulletPrefab, PoolSpawn, TurnOnObj, TurnOffObj);
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


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PoolObject<T>
{
    //I use poolobject to spam the bullets and the asteroids

    T _prefab;
    List<T> pool = new List<T>();
    List<T> objInScene = new List<T>();
    Action<T> _startMethod;
    Action<T> _sleepMethod;
    Func<T, T> _factory;

    int _init;

    public PoolObject(int init, T prefab, Func<T, T> factory, Action<T> startMethod, Action<T> sleepMethod, bool initialize = true)
    {
        _prefab = prefab;
        _startMethod = startMethod;
        _factory = factory;
        _sleepMethod = sleepMethod;
        _init = init;

        if (initialize)
            Initialize();
    }

    void Initialize()
    {
        for (int i = 0; i < _init; i++)
        {
            T obj = _factory(_prefab);
            _sleepMethod(obj);
            pool.Add(obj);

        }
    }

    public T Spawn()
    {
        if (pool.Count != 0)
        {
            var obj = pool[0];
            pool.Remove(pool[0]);
            _startMethod(obj);
            objInScene.Add(obj);
            return obj;
        }
        else
        {
            T newObj = _factory(_prefab);
            _startMethod(newObj);
            objInScene.Add(newObj);
            return newObj;
        }
    }

    public void Destroy(T obj)
    {
        _sleepMethod(obj);
        objInScene.Remove(obj);
        pool.Add(obj);
    }

    public void DestroyAllObjects()
    {
        for (int i = 0; i < objInScene.Count; i++)
        {
            _sleepMethod(objInScene[i]);
            pool.Add(objInScene[i]);
        }

        objInScene.Clear();
    }

}

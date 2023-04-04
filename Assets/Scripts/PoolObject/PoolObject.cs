using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PoolObject<T>
{
    T _prefab;
    List<T> pool = new List<T>();
    Action<T> _startMethod;
    Action<T> _sleepMethod;
    Func<T> _factory;

    int _init;

    public PoolObject(int init, T prefab, Func<T> factory, Action<T> startMethod, Action<T> sleepMethod)
    {
        _prefab = prefab;
        _startMethod = startMethod;
        _factory = factory;
        _sleepMethod = sleepMethod;
        _init = init;

        Initialize();
    }

    void Initialize()
    {
        for (int i = 0; i < _init; i++)
        {
            T obj = _factory();
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
            return obj;
        }
        else
        {
            T newObj = _factory();
            pool.Add(newObj);
            _startMethod(newObj);
            return newObj;
        }
    }

    public void Destroy(T obj)
    {
        _sleepMethod(obj);
        pool.Add(obj);
    }

}

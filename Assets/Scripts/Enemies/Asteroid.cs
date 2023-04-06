using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Asteroid : MonoBehaviour, IUpdate, IEventListener
{
    Stats _stats;
    Vector3 _dir;
    Vector3 _aim;
    protected Collider2D _collider;
    public Stats Stats { get => _stats; set => _stats = value; }

    void OnEnable()
    {
        UpdateManager.Instance.updates.Add(this);
        OnEnableEventListenerSubscriptions();
        SetStats();
        SetDirection();
    }

    void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    void OnDisable()
    {
        UpdateManager.Instance.updates.Remove(this);
        CancelEventListenerSubscriptions();
    }

    private void SetDirection()
    {
        _aim = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), 0);
    }

    public void IUpdate()
    {
        Move();
    }

    private void Move()
    {
        _dir = transform.position + _aim;

        transform.position += (_dir - transform.position).normalized * _stats.speed * Time.deltaTime;

    }

    public abstract void SetStats();
    public abstract void OnDestroyCall();
    public virtual void OnDestroyAsteroid(Hashtable data)
    {
        Collider2D collider = (Collider2D)data[DataEventHashtableParams.Collider.ToString()];
        if (collider == _collider)
        {
            PoolManager.AstroidBigPool.Destroy(this.gameObject);
            OnDestroyCall();


        }
    }

    public void OnEnableEventListenerSubscriptions()
    {
        EventManager.StartListening(GenericEvents.DestroyAsteroid, OnDestroyAsteroid);
    }

    public void CancelEventListenerSubscriptions()
    {
        EventManager.StopListening(GenericEvents.DestroyAsteroid, OnDestroyAsteroid);
    }
}

public struct Stats
{
    public float speed;
    public int value;
}

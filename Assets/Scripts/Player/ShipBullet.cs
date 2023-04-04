using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBullet : MonoBehaviour, IUpdate
{
    Player _player;
    BulletStats _stats;

    float _life;


    private void Awake()
    {
        _player = FindObjectOfType<Player>();
    }

    private void OnEnable()
    {
        UpdateManager.Instance.updates.Add(this);

        transform.position = _player.transform.position;
        transform.rotation = _player.transform.rotation;

        _stats = new BulletStats()
        {
            direction = (_player.Model.BulletSpawnPoint - transform.position).normalized,
            speed = 10f,
            life = 2
        };

        _life = _stats.life;
    }

    void OnDisable()
    {
        UpdateManager.Instance.updates.Remove(this);
    }


    public void IUpdate()
    {
        transform.position += _stats.direction * _stats.speed * Time.deltaTime;

        _life -= Time.deltaTime;

        if (_life < 0)
            PoolManager.ShipBulletPool.Destroy(this.gameObject);
    }
}

public struct BulletStats
{
    public Vector3 direction;
    public float speed;
    public float life;
}

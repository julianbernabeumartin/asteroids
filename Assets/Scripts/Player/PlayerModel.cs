using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    public ModelStats stats;
    public bool shoot;

    float _timeToRotate = 0;
    float _rotation = 0;
    float _shootCoolDown = 0;
    Vector3 _bulletSpawnPoint;


    public float TimeToRotate { get => _timeToRotate; set => _timeToRotate = value; }
    public float Rotation { get => _rotation; set => _rotation = value; }
    public Vector3 BulletSpawnPoint { get => _bulletSpawnPoint; set => _bulletSpawnPoint = value; }
    public float ShootCoolDown { get => _shootCoolDown; set => _shootCoolDown = value; }
}

public struct ModelStats
{
    public float speed;
    public float maxSpeed;
    public float turnSpeed;
    public float rotationAngle;
    public float shootCooldown;
}

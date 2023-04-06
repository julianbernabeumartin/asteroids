using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    // in this class, we add all the stats and propertis that the player will have.

    public ModelStats stats;
    public bool shoot;
    public bool move;

    float _timeToRotate = 0;
    float _rotation = 0;
    float _shootCoolDown = 0;
    Vector3 _bulletSpawnPoint;

    int _currentScore;

    public float TimeToRotate { get => _timeToRotate; set => _timeToRotate = value; }
    public float Rotation { get => _rotation; set => _rotation = value; }
    public Vector3 BulletSpawnPoint { get => _bulletSpawnPoint; set => _bulletSpawnPoint = value; }
    public float ShootCoolDown { get => _shootCoolDown; set => _shootCoolDown = value; }
    public int CurrentScore
    {
        get => _currentScore;
        set
        {
            _currentScore = value;

            EventManager.TriggerEvent(GenericEvents.UpdateUIScore, new Hashtable()
            {
                { UIEventHastableParams.Score.ToString(), _currentScore }
            });
        }

    }

}

// i use struct to storage the model stats as data.(i thought of doing this with scriptableobjects, but i wanted to try this)
public struct ModelStats
{
    public float speed;
    public float maxSpeed;
    public float turnSpeed;
    public float rotationAngle;
    public float shootCooldown;
}

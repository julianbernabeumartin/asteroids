using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    public ModelStats stats;

    float _timeToRotate = 0;
    float _rotation = 0;

    public float TimeToRotate { get => _timeToRotate; set => _timeToRotate = value; }
    public float Rotation { get => _rotation; set => _rotation = value; }
}

public struct ModelStats
{
    public float speed;
    public float maxSpeed;
    public float turnSpeed;
    public float rotationAngle;
}

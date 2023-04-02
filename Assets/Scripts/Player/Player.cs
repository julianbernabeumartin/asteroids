using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IUpdate
{
    PlayerModel _model = new PlayerModel();
    PlayerController _controller;

    Rigidbody2D _rb;

    public Rigidbody2D Rb { get => _rb; set => _rb = value; }



    void OnEnable()
    {
        UpdateManager.Instance.updates.Add(this);
    }

    void OnDisable()
    {
        UpdateManager.Instance.updates.Remove(this);
    }

    void OnDestroy()
    {
        UpdateManager.Instance.updates.Remove(this);
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _controller = new PlayerController(_model);

        _model.stats = new ModelStats
        {
            speed = 0.1f,
            turnSpeed = 100f,
            rotationAngle = 1f,
            maxSpeed = 2f,
        };
    }
    public void IUpdate()
    {

        if (Input.GetKey(KeyCode.W))
        {
            _controller.AddForce(this);
        }

        if (Input.GetKey(KeyCode.D))
            _controller.RotateShip(this, -1);

        if (Input.GetKey(KeyCode.A))
            _controller.RotateShip(this, 1);



        if (Input.GetKeyUp(KeyCode.D))
        {
            _controller.ResetRotateTimer();
        }


        Debug.Log(Rb.velocity);

    }
}

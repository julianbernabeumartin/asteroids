using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IUpdate
{
    PlayerModel _model = new PlayerModel();
    PlayerController _controller;
    Rigidbody2D _rb;

    public Rigidbody2D Rb { get => _rb; set => _rb = value; }
    public PlayerModel Model { get => _model; set => _model = value; }

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
        UpdateManager.Instance.updates.Add(this);
        _controller = new PlayerController(_model);

        _model.stats = new ModelStats
        {
            speed = 0.2f,
            turnSpeed = 100f,
            rotationAngle = 1f,
            maxSpeed = 2.5f,
            shootCooldown = 0.3f
        };

    }
    public void IUpdate()
    {
        _model.BulletSpawnPoint = transform.position + transform.up;

        _controller.CoolDown();
        _controller.Shoot();

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

        if (Input.GetKey(KeyCode.Space))
        {
            _controller.ToggleShooting(true);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            _controller.ToggleShooting(false);
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(_model.BulletSpawnPoint, Vector3.one / 2);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EventManager.TriggerEvent(GenericEvents.GameOver, new Hashtable()
            { });

            Destroy(this.gameObject);
        }
    }
}

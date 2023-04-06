using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IUpdate, IFixedUpdate
{

    #region VARIABLES
    //Include the variables of model and controller of the player  aswell as the rigidbody and the inputmanager
    PlayerModel _model = new PlayerModel();
    PlayerController _controller;
    Rigidbody2D _rb;
    InputManager _inputManager;
    #endregion

    #region PROPERTIES

    public Rigidbody2D Rb { get => _rb; set => _rb = value; }
    public PlayerModel Model { get => _model; set => _model = value; }
    public PlayerController Controller { get => _controller; set => _controller = value; }
    #endregion

    #region MONOBEHAVIOUR METHODS
    void OnDisable()
    {
        UpdateManager.Instance.updates.Remove(this);
        UpdateManager.Instance.fixedUpdates.Remove(this);

    }

    void OnDestroy()
    {
        UpdateManager.Instance.updates.Remove(this);
        UpdateManager.Instance.fixedUpdates.Remove(this);
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _inputManager = FindObjectOfType<InputManager>();
    }

    private void Start()
    {
        UpdateManager.Instance.updates.Add(this);
        UpdateManager.Instance.fixedUpdates.Add(this);

        //Crea an instace of player controller and we pass playermodel as a parameter

        _controller = new PlayerController(_model);

        //we add the playermodel to input manager using a builder

        _inputManager.AddPlayer(this);


        //we declare the values of the model's stats
        _model.stats = new ModelStats
        {
            speed = 1f,
            turnSpeed = 100f,
            rotationAngle = 1f,
            maxSpeed = 2.5f,
            shootCooldown = 0.3f
        };

    }

    //some gizmos i've used to check some mechanics
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(_model.BulletSpawnPoint, Vector3.one / 2);
    }


    //i use trigger collider to check if the player hit a asteroid. If it does, we call an event
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EventManager.TriggerEvent(GenericEvents.GameOver, new Hashtable()
            { });

            Destroy(this.gameObject);
        }
    }

    #endregion

    #region INTERFACE METHODS
    public void IUpdate()
    {
        // we call the controller methods, that have to be called per frame, to work

        _model.BulletSpawnPoint = transform.position + transform.up;

        _controller.CoolDown();
        _controller.Shoot();
    }

    public void IFixedUpdate()
    {
        // same case as IUpdate

        _controller.AddForce(this);
    }
    #endregion









}

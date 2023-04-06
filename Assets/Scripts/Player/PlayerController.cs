using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    //we include the variable of model so we can acces its properties and variables
    PlayerModel _model;

    //constructor to reference playermodel
    public PlayerController(PlayerModel model)
    {
        this._model = model;
    }

    //mechanic movement of the ship
    public void AddForce(Player player)
    {
        if (_model.move)
        {
            player.Rb.AddForce(player.transform.up * _model.stats.speed);
            player.Rb.velocity = new Vector2(Mathf.Clamp(player.Rb.velocity.x, -player.Model.stats.maxSpeed, player.Model.stats.maxSpeed),
            Mathf.Clamp(player.Rb.velocity.y, -player.Model.stats.maxSpeed, player.Model.stats.maxSpeed));
        }

    }

    //mechanic rotation of the ship
    public void RotateShip(Player player, int dir)
    {
        _model.TimeToRotate += _model.stats.turnSpeed * Time.deltaTime;

        if (_model.TimeToRotate > 1)
        {

            player.transform.Rotate(new Vector3(0, 0, _model.stats.rotationAngle * dir));

            _model.TimeToRotate = 0;
        }

    }

    //to reset the timer when i release the rotation buttons
    public void ResetRotateTimer()
    {
        _model.TimeToRotate = 0;
    }

    //to let my ship know that it can shoot
    public void ToggleShooting(bool set)
    {
        _model.shoot = set;
    }
    // same as toggleshooting() but for movement
    public void ToggleMove(bool set)
    {
        _model.move = set;
    }

    //shooting mechanic if cooldwon is 0 and we gave the ship the order to move, it will move
    public void Shoot()
    {
        if (_model.ShootCoolDown <= 0 && _model.shoot)
        {
            var obj = PoolManager.ShipBulletPool.Spawn();
            _model.ShootCoolDown = _model.stats.shootCooldown;
        }

    }

    //shooting cooldown so we can control the bullet spam.
    public void CoolDown()
    {
        if (_model.ShootCoolDown > 0)
            _model.ShootCoolDown -= Time.deltaTime;
    }
}

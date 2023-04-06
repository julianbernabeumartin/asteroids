using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    PlayerModel _model;

    public PlayerController(PlayerModel model)
    {
        this._model = model;
    }

    public void AddForce(Player player)
    {
        player.Rb.AddForce(player.transform.up * _model.stats.speed);

        if (player.Rb.velocity.x > _model.stats.maxSpeed)
        {
            player.Rb.velocity = new Vector2(_model.stats.maxSpeed, player.Rb.velocity.y);
        }

        if (player.Rb.velocity.x < -_model.stats.maxSpeed)
        {
            player.Rb.velocity = new Vector2(-_model.stats.maxSpeed, player.Rb.velocity.y);
        }

        if (player.Rb.velocity.y > _model.stats.maxSpeed)
        {
            player.Rb.velocity = new Vector2(player.Rb.velocity.x, _model.stats.maxSpeed);
        }

        if (player.Rb.velocity.y < -_model.stats.maxSpeed)
        {
            player.Rb.velocity = new Vector2(player.Rb.velocity.x, -_model.stats.maxSpeed);
        }

    }

    public void RotateShip(Player player, int dir)
    {
        _model.TimeToRotate += _model.stats.turnSpeed * Time.deltaTime;

        if (_model.TimeToRotate > 1)
        {

            player.transform.Rotate(new Vector3(0, 0, _model.stats.rotationAngle * dir));

            _model.TimeToRotate = 0;
        }

    }

    public void ResetRotateTimer()
    {
        _model.TimeToRotate = 0;
    }

    public void ToggleShooting(bool set)
    {
        _model.shoot = set;
    }

    public void Shoot()
    {
        if (_model.ShootCoolDown <= 0 && _model.shoot)
        {
            var obj = PoolManager.ShipBulletPool.Spawn();
            _model.ShootCoolDown = _model.stats.shootCooldown;
        }

    }

    public void CoolDown()
    {
        if (_model.ShootCoolDown > 0)
            _model.ShootCoolDown -= Time.deltaTime;
    }
}

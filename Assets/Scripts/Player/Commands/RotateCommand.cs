using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCommand : ICommand
{
    Player _player;

    int _dir = 0;
    public RotateCommand(Player player, int dir)
    {
        _player = player;

        _dir = dir;
    }

    public void Execute(bool isDown, bool isUp, bool isPressed)
    {
        if (_player == null) return;

        if (isPressed)
        {
            _player.Controller.RotateShip(_player, _dir);
        }

        if(isUp)
        {
            _player.Controller.ResetRotateTimer();
        }
    }
}

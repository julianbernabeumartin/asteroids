using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCommand : ICommand
{
    Player _player;

    public ShootCommand(Player player)
    {
        _player = player;
    }

    public void Execute(bool isDown, bool isUp, bool isPressed)
    {
        if (_player == null) return;

        if (isPressed)
        {
            _player.Controller.ToggleShooting(true);
        }

        if (isUp)
        {
            _player.Controller.ToggleShooting(false);
        }
    }
}

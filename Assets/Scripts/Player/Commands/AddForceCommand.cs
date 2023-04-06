using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceCommand : ICommand
{
    Player _player;

    public AddForceCommand(Player player)
    {
        _player = player;
    }

    public void Execute(bool isDown, bool isUp, bool isPressed)
    {
        if (_player == null) return;

        if (isPressed)
        {
            _player.Controller.ToggleMove(true);

        }

        if (isUp)
        {
            _player.Controller.ToggleMove(false);
        }
    }
}

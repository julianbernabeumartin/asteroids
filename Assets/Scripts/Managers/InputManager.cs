using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    Player _player;

    ICommand moveFowardCommand;

    public InputManager(Player player)
    {
        _player = player;
    }

}

public class UserInput
{
    public KeyCode KeyCode;
    public bool IsDown;
    public bool IsUp;
    public bool IsPressed;
}

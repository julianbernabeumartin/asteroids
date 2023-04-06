using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGameCommand : ICommand
{
    public void Execute(bool isDown, bool isUp, bool isPressed)
    {
        if (isUp)
        {
            Application.Quit();
        }
    }
}

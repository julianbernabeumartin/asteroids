using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    //Interface to execute the commands. The booleans are used to check if the user is pressing the button once, holding it or releasing it
    void Execute(bool isDown, bool isUp, bool isPressed);

}

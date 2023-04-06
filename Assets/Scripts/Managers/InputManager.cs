using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour, IUpdate
{
    //In this class, i add the inputs the player will use in game and call the methods assigned to those inputs

    #region VARIABLES
    Player _player;

    [SerializeField]
    List<UserInput> _userInputs = new List<UserInput>();

    #endregion


    //BUILDER
    public InputManager AddPlayer(Player player)
    {
        _player = player;
        AddInputsDefault();
        return this;
    }

    #region MONOBEHAVIOUR METHODS
    void Start()
    {
        UpdateManager.Instance.updates.Add(this);
    }

    void OnDisable()
    {
        UpdateManager.Instance.updates.Remove(this);
    }

    #endregion

    #region CLASS METHODS


    public void AddInputsDefault()
    {
        _userInputs.Add(new UserInput(KeyCode.W, new PlayerAction("Move Foward", new AddForceCommand(_player))));
        _userInputs.Add(new UserInput(KeyCode.D, new PlayerAction("Rotate Right", new RotateCommand(_player, -1))));
        _userInputs.Add(new UserInput(KeyCode.A, new PlayerAction("Rotate Left", new RotateCommand(_player, +1))));
        _userInputs.Add(new UserInput(KeyCode.Space, new PlayerAction("Shoot", new ShootCommand(_player))));
        _userInputs.Add(new UserInput(KeyCode.Escape, new PlayerAction("Exit Game", new ExitGameCommand())));


        for (int i = 0; i < _userInputs.Count; i++)
        {
            UIManager.Instance.EscapeText = _userInputs[i];

        }
    }

    #endregion

    public void IUpdate()
    {
        for (int i = 0; i < _userInputs.Count; i++)
        {
            _userInputs[i].IsDown = Input.GetKeyDown(_userInputs[i].KeyCode) ? true : false;
            _userInputs[i].IsUp = Input.GetKeyUp(_userInputs[i].KeyCode) ? true : false;
            _userInputs[i].IsPressed = Input.GetKey(_userInputs[i].KeyCode) ? true : false;

            if (_userInputs[i].IsDown || _userInputs[i].IsUp || _userInputs[i].IsPressed)
            {
                _userInputs[i].PlayerAction.Command.Execute(_userInputs[i].IsDown, _userInputs[i].IsUp, _userInputs[i].IsPressed);

            }

        }
    }

}

[System.Serializable]
public class UserInput
{
    public KeyCode KeyCode;
    public bool IsDown;
    public bool IsUp;
    public bool IsPressed;
    public PlayerAction PlayerAction;

    public UserInput(KeyCode keyCode, PlayerAction playerAction)
    {
        KeyCode = keyCode;
        PlayerAction = playerAction;
    }
}


[System.Serializable]
public class PlayerAction
{
    public string Name;
    public ICommand Command;

    public PlayerAction(string name, ICommand command)
    {
        Name = name;
        Command = command;
    }
}

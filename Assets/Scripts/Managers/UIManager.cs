using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject _level;
    public void StartGame()
    {
        var obj = Instantiate(_level);
        Destroy(this.gameObject);
    }
}

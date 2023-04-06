using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour, IEventListener
{
    [SerializeField]
    TMP_Text _score;
    [SerializeField]
    TMP_Text _escapeText;

    public static UIManager Instance { get; private set; }
    public UserInput EscapeText
    {
        set
        {
            _escapeText.text += $"Press {value.KeyCode} to {value.PlayerAction.Name} \n";
        }
    }
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        OnEnableEventListenerSubscriptions();
    }

    void OnDestroy()
    {
        CancelEventListenerSubscriptions();
    }

    public void UpdateScore(Hashtable data)
    {
        int num = (int)data[UIEventHastableParams.Score.ToString()];
        _score.text = $"Score: {num}";
    }

    public void CancelEventListenerSubscriptions()
    {
        EventManager.StopListening(GenericEvents.UpdateUIScore, UpdateScore);
    }

    public void OnEnableEventListenerSubscriptions()
    {
        EventManager.StartListening(GenericEvents.UpdateUIScore, UpdateScore);
    }
}

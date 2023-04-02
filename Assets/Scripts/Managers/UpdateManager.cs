using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    public List<IUpdate> updates = new List<IUpdate>();
    public List<IFixedUpdate> fixedUpdates = new List<IFixedUpdate>();
    public List<ILateUpdate> lateUpdates = new List<ILateUpdate>();

    public static UpdateManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (updates.Count != 0)
            for (int i = 0; i < updates.Count; i++)
            {
                updates[i].IUpdate();
            }
    }

    private void FixedUpdate()
    {
        if (fixedUpdates.Count != 0)
            for (int i = 0; i < fixedUpdates.Count; i++)
            {
                fixedUpdates[i].IFixedUpdate();
            }
    }

    private void LateUpdate()
    {
        if (lateUpdates.Count != 0)
            for (int i = 0; i < lateUpdates.Count; i++)
            {
                lateUpdates[i].ILateUpdate();
            }
    }
}

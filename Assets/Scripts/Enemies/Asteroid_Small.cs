using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid_Small : Asteroid
{
    public override void OnDestroyCall()
    {

    }

    public override void SetStats()
    {
        Stats = new Stats()
        {
            speed = 2f,
            value = 2
        };
    }
}

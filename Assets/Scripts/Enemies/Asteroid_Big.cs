using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid_Big : Asteroid
{
    int numOfSmallAsteroids = 2;


    public override void SetStats()
    {
        Stats = new Stats()
        {
            speed = 1f,
            value = 1
        };
    }
    public override void OnDestroyCall()
    {
        for (int i = 0; i < numOfSmallAsteroids; i++)
        {
            var obj = PoolManager.AstroidSmallPool.Spawn();
            obj.transform.position = transform.position;
        }

        EventManager.TriggerEvent(GenericEvents.UpdateAsteroidsDestroyed, new Hashtable()
        {{DataEventHashtableParams.Collider.ToString(),_collider}
        });

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    private void Awake()
    {
        //Need to change this at some point
        Init();
    }

    public override void Died()
    {
        base.Died();
        SimplePool.Despawn(gameObject);
    }

}

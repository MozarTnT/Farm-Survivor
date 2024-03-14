using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenZombie : Monster
{

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        data.Speed = 2.0f;
        data.Power = 1;
        data.HP = 100;

        base.Init();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteZombie : Monster
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
        data.AttDelay = 1.0f;

        base.Init();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdZombie : Monster
{
    
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        data.Speed = 3.0f;
        data.Power = 1;
        data.HP = 80;
        data.AttDelay = 1.0f;

        base.Init();
    }

}

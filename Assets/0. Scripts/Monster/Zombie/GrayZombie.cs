using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrayZombie : Monster
{
    
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        data.Speed = 2.5f;
        data.Power = 1;
        data.HP = 120;
        data.AttDelay = 1.0f;

        base.Init();
    }

}

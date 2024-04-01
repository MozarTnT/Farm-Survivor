using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingSlime : Boss
{
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        data.Speed = 2.5f;
        data.Power = 10;
        data.HP = 200.0f;
        data.AttDelay = 0.5f;
        data.Defence = 0.63f;


        base.Init();
    }
}

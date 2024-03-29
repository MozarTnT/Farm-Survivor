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
        data.Speed = 1.5f;
        data.Power = 10;
        data.HP = 1000;
        data.AttDelay = 0.5f;
        data.Defence = 0.3f;


        base.Init();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinZombie : Monster
{
    void Start()
    {
        transform.SetParent(GameManager.instance.P.zombie_Parent);
    }
    private void OnEnable()
    {
        Init();
    }

    public override void Init()
    {
        data.Speed = 3.5f;
        data.Power = 5;
        data.HP = 150 + GameManager.instance.UI.topUI.Level;
        data.AttDelay = 1.3f;
        data.Defence = 0.5f + (GameManager.instance.UI.topUI.Level / 100.0f);


        base.Init();
    }
}

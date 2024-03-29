using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightGreenZombie : Monster
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
        data.Speed = 3.0f;
        data.Power = 2;
        data.HP = 120 + GameManager.instance.UI.topUI.Level;
        data.AttDelay = 1.2f;
        data.Defence = 0.1f + (GameManager.instance.UI.topUI.Level / 100.0f);

        //Debug.Log(0.1f + (GameManager.instance.UI.topUI.Level / 100.0f));

        base.Init();
    }
}

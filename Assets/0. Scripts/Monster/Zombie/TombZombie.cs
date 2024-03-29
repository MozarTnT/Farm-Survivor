using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TombZombie : Monster
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
        data.Speed = 4.0f;
        data.Power = 7;
        data.HP = 180 + GameManager.instance.UI.topUI.Level;
        data.AttDelay = 1.3f;
        data.Defence = 0.7f + (GameManager.instance.UI.topUI.Level / 100.0f);

        //Debug.Log(0.1f + (GameManager.instance.UI.topUI.Level / 100.0f));

        base.Init();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteZombie : Monster
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
        data.Power = 3;
        data.HP = 150 + GameManager.instance.UI.topUI.Level;
        data.AttDelay = 0.8f;
        data.Defence = 0.3f + (GameManager.instance.UI.topUI.Level / 100.0f);

        //Debug.Log(0.1f + (GameManager.instance.UI.topUI.Level / 100.0f));

        base.Init();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenZombie : Monster
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
        data.Speed = 2.0f;
        data.Power = 1;
        data.HP = 100 + GameManager.instance.UI.topUI.Level;
        data.AttDelay = 1.0f;
        data.Defence = 0.1f + (GameManager.instance.UI.topUI.Level / 100.0f);

        Debug.Log(0.1f + (GameManager.instance.UI.topUI.Level / 100.0f));
        
        
        base.Init();
    }
}

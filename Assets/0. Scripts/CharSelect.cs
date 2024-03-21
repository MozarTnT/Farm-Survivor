using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSelect : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void CharSel(int index)
    {
        GameManager.instance.charSelectIndex = index;
        GameManager.instance.OnGame();
    }
}

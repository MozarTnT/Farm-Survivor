using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Image expImg;
    [SerializeField] private Text killTxt;
    [SerializeField] private Text timeTxt;
    [SerializeField] private Text lvTxt;

    private int killCount;
    public int KillCount 
    {
        get { return killCount; }
        set
        {
            killCount = value;
            killTxt.text = $" : {killCount}";
        }
    }
    private int level;
    public int Level
    {
        get { return level; }
        set
        {
            level = value;
            lvTxt.text = $"Lv.{level}";
        }
    }

    private float maxExp;
    private float exp;
    public float Exp 
    {
        get { return exp; }
        set
        {
            exp = value;
            if (exp >= maxExp)
            {
                exp = 0;
                maxExp += 50;
                Level++;
            }
            float val = exp / maxExp * 1270;
            expImg.rectTransform.sizeDelta = new Vector2(val + 10, 44); 
  
        }
    }


    private float sec;
    private int min;

    void Start()
    {
        maxExp = 50;
        Level = 1;
        KillCount = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            
            Exp += 10;
        }


        sec += Time.deltaTime;
        if (sec >= 60.0f)
        {
            min += 1;
            sec = 0f;
        }

        timeTxt.text = string.Format("{0:D2}:{1:D2}", min, (int)sec); // 2자리까지만 표현

    }
}

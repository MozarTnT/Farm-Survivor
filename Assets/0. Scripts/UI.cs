using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using static UnityEditor.Progress;

public class UI : MonoBehaviour
{
    [System.Serializable]
    public class Top
    {
        public Image expImg;
        public Text killTxt;
        public Text timeTxt;
        public Text lvTxt;

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

        [HideInInspector]
        public float maxExp;


        private float exp = 0;
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
                    GameManager.instance.UI.OnShowLevelUpPopUp(true);
                }
                float val = exp / maxExp * 1270;
                expImg.rectTransform.sizeDelta = new Vector2(val + 10, 44);

            }
        }

    }

    public Top topUI;


    [System.Serializable]
    public class LevelUp
    {
        public Image icon;
        public Text level;
        public Text title;
        public Text desc;
    }

    [SerializeField] private List<LevelUp> levelupUIs;
    [SerializeField] private GameObject levelupPopup;

    [System.Serializable]
    public class Result
    {
        public Image title;

        [HideInInspector]
        public float[] deadTitleValue = new float[5] {0.21f, 0.4f, 0.59f, 0.78f, 1f};
    }

    [SerializeField] private Result result;

    private float sec;
    private int min;

    void Start()
    {
        topUI.maxExp = 50;
        topUI.Level = 1;
        topUI.KillCount = 0;

      
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            //topUI.Exp += 10;
            DeadTitleStart();
        }


        sec += Time.deltaTime;
        if (sec >= 60.0f)
        {
            min += 1;
            sec = 0f;
        }

        topUI.timeTxt.text = string.Format("{0:D2}:{1:D2}", min, (int)sec); // 2자리까지만 표현

    }


    public void OnShowLevelUpPopUp(bool isShow)
    {
        GameManager.instance.state = GameState.Stop;
        levelupPopup.SetActive(isShow);

        if(isShow == true)
        {
            Transform bg = levelupPopup.transform.GetChild(0).GetChild(0);
            bg.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            bg.DOScale(Vector3.one, 0.2f)
                .SetEase(Ease.InQuint);
        }
        else
        {
            GameManager.instance.state = GameState.Play;
        }

    }

    public void OnItemSelect(int index)
    {

    }

    public void DeadTitleStart()
    {
        GameManager.instance.state = GameState.Stop;
        result.title.fillAmount = 0;
        StartCoroutine(CDeadTitle());
    }

    IEnumerator CDeadTitle()
    {
        foreach (var item in result.deadTitleValue)
        {
            result.title.fillAmount = item;
            yield return new WaitForSeconds(0.2f);
        }
    }
}

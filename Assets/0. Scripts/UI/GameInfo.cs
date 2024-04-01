using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInfo : MonoBehaviour
{

     // 테스트용 
    public Button HTPBtn; //물음표

    public GameObject HTP_Pannel; // 게임방법 판넬
    public Button[] HTPBtns; // 게임방법 버튼들
    public GameObject[] HTP_Pannels; // 게임방법 설명판넬 오브젝트
    public Button ExitBtn;   //나가기
    
   
    void Start()
    {
        if (HTPBtn != null)
            HTPBtn.onClick.AddListener(() =>
            {
                HTP_Pannel.SetActive(true);
            });
        HTPBtns[0].onClick.AddListener(() => { HowToPlayPannelOn(0); });
        HTPBtns[1].onClick.AddListener(() => { HowToPlayPannelOn(0); });
        HTPBtns[2].onClick.AddListener(() => { HowToPlayPannelOn(0); });
        HTPBtns[3].onClick.AddListener(() => { HowToPlayPannelOn(0); });
        HTPBtns[4].onClick.AddListener(() => { HowToPlayPannelOn(0); });

        if (ExitBtn != null)
            ExitBtn.onClick.AddListener(() =>
            {
                HTP_Pannel.SetActive(false);
            });
    }

    
    void Update()
    {
        
    }

    void HowToPlayPannelOn(int index)
    {
        HTP_Pannels[index].gameObject.SetActive(true);
        for (int i = 0; i < HTP_Pannels.Length; i++)
        {
            if (index != i)
                HTP_Pannels[i].gameObject.SetActive(false);
        }
    }
}

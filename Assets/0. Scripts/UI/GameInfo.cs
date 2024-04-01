using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInfo : MonoBehaviour
{

     // �׽�Ʈ�� 
    public Button HTPBtn; //����ǥ

    public GameObject HTP_Pannel; // ���ӹ�� �ǳ�
    public Button[] HTPBtns; // ���ӹ�� ��ư��
    public GameObject[] HTP_Pannels; // ���ӹ�� �����ǳ� ������Ʈ
    public Button ExitBtn;   //������
    
   
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

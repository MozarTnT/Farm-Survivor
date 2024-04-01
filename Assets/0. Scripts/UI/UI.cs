using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;
using static UI;


public class UI : MonoBehaviour
{
    [System.Serializable]
    public class Top
    {
        public Image expImg;
        public Text killTxt;
        public Text timeTxt;
        public Text lvTxt;
        public Text expTxt;

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
                float val = exp / maxExp * 640;
                expImg.rectTransform.sizeDelta = new Vector2(val + 10, 44);
                expTxt.text = $"{(exp/maxExp * 100f).ToString("F2")} %"; // 소수점 2자리 버리기

            }
        }

    }

    public Top topUI;

    [System.Serializable]
    public class ItemLevelUI
    {
        public GameObject gunObject;
        public GameObject tridentObject;
        public GameObject bibleObject;
        public GameObject magnetObject;
        public GameObject bootsObject;

        public Text gunLevel;
        public Text tridentLevel;
        public Text bibleLevel;
        public Text magnetLevel;
        public Text bootsLevel;
    }

    public ItemLevelUI itemLevelUI;


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
    [SerializeField] private Text killCountText;

    [SerializeField] private GameObject booster;
    [SerializeField] private Image boosterImage;

    [SerializeField] public UIAnimation uiAnimation;
    [SerializeField] public GameObject bossGameObject;
    // 아이템 데이터
    [SerializeField] private ItemData[] itemDatas;

    [SerializeField] public GameObject bossHP;
    [SerializeField] private Image bossHPImg;
    [SerializeField] private Text bossHPTxt;


    List<ItemData> levelUpItemData = new List<ItemData>();


    [System.Serializable]
    public class Result
    {
        public GameObject obj;
        public GameObject backObj;
        public Image title;

        [HideInInspector]
        public float[] deadTitleValue = new float[7] {0.21f, 0.4f, 0.59f, 0.78f, 0.85f, 0.92f, 1f};
    }



    [SerializeField] private Result result;

    private float sec;
    private int min;

    private int killText;

    public class ItemCount
    {
        public int bulletdmgCnt { get; set; }
        public int bulletspdCnt { get; set; }
        public int bootsCnt { get; set; }
        public int magnetCnt { get; set; }
        public int bibleCnt { get; set; }
        public int tridentCnt { get; set; }
    }


    public ItemCount itemCount = new ItemCount();

    public Button HTPBtn; //물음표

    public GameObject HTP_Pannel; // 게임방법 판넬
    public Button[] HTPBtns; // 게임방법 버튼들
    public GameObject[] HTP_Pannels; // 게임방법 설명판넬 오브젝트
    public Button ExitBtn;   //나가기
    [SerializeField] private GameObject gameEx;
    
    void Start()
    {
        topUI.maxExp = 100;
        topUI.Level = 1;
        topUI.KillCount = 0;
        topUI.Exp = 0;


        if (GameManager.instance.P.isBooster == true)
        {
            booster.SetActive(true);
        }

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
        SetKillCount();
        SetTimer();
        itemLevelSetUP();
        BossHP();

        if (GameManager.instance.P.isBooster == true)
        {
            ShowBooster();
        }

    }

    private void BossHP()
    {
        float slimeHP = 0f;
        GameManager.instance.P.FindKingSlime(slimeHP);

        bossHPImg.fillAmount = GameManager.instance.P.FindKingSlime(slimeHP) / 2000.0f;

        bossHPTxt.text = $"{(bossHPImg.fillAmount * 100.0f).ToString("F2")}%";
    }

    private void itemLevelSetUP()
    {
        itemLevelUI.gunLevel.text = $"Lv.{itemCount.bulletdmgCnt}";
        itemLevelUI.tridentLevel.text = $"Lv.{itemCount.tridentCnt}";
        itemLevelUI.bibleLevel.text = $"Lv.{itemCount.bibleCnt}";
        itemLevelUI.magnetLevel.text = $"Lv.{itemCount.magnetCnt}";
        itemLevelUI.bootsLevel.text = $"Lv.{itemCount.bootsCnt}";
    }

    public void ShowBooster()
    {
        boosterImage.fillAmount = GameManager.instance.P.Stamina;
    }

    public void SetKillCount()
    {
        killText = GameManager.instance.killCount;
        killCountText.text = $": {killText}";
    }

    public void SetTimer()
    {
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

        if (isShow == true)
        {
            ItemShuffle();

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

    public void ItemShuffle()
    {
        levelUpItemData.Clear();
        List <ItemData> itemData = itemDatas.ToList();

        for (int i = 0; i < levelupUIs.Count; i++)
        {
            int rand = Random.Range(0, itemData.Count);
            levelUpItemData.Add(itemData[rand]);
            itemData.RemoveAt(rand);
        }

        // UI 표현
        for (int i = 0; i < levelupUIs.Count; i++)
        {
            LevelUp ui = levelupUIs[i];
            ItemData data = levelUpItemData[i];

            ui.icon.sprite = data.Icon;
            ui.title.text = data.Title;
            ui.desc.text = data.Desc;

        }

    }

    public void OnItemSelect(int index)
    {
        ItemData data = levelUpItemData[index];
        Debug.Log(data.Title);

        switch(data.Type)
        {
            case ItemType.Bullet_Att:
                Debug.Log(GameManager.instance.P.data.Power);
                CaseBulletAttSetUp();
                Debug.Log(GameManager.instance.P.data.Power);
                break;
            case ItemType.Bullet_Spd:
                Debug.Log(GameManager.instance.B.Speed);
                CaseBulletSpdSetUp();
                Debug.Log(GameManager.instance.B.Speed);
                break;
            case ItemType.Bible:
                CaseBibleSetUp();
                break;
            case ItemType.Boots:
                Debug.Log($"Old Speed : {GameManager.instance.P.data.Speed}");
                CaseBootsSetUp();
                Debug.Log($"New Speed : {GameManager.instance.P.data.Speed}");
                break;
            case ItemType.Heal:
                GameManager.instance.P.data.HP = 100; // 풀피 회복
                float sizeX = ((float)GameManager.instance.P.data.HP / (float)GameManager.instance.P.data.MaxHP) * 120.0f;
                GameManager.instance.P.hpRect.sizeDelta = new Vector2(sizeX, 30.0f);
                break;
            case ItemType.Magnet:
                Debug.Log("자석 획득");
                CaseMagnetSetUp();
                break;
            case ItemType.Trident:
                Debug.Log("삼지창 획득");
                CaseTridentSetUp();
              
                break;

        }
    }

    private void CaseBulletAttSetUp()
    {
        if (itemLevelUI.gunObject != null && !itemLevelUI.gunObject.activeSelf)
        {
            itemLevelUI.gunObject.SetActive(true);
        }

        itemCount.bulletdmgCnt++;
        float r = Random.Range(0.2f, 0.3f); // 20~30 %
        GameManager.instance.P.data.Power += (GameManager.instance.P.weaponValue.c_BulletPower + (GameManager.instance.P.data.Power * r));
        Debug.Log($"총알 데미지 강화 : {itemCount.bulletdmgCnt}");

        // 랜덤값 20~30% + 무기상수
    }
    private void CaseBulletSpdSetUp()
    {
        itemCount.bulletspdCnt++;
        float r = Random.Range(0.08f, 0.12f); // 8 ~ 12%
        GameManager.instance.B.Speed += (GameManager.instance.P.weaponValue.c_BulletSpd + (GameManager.instance.B.Speed * r));
        Debug.Log($"총알 연사 속도 강화 : {itemCount.bulletspdCnt}");

        // 랜덤값 8~12% + 무기상수
    }
    private void CaseBibleSetUp()
    {
        if (itemLevelUI.bibleObject != null && !itemLevelUI.bibleObject.activeSelf)
        {
            itemLevelUI.bibleObject.SetActive(true);
        }

        itemCount.bibleCnt++;
        if (itemCount.bibleCnt < 4) // 갯수 4 아래일때는 도끼 추가
        {
            
            GameManager.instance.P.BibleAdd();
            Debug.Log($"낫 강화 : {itemCount.bibleCnt}");

        }
        else if (itemCount.bibleCnt >= 4)// 갯수 4 이상일때는 도끼 추가
        {
            float r = Random.Range(0.08f, 0.12f);
            GameManager.instance.P.data.BiblePower += (GameManager.instance.P.weaponValue.c_Bible + (GameManager.instance.P.data.BiblePower * r));
        }
        else
        {

        }
    }
    private void CaseBootsSetUp()
    {
        if (itemLevelUI.bootsObject != null && !itemLevelUI.bootsObject.activeSelf)
        {
            itemLevelUI.bootsObject.SetActive(true);
        }

        itemCount.bootsCnt++;

        if (itemCount.bootsCnt < 4)
        {
            float r = Random.Range(0.1f, 0.15f);
            GameManager.instance.P.data.Speed += (GameManager.instance.P.weaponValue.c_Boots + (GameManager.instance.P.data.Speed * r));
            Debug.Log($"신발 강화 : {itemCount.bootsCnt}");
        }
        else if (itemCount.bootsCnt >= 4)
        {
            // 3개가 넘어갈 경우 처리 추가
        }
        else
        {

        }

    }
    private void CaseMagnetSetUp()
    {
        if (itemLevelUI.magnetObject != null && !itemLevelUI.magnetObject.activeSelf)
        {
            itemLevelUI.magnetObject.SetActive(true);
        }

        itemCount.magnetCnt++;

        if (itemCount.magnetCnt < 3) // 테스트 필요
        {
            
            GameManager.instance.P.data.itemDistanceLimit += GameManager.instance.P.weaponValue.c_Magnet; // 기본 1.5f 최대 3.7f 목표 3번까지만 나오기

            Vector2 vector2 = GameManager.instance.P.magnetScale.localScale;
            vector2.x = (float)GameManager.instance.P.data.itemDistanceLimit;
            vector2.y = (float)GameManager.instance.P.data.itemDistanceLimit;
            GameManager.instance.P.magnetScale.localScale = vector2;

            GameManager.instance.P.magnet.SetActive(true);
            Debug.Log($"자석 강화 : {itemCount.magnetCnt}");
        }
        else
        {
            // 3개 넘어갈 경우 처리 추가
        }
    }
    private void CaseTridentSetUp()
    {
        if (itemLevelUI.tridentObject != null && !itemLevelUI.tridentObject.activeSelf)
        {
            itemLevelUI.tridentObject.SetActive(true);
        }

        itemCount.tridentCnt++;

        if(itemCount.tridentCnt <= 1)
        {
            Player player = GameManager.instance.P;
            player.isTridentOn = true;
            player.InvokeRepeating("AddTrident", 0, 2.5f);
        }

        else if (itemCount.tridentCnt > 1 && itemCount.bootsCnt <= 4)
        {
            float r = Random.Range(0.08f, 0.12f);
            GameManager.instance.P.data.TridentPower += (GameManager.instance.P.weaponValue.c_Trident + (GameManager.instance.P.data.TridentPower * r));
        }
        else
        {
            // 5 이후 추가
        }
    }



    public void DeadTitleStart()
    {
        GameManager.instance.state = GameState.Stop;
        result.obj.SetActive(true);
        result.backObj.SetActive(false);
        result.title.fillAmount = 0;
        StopCoroutine("CDeadTitle");
        StartCoroutine("CDeadTitle");
    }

    IEnumerator CDeadTitle()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log(result.deadTitleValue.Length);

        foreach (var item in result.deadTitleValue)
        {
            result.title.fillAmount = item;
            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(0.2f);
        result.backObj.SetActive(true);

    }

    public void HowToPlayPannelOn(int index)
    {
        //GameManager.instance.state = GameState.Stop;
        //gameEx.SetActive(isShow);

        HTP_Pannels[index].gameObject.SetActive(true);
        for (int i = 0; i < HTP_Pannels.Length; i++)
        {
            if (index != i)
                HTP_Pannels[i].gameObject.SetActive(false);
        }
    }

    public void HTPOn(bool isShow)
    {
        gameEx.SetActive(isShow);

        if (isShow == true)
        {
            GameManager.instance.state = GameState.Stop;
        }
        else
        {
            GameManager.instance.state = GameState.Play;
        }
    }

}

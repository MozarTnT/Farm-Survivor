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
                expTxt.text = $"{(exp/maxExp * 100f).ToString("F2")} %"; // �Ҽ��� 2�ڸ� ������

            }
        }

    }

    public class ItemCount
    {
        public int bulletdmgCnt { get; set; }
        public int bulletspdCnt { get; set; }
        public int bootsCnt { get; set; }
        public int magnetCnt { get; set; }
        public int bibleCnt { get; set; }
        public int tridentCnt { get; set; }
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
    [SerializeField] private Text killCountText;

    [SerializeField] private GameObject booster;
    [SerializeField] private Image boosterImage;

    // ������ ������
    [SerializeField] private ItemData[] itemDatas;

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

    public ItemCount itemCount;

    void Start()
    {
        topUI.maxExp = 100;
        topUI.Level = 1;
        topUI.KillCount = 0;
        topUI.Exp = 0;

        itemCountSetUp();

        if (GameManager.instance.P.isBooster == true)
        {
            booster.SetActive(true);
        }

    }

    void Update()
    {
        SetKillCount();
        SetTimer();

        if (GameManager.instance.P.isBooster == true)
        {
            ShowBooster();
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            //topUI.Exp += 10;
            //DeadTitleStart();
        }

    }

    public void itemCountSetUp()
    {
        itemCount.bulletdmgCnt = 0;
        itemCount.bulletspdCnt = 0;
        itemCount.bootsCnt = 0;
        itemCount.magnetCnt = 0;
        itemCount.bibleCnt = 0;
        itemCount.tridentCnt = 0;
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

        topUI.timeTxt.text = string.Format("{0:D2}:{1:D2}", min, (int)sec); // 2�ڸ������� ǥ��
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

        // UI ǥ��
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
                GameManager.instance.P.data.HP = 100; // Ǯ�� ȸ��
                float sizeX = ((float)GameManager.instance.P.data.HP / (float)GameManager.instance.P.data.MaxHP) * 120.0f;
                GameManager.instance.P.hpRect.sizeDelta = new Vector2(sizeX, 30.0f);
                break;
            case ItemType.Magnet:
                Debug.Log("�ڼ� ȹ��");
                CaseMagnetSetUp();
                break;
            case ItemType.Trident:
                //Debug.Log("����â ȹ��");
                itemCount.tridentCnt++;
                Player player = GameManager.instance.P;
                player.isTridentOn = true;
                player.InvokeRepeating("AddTrident", 0, 2.5f);
                break;

        }
    }

    private void CaseBulletAttSetUp()
    {
        itemCount.bulletdmgCnt++;
        float r = Random.Range(0.2f, 0.3f); // 20~30 %
        GameManager.instance.P.data.Power += (GameManager.instance.P.weaponValue.c_BulletPower + (float)(GameManager.instance.P.data.Power * r));
        // ������ 20~30% + ������
    }
    private void CaseBulletSpdSetUp()
    {
        itemCount.bulletspdCnt++;
        float r = Random.Range(0.08f, 0.12f); // 8 ~ 12%
        GameManager.instance.B.Speed += (GameManager.instance.P.weaponValue.c_BulletSpd + (GameManager.instance.B.Speed * r));
        // ������ 8~12% + ������
    }
    private void CaseBibleSetUp()
    {
        if (itemCount.bibleCnt < 4) // ���� 4 �Ʒ��϶��� ���� �߰�
        {
            itemCount.bibleCnt++;
            GameManager.instance.P.BibleAdd();
        }
        else if (itemCount.bibleCnt >= 4)// ���� 4 �̻��϶��� ���� �߰�
        {
            float r = Random.Range(0.08f, 0.12f);
            GameManager.instance.P.data.BiblePower += (GameManager.instance.P.weaponValue.c_Bible + (GameManager.instance.P.data.BiblePower * r));
        }
    }
    private void CaseBootsSetUp()
    {
        if (itemCount.bootsCnt < 4)
        {
            itemCount.bootsCnt++;
            float r = Random.Range(0.1f, 0.15f);
            GameManager.instance.P.data.Speed += (GameManager.instance.P.weaponValue.c_Boots + (GameManager.instance.P.data.Speed * r));
        }
       else if (itemCount.bootsCnt >= 4)
        {
            // 3���� �Ѿ ��� ó�� �߰�
        }

    }
    private void CaseMagnetSetUp()
    {
        if(itemCount.magnetCnt < 3) // �׽�Ʈ �ʿ�
        {
            itemCount.magnetCnt++;
            GameManager.instance.P.data.itemDistanceLimit += GameManager.instance.P.weaponValue.c_Magnet; // �⺻ 1.5f �ִ� 3.7f ��ǥ 3�������� ������

            Vector2 vector2 = GameManager.instance.P.magnetScale.localScale;
            vector2.x = (float)GameManager.instance.P.data.itemDistanceLimit;
            vector2.y = (float)GameManager.instance.P.data.itemDistanceLimit;
            GameManager.instance.P.magnetScale.localScale = vector2;

            GameManager.instance.P.magnet.SetActive(true);
        }
        else
        {
            // 3�� �Ѿ ��� ó�� �߰�
        }
    }
    private void CaseTridentSetUp()
    {

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



}

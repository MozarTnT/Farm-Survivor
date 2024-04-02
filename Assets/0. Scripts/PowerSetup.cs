using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PowerSetup : MonoBehaviour
{

    [SerializeField] private Text useridtext;
    [SerializeField] private Text moneyText;
    [SerializeField] private Text minusmoneyText;
    [SerializeField] private Text resultText;

    [SerializeField] private Slider dropSlider;
    [SerializeField] private Slider damageSlider;

    [SerializeField] private GameObject confirmAnnouncement;

    private int minusMoney;
    private int dropPoint;
    private int damagePoint;

    void Start()
    {
        
    }

    void Update()
    {
        SetUserData();
        if (minusMoney <= 0)
        {
            minusmoneyText.text = "";
        }
    }
    public void SetUserData()
    {
        useridtext.text = UserDataConnection.instance.userID;
        moneyText.text = UserDataConnection.instance.myLoginResponseData.score.ToString();
    }
    public void DropOnValueChange()
    {
         int sliderValue = Mathf.RoundToInt(dropSlider.value);
         switch (sliderValue)
         {
            case 0:
                Debug.Log("Slider Value is 0");
                dropPoint = 0;
                StartCalculator();
                minusmoneyText.text = $"- {minusMoney.ToString()}";
                break;
            case 1:
                Debug.Log("Slider Value is 1");
                dropPoint = 300;
                StartCalculator();
                minusmoneyText.text = $"- {minusMoney.ToString()}";
                break;
            case 2:
                Debug.Log("Slider Value is 2");
                dropPoint = 600;
                StartCalculator();
                minusmoneyText.text = $"- {minusMoney.ToString()}";
                break;
            case 3:
                Debug.Log("Slider Value is 3");
                dropPoint = 900;
                StartCalculator();
                minusmoneyText.text = $"- {minusMoney.ToString()}";
                break;
            default:
                Debug.Log("Slider Value is not recognized");
                break;
         }

    }
    public void DamageOnValueChange()
    {
        int sliderValue = Mathf.RoundToInt(damageSlider.value);
        switch (sliderValue)
        {
            case 0:
                Debug.Log("Slider Value is 0");
                damagePoint = 0;
                StartCalculator();
                minusmoneyText.text = $"- {minusMoney.ToString()}";
                break;
            case 1:
                Debug.Log("Slider Value is 1");
                damagePoint = 500;
                StartCalculator();
                minusmoneyText.text = $"- {minusMoney.ToString()}";
                break;
            case 2:
                Debug.Log("Slider Value is 2");
                damagePoint = 1000;
                StartCalculator();
                minusmoneyText.text = $"- {minusMoney.ToString()}";
                break;
            case 3:
                Debug.Log("Slider Value is 3");
                damagePoint = 1500;
                StartCalculator();
                minusmoneyText.text = $"- {minusMoney.ToString()}";
                break;
            default:
                Debug.Log("Slider Value is not recognized");
                break;
        }
    }

    public void ConfirmBtnOnClicked()
    {
        StartCalculator();
        if (minusMoney <= UserDataConnection.instance.myLoginResponseData.score)
        {
            confirmAnnouncement.SetActive(true);
            resultText.text = "능력치가\n 적용되었습니다.";

            AbilityCalculator();
            Debug.Log(GameManager.instance.SetupPower.ToString());
            Debug.Log(GameManager.instance.SetupDrop.ToString());
        }
        else
        {
            confirmAnnouncement.SetActive(true);
            resultText.text = "포인트가\n 부족합니다.";
        }
    }

   

    public void StartCalculator()
    {
        minusMoney = dropPoint + damagePoint;
    }

    public void AnnouncementConfirmBtnOnClicked()
    {
        confirmAnnouncement.SetActive(false);
    }

    public void AbilityCalculator()
    {
        if(damagePoint != 0)
        {
            GameManager.instance.SetupPower = (float)damagePoint / 100.0f;
        }
        else
        {
            GameManager.instance.SetupPower = 0;
        }

        if(dropPoint != 0)
        {
            GameManager.instance.SetupDrop = (float)dropPoint / 100.0f;
        }
        else
        {
            GameManager.instance.SetupDrop = 0;
        }
    }

}

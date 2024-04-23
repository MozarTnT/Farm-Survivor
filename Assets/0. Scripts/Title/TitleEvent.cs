using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class TitleEvent : MonoBehaviour
{
    [SerializeField] public GameObject loadGame;
    [SerializeField] private GameObject TopUI;
    [SerializeField] private GameObject btnUI;
    [SerializeField] private GameObject settingUI;
    [SerializeField] private GameObject loginInfoUI;
    [SerializeField] private GameObject loginUI;
    [SerializeField] private GameObject backPanel;

    [SerializeField] private GameObject touchBtn; 
    [SerializeField] private GameObject settingBtn; 

    [SerializeField] TitleAnimation titleAnimation;
    [SerializeField] UserDataConnection userDataConnection;
    private void OnEnable()
    {
        backPanel.SetActive(false);
    }


    public void LoginExitBtnOnClicked()
    {
        backPanel.gameObject.SetActive(false);
        loginUI.gameObject.SetActive(false);
        touchBtn.gameObject.SetActive(true);
        settingBtn.gameObject.SetActive(true);
    }

    public void InfoExitBtnOnClicked()
    {
        loginInfoUI.SetActive(false);

        if(userDataConnection.isLogin == true)
        {
            SceneManager.LoadScene("CharacterSelect");
        }

    }
    public void OnSelectNewGame() // 새 게임 시작
    {
        SceneManager.LoadScene("CharacterSelect");
    }


    public void OnSelectExit() // 게임 종료
    {
#if UNITY_EDITOR
        UserDataConnection.instance.QuitGameOnClicked();
        UnityEditor.EditorApplication.isPlaying = false;
#else
        UserDataConnection.instance.QuitGameOnClicked();
        Application.Quit(); // 어플리케이션 종료
#endif

    }

    public void SetTouchToStart()
    {
        backPanel.gameObject.SetActive(true);
        loginUI.gameObject.SetActive(true);
        touchBtn.gameObject.SetActive(false);
        settingBtn.gameObject.SetActive(false);
    }


    public void LoadGameBackBtnOnclicked()
    {
        loadGame.SetActive(false);
        TopUI.SetActive(true);
        btnUI.SetActive(true);
    }

    public void SettingBtnOnClicked()
    {
        touchBtn.SetActive(false);
        settingUI.SetActive(true);
        TopUI.SetActive(false);
        btnUI.SetActive(false);

        SettingAnimation();
    }

    public void SettingCfmOnClicked()
    {
        touchBtn.SetActive(true);
        settingUI.SetActive(false);
        TopUI.SetActive(true);
        btnUI.SetActive(true);
    }

    public void SendMessageBtn()
    {
        Application.OpenURL("https://mozartnt.tistory.com/");
    
    }



    public void OnGame()
    {
        SceneManager.LoadScene("Game");
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }

    private void LoadGameAnimation()
    {
        loadGame.transform.DOMoveX(640, 0.5f).SetDelay(0.3f).SetEase(Ease.Linear);
    }

    private void SettingAnimation()
    {
        settingUI.transform.DOMoveX(640, 0.5f).SetDelay(0.3f).SetEase(Ease.Linear);
    }

}

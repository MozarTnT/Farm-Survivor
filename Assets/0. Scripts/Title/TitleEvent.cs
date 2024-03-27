using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class TitleEvent : MonoBehaviour
{
    [SerializeField] public GameObject loadGame;
    [SerializeField] private GameObject TopUI;
    [SerializeField] private GameObject btnUI;
    [SerializeField] private GameObject settingUI;

    [SerializeField] TitleAnimation titleAnimation;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnSelectNewGame() // 새 게임 시작
    {
        SceneManager.LoadScene("CharacterSelect");
    }

    public void OnSelectLoadGame() // 새 게임 시작
    {
        loadGame.SetActive(true);
        TopUI.SetActive(false);
        btnUI.SetActive(false);

        LoadGameAnimation();

    }

    public void OnSelectExit() // 게임 종료
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }
    public void LoadGameBackBtnOnclicked()
    {
        loadGame.SetActive(false);
        TopUI.SetActive(true);
        btnUI.SetActive(true);
    }

    public void SettingBtnOnClicked()
    {
        settingUI.SetActive(true);
        TopUI.SetActive(false);
        btnUI.SetActive(false);

        SettingAnimation();
    }

    public void SettingCfmOnClicked()
    {
        settingUI.SetActive(false);
        TopUI.SetActive(true);
        btnUI.SetActive(true);
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

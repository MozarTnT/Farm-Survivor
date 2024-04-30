using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Dead : MonoBehaviour
{
    
    public void ToMainBtnClicked()
    {
        UIManager.Instance.FadeInLoadCharSelectScene();
    }

    public void ToExitBtnClicked()
    {
#if UNITY_EDITOR
        UserDataConnection.instance.QuitGameOnClicked();
        UnityEditor.EditorApplication.isPlaying = false;
#else
        UserDataConnection.instance.QuitGameOnClicked();
        Application.Quit(); // 어플리케이션 종료
#endif


    }


}

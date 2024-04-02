using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pause;

    void Update()
    {
        stopManager();
    }

    public void stopManager()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            pause.SetActive(true);
            GameManager.instance.state = GameState.Stop;
        }
    }

    public void pauseResumeBtnOnClicked()
    {
        pause.SetActive(false);
        GameManager.instance.state = GameState.Play;
    }

    public void pauseExitBtnOnClicked()
    {
        pause.SetActive(false);
        SceneManager.LoadScene("CharacterSelect");
    }

}

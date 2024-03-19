using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Play,
    Stop,
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameState state = GameState.Stop;

    private UI ui;
    public UI UI 
    {
        get 
        {
            if(ui == null)
            {
                ui = GameObject.FindObjectOfType<UI>();
            }
            return ui;
        }
    }

    private Player p;
    public Player P
    {
        get
        {
            if(p == null) 
            {
                p = GameObject.FindObjectOfType<Player>();
            }
            return p;
        }
    }

    void Start()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void OnGame()
    {
        SceneManager.LoadScene("Game");
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }

}

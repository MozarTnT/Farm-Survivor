using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;


public enum GameState
{
    Play,
    Stop,
}

public enum ItemType
{
    Bullet_Att,
    Bullet_Spd,
    Bible,
    Heal,
    Boots,
    Magnet,
}


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [System.Serializable]
    public class CharSprite
    {
        public List<Sprite> stand;
        public List<Sprite> run;
        public List<Sprite> dead;
    }

    [SerializeField] private GameObject loadGame;
    [SerializeField] private GameObject TopUI;
    [SerializeField] private GameObject btnUI;

    public List<CharSprite> charSprites;



    public GameState state = GameState.Stop;


    public int charSelectIndex = 0;
    public int killCount = 0;

    private Bullet b;

    public Bullet B
    { 
        get
        {
            if(b == null)
            {
                b = GameObject.FindObjectOfType<Bullet>();
            }
            return b;
        }
    }

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

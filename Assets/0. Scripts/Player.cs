using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
    public class Data
    {
        public int HP { get; set; }
        public int MaxHP { get; set; }
        public float Exp { get; set; }
        public int Level { get; set; }
        public float Speed { get; set; }
        public float FireDelay { get; set; }
        public float Power { get; set; }
        public float BiblePower { get; set; }
    }

    public enum State
    {
        Stand,
        Run,
        Dead,
    }

    State state = State.Stand;

    private List<Sprite> stand;
    private List<Sprite> run;
    private List<Sprite> dead;

    [SerializeField] private RectTransform backHpRect;
    [SerializeField] private RectTransform hpRect;
    [SerializeField] private Transform firePos;
    [SerializeField] private Bullet bullet;
    [SerializeField] private Transform bibleTrans;
    [SerializeField] private Transform bible;

    [SerializeField] public GameObject magnet;

    public Transform target;
    public Data data = new Data();

    private float fireTimer = 0;
    public bool isBooster = false;

    public float Stamina { get; set; } = 1;
    private int maxStamina = 1;
    private int minStamina = 0;

    public float staminaUpSpeed = 0.5f;
    public float staminaDownSpeed = 0.8f;

    public float itemDistanceLimit = 1.5f;

    void Start()
    {
        GameManager.instance.state = GameState.Play;


        // 캐릭터 선택에 따른 Sprite 적용
        int index = GameManager.instance.charSelectIndex;

        stand = GameManager.instance.charSprites[index].stand;
        run = GameManager.instance.charSprites[index].run;
        dead = GameManager.instance.charSprites[index].dead;


        GetComponent<SpriteAnimation>().SetSprite(stand, 0.2f);
        data.HP = data.MaxHP = 100;
        data.Speed = 3.0f;
        data.FireDelay = 0.8f;
        data.Power = 20.0f;
        data.BiblePower = 50.0f;

        switch (index)
        {
            case 0:
                data.Speed += (data.Speed * 0.1f);
                break;

            case 1:
                data.Power += (data.Power * 0.2f);
                break;

            case 2:
                data.FireDelay -= 0.1f;
                break;

            case 3:
                isBooster = true;
                break;
        }

        

    }


    void Update()
    {
        if (GameManager.instance != null && GameManager.instance.state != GameState.Play)
            return;


        Move();
        FindMonster();
        SetHPPosition();
        BoosterMode();

        if (target != null)
        {
            FireRotate();
            Fire();
        }

        FindExp();
        bibleTrans.position = transform.position;
        bibleTrans.Rotate(Vector3.back * Time.deltaTime * 300f);

    }

    public void SetHPPosition()
    {
        Vector3 newPosition = transform.position;

        newPosition.y = transform.position.y -1f;

        backHpRect.position = newPosition;
    }



    public void BibleAdd()
    {
        Instantiate(bible, bibleTrans);

        SetBible();
    }

    public void BibleDelete()
    {
        Destroy(bibleTrans.GetChild(bibleTrans.childCount - 1).gameObject);

        SetBible();
    }

    void SetBible()
    {
        bibleTrans.rotation = Quaternion.identity;

        float rot = 360 / bibleTrans.childCount;
        float addRot = 0;
        for (int i = 0; i < bibleTrans.childCount; i++)
        {
            bibleTrans.GetChild(i).rotation = Quaternion.Euler(0f, 0f, addRot);
            addRot += rot;
        }

    }

    private void Move()
    {
        // Move
        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * data.Speed;
        float y = Input.GetAxisRaw("Vertical") * Time.deltaTime * data.Speed;

        float cX = Mathf.Clamp(transform.position.x + x, -19f, 19f);
        float cY = Mathf.Clamp(transform.position.y + y, -19.2f, 19.4f);

        transform.position = new Vector2(cX, cY);

        if (x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (x > 0)
        {
            transform.localScale = Vector3.one;
        }

        // Animation
        if (x == 0 && y == 0 && state != State.Stand)
        {
            state = State.Stand;
            GetComponent<SpriteAnimation>().SetSprite(stand, 0.2f);
        }
        else if ((x != 0 || y != 0) && state != State.Run)
        {
            state = State.Run;
            GetComponent<SpriteAnimation>().SetSprite(run, 1 / data.Speed);
        }
    }


    private void BoosterMode()
    {
        if (isBooster == true)
        {
            if (Input.GetKey(KeyCode.LeftShift) && Stamina > minStamina)
            {
             
                data.Speed = 5.0f;

                Stamina -= staminaDownSpeed * Time.deltaTime;
            }
            else
            {
                data.Speed = 3.0f;
                Stamina += staminaUpSpeed * Time.deltaTime;
            }

            Stamina = Mathf.Clamp(Stamina, minStamina, maxStamina);
        }
    }
    private void FireRotate()
    {
        // 타켓을 찾아 방향 전환
        Vector2 vec = transform.position - target.position;
        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
        firePos.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    }




  
    private void Fire()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= data.FireDelay)
        {
            fireTimer = 0;
            Bullet b = Instantiate(bullet, firePos.GetChild(0));
            b.transform.SetParent(null);
            b.SetPower(power:data.Power);
        }
    }

    public void Hit(int dmg)
    {
        if (data.HP <= 0)
            return;

        data.HP -= dmg;

        float sizeX = ((float)data.HP / (float)data.MaxHP) * 120.0f;
        hpRect.sizeDelta = new Vector2(sizeX, 30.0f);

        Debug.Log($"Player HP : {data.HP} ");

        if(data.HP <= 0)
        {
            isBooster = false;
            GameManager.instance.UI.DeadTitleStart();
        }

    }


    private void FindMonster()
    {
        target = null;
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Monster");

        if(objs.Length > 0)
        {
            float distance = float.MaxValue;
            int findIndex = -1;
            for (int i = 0; i < objs.Length; i++)
            {
                float dis = Vector2.Distance(objs[i].transform.position, transform.position);
                if(dis <= 6)
                {
                    if (dis <= distance)
                    {
                        findIndex = i;
                        distance = dis;
                    }
                }
            }

            if(findIndex != -1)
            {
                target = objs[findIndex].transform;
            }
        }
    }

    public void FindExp()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Exp");

        if(objs.Length > 0) 
        {
            foreach(var item in objs.Select((value, index) => (value, index)))
            {
                float distance = Vector2.Distance(transform.position, item.value.transform.position);

                if(distance <= itemDistanceLimit)
                {
                    item.value.GetComponent<Exp>().Target = transform;

                }
            }
        }
    }
}

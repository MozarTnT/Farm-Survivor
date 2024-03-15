using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Player : MonoBehaviour
{
    public class Data
    {
        public int HP { get; set; }
        public float Exp { get; set; }
        public int Level { get; set; }
        public float Speed { get; set; }
    }

    public enum State
    {
        Stand,
        Run,
        Dead,
    }

    State state = State.Stand;

    [SerializeField] private List<Sprite> stand;
    [SerializeField] private List<Sprite> run;
    [SerializeField] private List<Sprite> dead;

    [SerializeField] private Transform firePos;
    [SerializeField] private Bullet bullet;
    [SerializeField] private Transform bibleTrans;
    [SerializeField] private Transform bible;

    [SerializeField] private float fireDelay;

    public Transform target;
    public Data data = new Data();

    private float fireTimer = 0;

    void Start()
    {
        GetComponent<SpriteAnimation>().SetSprite(stand, 0.1f);
        data.HP = 100;
        data.Speed = 3.0f;
    }

    
    void Update()
    {
        Move();
        FindMonster();

        if(target != null)
        {
            FireRotate();
            Fire();
        }

        FindExp();

        bibleTrans.Rotate(Vector3.back * Time.deltaTime * 300f);

        if(Input.GetKeyDown(KeyCode.F5))
        {
            Instantiate(bible, bibleTrans);

            bibleTrans.rotation = Quaternion.identity;

            float rot = 360 / bibleTrans.childCount;
            float addRot = 0;
            for(int i = 0; i < bibleTrans.childCount; i++)
            {
                bibleTrans.GetChild(i).rotation = Quaternion.Euler(0f, 0f, addRot);
                addRot += rot;
            }
        } 

        if(Input.GetKeyDown(KeyCode.F6))
        {
            Destroy(bibleTrans.GetChild(bibleTrans.childCount - 1).gameObject);

            bibleTrans.rotation = Quaternion.identity;

            float rot = 360 / bibleTrans.childCount;
            float addRot = 0;
            for (int i = 0; i < bibleTrans.childCount; i++)
            {
                bibleTrans.GetChild(i).rotation = Quaternion.Euler(0f, 0f, addRot);
                addRot += rot;
            }
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
            GetComponent<SpriteAnimation>().SetSprite(stand, 0.1f);
        }
        else if ((x != 0 || y != 0) && state != State.Run)
        {
            state = State.Run;
            GetComponent<SpriteAnimation>().SetSprite(run, 1 / data.Speed);
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
        if (fireTimer >= fireDelay)
        {
            fireTimer = 0;
            Instantiate(bullet, firePos.GetChild(0)).transform.SetParent(null);
        }
    }

    public void Hit(int dmg)
    {
        if (data.HP <= 0)
            return;

        data.HP -= dmg;
        Debug.Log($"Player HP : {data.HP} ");

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
                if(distance <= 3)
                {
                    item.value.GetComponent<Exp>().Target = transform;

                }
            }
        }
    }
}

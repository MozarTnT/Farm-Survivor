using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public abstract class Boss : MonoBehaviour
{
    public enum State
    {
        Run,
        Hit,
        Dead,
    }

    [System.Serializable]
    public class Data
    {
        public int Level { get; set; }
        public float HP { get; set; }
        public int Power { get; set; }
        public float Defence { get; set; }
        public float Speed { get; set; }
        public float HitDelay { get; set; }
        public float AttDelay { get; set; }
    }

    [SerializeField] protected List<Sprite> run;
    [SerializeField] protected List<Sprite> hit;
    [SerializeField] protected List<Sprite> dead;

    private Exp[] exps;

    public Data data = new Data();

    private SpriteRenderer sr;
    private SpriteAnimation sa;
    private State state = State.Run;

    public WaterPop wpop;

    private float attTimer;

    public static Vector3 waterPopVec = new Vector3();

    // -- Test
    public Transform target;

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void SetExp(Exp[] exps)
    {
        this.exps = exps;
    }


    public virtual void Init()
    {
        sr = GetComponent<SpriteRenderer>();
        sa = GetComponent<SpriteAnimation>();

        sa.SetSprite(run, 0.4f / data.Speed);
    }


    void Update()
    {

        if (GameManager.instance != null && GameManager.instance.state != GameState.Play)
            return;


        if (target == null || data.HP <= 0)
            return;


        if (data.HitDelay >= 0)
        {
            data.HitDelay -= Time.deltaTime;
            return;
        }
        else if (state == State.Hit)
        {
            state = State.Run;
            sa.SetSprite(run, 0.3f / data.Speed);
        }

        Direction();

    }

    void Direction()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance > 1.0f)
        {
            Vector2 dis = target.position - transform.position;
            Vector2 dir = dis.normalized * Time.deltaTime * data.Speed;
            transform.Translate(dir);

            if (dir.normalized.x != 0)
            {
                sr.flipX = dir.normalized.x < 0 ? false : true;
            }


        }
        else
        {
            attTimer += Time.deltaTime;
            if (attTimer >= data.AttDelay)
            {
                attTimer = 0;
                target.GetComponent<Player>().Hit(data.Power);
            }
        }

    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (data.HP < 0)
        {
            return;
        }
        if (collision.CompareTag("pBullet"))
        {
            Bullet b = collision.GetComponent<Bullet>();
            data.HP -= GameManager.instance.P.data.Power * (1 - data.Defence);
            Debug.Log($"보스 체력 = {data.HP}");
            state = State.Hit;
            data.HitDelay = 0.3f;
            sa.SetSprite(hit, 0.3f);

            if (data.HP <= 0)
            {
                GetComponent<Collider2D>().enabled = false;
                tag = "Untagged";
                sa.SetSprite(dead, 0.1f, 1.0f, End); // Enemy 제거
                GameManager.instance.killCount++;
            }

            BulletPooling.Instance.AddpBullet(b); // 총알 삭제
        }

        if (collision.CompareTag("Bible"))
        {
            data.HP -= GameManager.instance.P.data.BiblePower * (1 - data.Defence);
            Debug.Log($"보스 체력 = {data.HP}");


            state = State.Hit;
            data.HitDelay = 0.3f;
            sa.SetSprite(hit, 0.3f);

            if (data.HP <= 0)
            {
                GetComponent<Collider2D>().enabled = false;
                tag = "Untagged";
                sa.SetSprite(dead, 0.3f, 1.0f, End); // Enemy 제거           
                GameManager.instance.killCount++;
            }
        }

        if (collision.CompareTag("Trident"))
        {
            data.HP -= (int)GameManager.instance.P.data.TridentPower;
            Debug.Log($"보스 체력 = {data.HP}");


            state = State.Hit;
            data.HitDelay = 0.3f;
            sa.SetSprite(hit, 0.3f);

            if (data.HP <= 0)
            {
                GetComponent<Collider2D>().enabled = false;
                tag = "Untagged";
                sa.SetSprite(dead, 0.3f, 1.0f, End); // Enemy 제거           
                GameManager.instance.killCount++;
            }
        }

        if (collision.CompareTag("Water"))
        {
            data.HP -= GameManager.instance.P.data.WaterPower * (1 - data.Defence);

            state = State.Hit;
            data.HitDelay = 0.1f;
            sa.SetSprite(hit, 0.2f);

            Destroy(collision.gameObject);
            waterPopVec = collision.transform.position;


            float aX = Mathf.Abs(waterPopVec.x);
            float aY = Mathf.Abs(waterPopVec.y);

            // Instantiate(wpop, Player.Instance.weaponParent); // 보스에서는 물폭탄 미 생성


            if (data.HP <= 0)
            {
                GetComponent<Collider2D>().enabled = false;
                tag = "Untagged";
                sa.SetSprite(dead, 0.1f, 1.0f, End); // Enemy 제거           
                GameManager.instance.killCount++;
            }
        }
    }

    private float damageInterval = 0.5f; // 데미지 간격을 설정합니다.
    private float timeSinceLastDamage = 0f; // 마지막 데미지가 입혀진 후의 시간입니다.

    private void OnTriggerStay2D(Collider2D collision) // 물방울 지속시간 trigger
    {

        if (collision.CompareTag("WaterPop"))
        {
            timeSinceLastDamage += Time.deltaTime;

            if (timeSinceLastDamage >= damageInterval)
            {
                data.HP -= GameManager.instance.P.data.WaterPopPower * (1 - data.Defence);

                Debug.Log(data.HP);

                timeSinceLastDamage = 0f;

                state = State.Hit;
                data.HitDelay = 0.1f;
                sa.SetSprite(hit, 0.1f);

                if (data.HP <= 0)
                {
                    GetComponent<Collider2D>().enabled = false;
                    tag = "Untagged";
                    sa.SetSprite(dead, 0.1f, 1.0f, End); // Enemy 제거           
                    GameManager.instance.killCount++;
                }

            }
        }

    }


    void End()
    {

        int expIndex = data.Level <= 2 ? 0 : data.Level <= 5 ? Random.Range(0, 2) : Random.Range(0, 3);
        Gold gc = ItemPooling.Instance.GetPGoldCoin();
        gc.transform.position = transform.position;
        gc.transform.rotation = transform.rotation; 

        Destroy(gameObject);
        GameManager.instance.UI.bossHP.gameObject.SetActive(false);

    }

}

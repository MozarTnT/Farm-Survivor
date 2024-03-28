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
        public int HP { get; set; }
        public int Power { get; set; }
        public int Defence { get; set; }
        public float Speed { get; set; }
        public float HitDelay { get; set; }
        public float AttDelay { get; set; }
    }

    [SerializeField] protected List<Sprite> run;
    [SerializeField] protected List<Sprite> hit;
    [SerializeField] protected List<Sprite> dead;

    private Exp[] exps;

    protected Data data = new Data();

    private SpriteRenderer sr;
    private SpriteAnimation sa;
    private State state = State.Run;

    private float attTimer;

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

            if (dir.normalized.x > 0)
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
            //Bullet b = collision.GetComponent<Bullet>();
            data.HP -= (int)GameManager.instance.P.data.Power;

            state = State.Hit;
            data.HitDelay = 0.5f;
            sa.SetSprite(hit, 0.3f);

            if (data.HP <= 0)
            {
                GetComponent<Collider2D>().enabled = false;
                tag = "Untagged";
                sa.SetSprite(dead, 0.1f, 1.0f, End); // Enemy 제거
                GameManager.instance.killCount++;
            }

            Destroy(collision.gameObject); // 총알 삭제
        }

        if (collision.CompareTag("Bible"))
        {
            data.HP -= (int)GameManager.instance.P.data.BiblePower;

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

            state = State.Hit;
            data.HitDelay = 0.1f;
            sa.SetSprite(hit, 0.3f);

            if (data.HP <= 0)
            {
                GetComponent<Collider2D>().enabled = false;
                tag = "Untagged";
                sa.SetSprite(dead, 0.1f, 1.0f, End); // Enemy 제거           
                GameManager.instance.killCount++;
            }
        }

    }




    void End()
    {
        int rand = Random.Range(0, 100);
        if (rand < 95)
        {
            int expIndex = data.Level <= 2 ? 0 : data.Level <= 5 ? Random.Range(0, 2) : Random.Range(0, 3);
            Instantiate(exps[expIndex], transform.position, Quaternion.identity);
        }
        else
        {
            // 아이템 박스
        }

        Destroy(gameObject);
    }

}

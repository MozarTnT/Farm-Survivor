using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public abstract class Monster : MonoBehaviour
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

    protected Data data = new Data();

    private SpriteRenderer sr;
    private SpriteAnimation sa;
    private State state = State.Run;

    // -- Test
    public Transform target;

    public virtual void Init()
    {
        sr = GetComponent<SpriteRenderer>();
        sa = GetComponent<SpriteAnimation>();

        sa.SetSprite(run, 0.2f / data.Speed);
    }


    void Update()
    {
        if (target == null)
        {
            return;
        }

        if(data.HitDelay >= 0)
        {
            data.HitDelay -= Time.deltaTime;
            return;
        }
        else if (state == State.Hit)
        {
            state = State.Run;
            sa.SetSprite(run, 0.2f / data.Speed);
        }
        
    


        float distance = Vector3.Distance(target.position, transform.position);

        if(distance > 1.0f)
        {
            Vector2 dis = target.position - transform.position;
            Vector2 dir = dis.normalized * Time.deltaTime * data.Speed;
            transform.Translate(dir);

            if (dir.normalized.x != 0)
            {
                sr.flipX = dir.normalized.x > 0 ? false : true;
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
            data.HP -= b.Power;

            state = State.Hit;
            data.HitDelay = 0.5f;
            sa.SetSprite(hit, 0.1f);

            if(data.HP <= 0)
            {
                sa.SetSprite(dead, 0.1f, 1f, () => Destroy(gameObject)); // Enemy 제거
            }

            Destroy(collision.gameObject); // 총알 삭제
        }
    }
}

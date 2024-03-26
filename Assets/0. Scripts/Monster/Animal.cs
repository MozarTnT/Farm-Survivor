using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Animal : MonoBehaviour
{

    public enum State
    { 
        Idle,
        Walk,
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

    private State state = State.Idle;


    protected Data data = new Data();

    public Transform target;

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (GameManager.instance != null && GameManager.instance.state != GameState.Play)
            return;


        if (target == null || data.HP <= 0)
            return;


    }



    //public void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (data.HP < 0)
    //    {
    //        return;
    //    }
    //    if (collision.CompareTag("pBullet"))
    //    {
    //        Bullet b = collision.GetComponent<Bullet>();
    //        data.HP -= (int)b.Power;

    //        state = State.Hit;
    //        data.HitDelay = 0.5f;
    //        sa.SetSprite(hit, 0.1f);

    //        if (data.HP <= 0)
    //        {
    //            GetComponent<Collider2D>().enabled = false;
    //            tag = "Untagged";
    //            sa.SetSprite(dead, 0.1f, 1.0f, End); // Enemy 제거
    //        }

    //        Destroy(collision.gameObject); // 총알 삭제
    //    }
    //    if (collision.CompareTag("Bible"))
    //    {
    //        data.HP -= (int)GameManager.instance.P.data.BiblePower;

    //        state = State.Hit;
    //        data.HitDelay = 0.3f;
    //        sa.SetSprite(hit, 0.1f);

    //        if (data.HP <= 0)
    //        {
    //            GetComponent<Collider2D>().enabled = false;
    //            tag = "Untagged";
    //            sa.SetSprite(dead, 0.1f, 1.0f, End); // Enemy 제거           
    //        }
    //    }
    //}
}

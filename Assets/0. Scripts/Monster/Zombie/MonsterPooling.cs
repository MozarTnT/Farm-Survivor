using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterPooling : Singleton<MonsterPooling>
{
    [SerializeField] private GreenZombie pGreenZombie;
    [SerializeField] private WhiteZombie pWhiteZombie;
    [SerializeField] private TombZombie pTombZombie;
    [SerializeField] public Transform zombie_Parent;

    Queue<GreenZombie> pGreenZombiesQueue = new Queue<GreenZombie>();
    Queue<WhiteZombie> pWhiteZombiesQueue = new Queue<WhiteZombie>();
    Queue<TombZombie> pTombZombiesQueue = new Queue<TombZombie>();


    public GreenZombie GetPGreenZombie() // 그린 좀비 풀링
    {
        Player p = GameManager.instance.P;
        GreenZombie qGreenZombie = null;

        if(pGreenZombiesQueue.Count == 0)
        {
            Debug.Log("Creating New Zombie");
            GreenZombie gz = Instantiate(pGreenZombie);
            gz.transform.SetParent(zombie_Parent);
            pGreenZombiesQueue.Enqueue(gz);

            qGreenZombie = pGreenZombiesQueue.Dequeue();
        }
        else
        {
            Debug.Log("Reusing Zombie.");
            qGreenZombie = pGreenZombiesQueue.Dequeue();
            qGreenZombie.gameObject.SetActive(true);
        }
        return qGreenZombie;
    }

    public void AddpGreenZombie(GreenZombie gz)
    {
        gz.gameObject.SetActive(false);
        pGreenZombiesQueue.Enqueue(gz);
    }


    public WhiteZombie GetPWhiteZombie() // 화이트 좀비 풀링
    {
        Player p = GameManager.instance.P;
        WhiteZombie qWhiteZombie = null;

        if (pWhiteZombiesQueue.Count == 0)
        {
            Debug.Log("Creating New White Zombie");
            WhiteZombie wz = Instantiate(pWhiteZombie);
            wz.transform.SetParent(zombie_Parent);
            pWhiteZombiesQueue.Enqueue(wz);

            qWhiteZombie = pWhiteZombiesQueue.Dequeue();
        }
        else
        {
            Debug.Log("Reusing White Zombie.");
            qWhiteZombie = pWhiteZombiesQueue.Dequeue();
            qWhiteZombie.gameObject.SetActive(true);
        }
        return qWhiteZombie;
    }

    public void AddpWhiteZombie(WhiteZombie wz)
    {
        wz.gameObject.SetActive(false);
        pWhiteZombiesQueue.Enqueue(wz);
    }

    public TombZombie GetPTombZombie() // 무덤 좀비 풀링
    {
        Player p = GameManager.instance.P;
        TombZombie qTombZombie = null;

        if (pTombZombiesQueue.Count == 0)
        {
            Debug.Log("Creating New Tomb Zombie");
            TombZombie tz = Instantiate(pTombZombie);
            tz.transform.SetParent(zombie_Parent);
            pTombZombiesQueue.Enqueue(tz);

            qTombZombie = pTombZombiesQueue.Dequeue();
        }
        else
        {
            Debug.Log("Reusing Tomb Zombie.");
            qTombZombie = pTombZombiesQueue.Dequeue();
            qTombZombie.gameObject.SetActive(true);
        }
        return qTombZombie;
    }

    public void AddpTombZombie(TombZombie tz)
    {
        tz.gameObject.SetActive(false);
        pTombZombiesQueue.Enqueue(tz);
    }

}

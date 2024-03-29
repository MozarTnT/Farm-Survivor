using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterPooling : Singleton<MonsterPooling>
{
    [SerializeField] private GreenZombie pGreenZombie;
    [SerializeField] public Transform zombie_Parent;

    Queue<GreenZombie> pGreenZombiesQueue = new Queue<GreenZombie>();

    public GreenZombie GetPGreenZombie()
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

}

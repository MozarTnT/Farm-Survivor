using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletPooling : Singleton<BulletPooling>
{

    [SerializeField] private Bullet pBullet;
    [SerializeField] public Transform pBulletParent;

    Queue<Bullet> pBulletQueue = new Queue<Bullet>();

    public Bullet GetPBullet()
    {
        Player p = GameManager.instance.P;
        Bullet qBullet = null;

        if (pBulletQueue.Count == 0)
        {
            Debug.Log("Creating new bullet.");
            Quaternion quaternion = GameManager.instance.P.firePos.transform.rotation;
            Bullet b = Instantiate(pBullet, p.transform.GetChild(0).position, quaternion);
            b.transform.SetParent(pBulletParent);
            pBulletQueue.Enqueue(b);

            qBullet = pBulletQueue.Dequeue();

        }
        else
        {
            Debug.Log("Reusing bullet from the pool.");
            Quaternion quaternion = GameManager.instance.P.firePos.transform.rotation;
            qBullet = pBulletQueue.Dequeue();
            qBullet.transform.position = p.transform.GetChild(0).position;
            qBullet.transform.rotation = quaternion;
            qBullet.gameObject.SetActive(true);
        }

        return qBullet;
    }
    
    public void AddpBullet(Bullet b)
    {
        b.gameObject.SetActive(false);
        pBulletQueue.Enqueue(b);
    }




}

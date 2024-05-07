using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TridentPooling : Singleton<TridentPooling>
{
    [SerializeField] private Trident pTrident;
    [SerializeField] public Transform weaponParent;

    Queue<Trident> pTridentQueue = new Queue<Trident>();

    public Trident GetPTrident() // Trident 생성
    {
        Player p = GameManager.instance.P;
        Trident qTrident = null;

        if (pTridentQueue.Count == 0) // 만든 Trident가 없다면
        {
            Debug.Log("Creating new trident.");
            Trident t = Instantiate(pTrident);
            t.transform.position = GameManager.instance.P.tridentPos.position;
            t.transform.SetParent(weaponParent);
            pTridentQueue.Enqueue(t);

            qTrident = pTridentQueue.Dequeue();
        }
        else // 만든 Trident가 있다면
        {
            Debug.Log("Reusing tridnt.");
            qTrident = pTridentQueue.Dequeue();
            qTrident.transform.position = p.tridentPos.position;
            qTrident.gameObject.SetActive(true);
            qTrident.tag = "Trident";
        }

        return qTrident;
    }

    public void AddpTrident(Trident td) // Trident 비활성화
    {
        td.gameObject.SetActive(false);
        pTridentQueue.Enqueue(td);
    }

}

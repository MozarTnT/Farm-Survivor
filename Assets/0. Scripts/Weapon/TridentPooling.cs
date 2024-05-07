using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TridentPooling : Singleton<TridentPooling>
{
    [SerializeField] private Trident pTrident;
    [SerializeField] public Transform weaponParent;

    Queue<Trident> pTridentQueue = new Queue<Trident>();

    public Trident GetPTrident() // Trident ����
    {
        Player p = GameManager.instance.P;
        Trident qTrident = null;

        if (pTridentQueue.Count == 0) // ���� Trident�� ���ٸ�
        {
            Debug.Log("Creating new trident.");
            Trident t = Instantiate(pTrident);
            t.transform.position = GameManager.instance.P.tridentPos.position;
            t.transform.SetParent(weaponParent);
            pTridentQueue.Enqueue(t);

            qTrident = pTridentQueue.Dequeue();
        }
        else // ���� Trident�� �ִٸ�
        {
            Debug.Log("Reusing tridnt.");
            qTrident = pTridentQueue.Dequeue();
            qTrident.transform.position = p.tridentPos.position;
            qTrident.gameObject.SetActive(true);
            qTrident.tag = "Trident";
        }

        return qTrident;
    }

    public void AddpTrident(Trident td) // Trident ��Ȱ��ȭ
    {
        td.gameObject.SetActive(false);
        pTridentQueue.Enqueue(td);
    }

}

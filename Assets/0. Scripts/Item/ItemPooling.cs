using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemPooling : Singleton<ItemPooling>
{
    [SerializeField] private Bronze pBronzeCoin;
    [SerializeField] private Silver pSilverCoin;
    [SerializeField] private Gold pGoldCoin;

    [SerializeField] public Transform coin_Parent;

    Queue<Bronze> pBronzeCoinsQueue = new Queue<Bronze>();
    Queue<Silver> pSilverCoinsQueue = new Queue<Silver>();
    Queue<Gold> pGoldCoinsQueue = new Queue<Gold>();

    public Bronze GetPBronzeCoin()
    {
        Player p = GameManager.instance.P;
        Bronze qBronzeCoin = null;

        if(pBronzeCoinsQueue.Count == 0)
        {
            Debug.Log("Creatring New Bronze Coin");
            Bronze bc = Instantiate(pBronzeCoin);
            bc.transform.SetParent(coin_Parent);
            pBronzeCoinsQueue.Enqueue(bc);

            qBronzeCoin = pBronzeCoinsQueue.Dequeue();
        }
        else
        {
            Debug.Log("Reusing Bronze Coin");
            qBronzeCoin = pBronzeCoinsQueue.Dequeue();
            qBronzeCoin.gameObject.SetActive(true);
        }
        return qBronzeCoin;
    }
    public void AddpBronzeCoin(Bronze bc)
    {
        bc.gameObject.SetActive(false);
        pBronzeCoinsQueue.Enqueue(bc);
    }


    public Gold GetPGoldCoin()
    {
        Player p = GameManager.instance.P;
        Gold qGoldCoin = null;

        if (pGoldCoinsQueue.Count == 0)
        {
            Debug.Log("Creatring New Gold Coin");
            Gold gc = Instantiate(pGoldCoin);
            gc.transform.SetParent(coin_Parent);
            pGoldCoinsQueue.Enqueue(gc);

            qGoldCoin = pGoldCoinsQueue.Dequeue();
        }
        else
        {
            Debug.Log("Reusing Gold Coin");
            qGoldCoin = pGoldCoinsQueue.Dequeue();
            qGoldCoin.gameObject.SetActive(true);
        }
        return qGoldCoin;
    }
    public void AddpGoldCoin(Gold gc)
    {
        gc.gameObject.SetActive(false);
        pGoldCoinsQueue.Enqueue(gc);
    }


}

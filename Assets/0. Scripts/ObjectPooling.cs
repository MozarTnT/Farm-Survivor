using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling Instance;

    public int defaultCapacity = 10;
    public int maxPoolSize = 15;

    public GameObject tridentPrefab;

    public IObjectPool<GameObject> Pool { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }


    }

    //private void Init()
    //{
    //    Pool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool,
    //    OnDestroyPoolObject, true, defaultCapacity, maxPoolSize);

    //    // �̸� ������Ʈ ���� �س���
    //    for (int i = 0; i < defaultCapacity; i++)
    //    {
    //        Trident trident = CreatePooledItem().GetComponent<Trident>();
    //        trident.Pool.Release(bullet.gameObject);
    //    }
    //}

    //// ����
    //private GameObject CreatePooledItem()
    //{
    //    GameObject poolGo = Instantiate(tridentPrefab);
    //    poolGo.GetComponent<Trident>().Pool = this.Pool;
    //    return poolGo;
    //}

    // ���
    private void OnTakeFromPool(GameObject poolGo)
    {
        poolGo.SetActive(true);
    }

    // ��ȯ
    private void OnReturnedToPool(GameObject poolGo)
    {
        poolGo.SetActive(false);
    }

    // ����
    private void OnDestroyPoolObject(GameObject poolGo)
    {
        Destroy(poolGo);
    }
}

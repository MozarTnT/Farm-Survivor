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

    //    // 미리 오브젝트 생성 해놓기
    //    for (int i = 0; i < defaultCapacity; i++)
    //    {
    //        Trident trident = CreatePooledItem().GetComponent<Trident>();
    //        trident.Pool.Release(bullet.gameObject);
    //    }
    //}

    //// 생성
    //private GameObject CreatePooledItem()
    //{
    //    GameObject poolGo = Instantiate(tridentPrefab);
    //    poolGo.GetComponent<Trident>().Pool = this.Pool;
    //    return poolGo;
    //}

    // 사용
    private void OnTakeFromPool(GameObject poolGo)
    {
        poolGo.SetActive(true);
    }

    // 반환
    private void OnReturnedToPool(GameObject poolGo)
    {
        poolGo.SetActive(false);
    }

    // 삭제
    private void OnDestroyPoolObject(GameObject poolGo)
    {
        Destroy(poolGo);
    }
}

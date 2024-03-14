using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    float spawnTimer;
    float spawnDelay;

    [SerializeField] private Monster m;
    [SerializeField] private Player player;

    void Start()
    {
        spawnDelay = Random.Range(1, 5);
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if(spawnTimer >= spawnDelay)
        {
            spawnTimer = 0;
            spawnDelay = Random.Range(5, 15);
            Spawn();

        }
    }

    // 랜덤 스폰 
    // 위에서 언급한 Plane의 자식인 RespawnRange 오브젝트
    public GameObject rangeObject;
    Collider2D rangeCollider;

    private void Awake()
    {
        rangeCollider = rangeObject.GetComponent<Collider2D>();
    }


    public void Spawn()
    {
        Monster mon = Instantiate(m, Return_RandomPosition(), Quaternion.identity);
        mon.SetTarget(player.transform);
    }

    Vector3 Return_RandomPosition()
    {
        Vector3 originPosition = rangeObject.transform.position;
        // 콜라이더의 사이즈를 가져오는 bound.size 사용
        float range_X = rangeCollider.bounds.size.x;
        float range_Y = rangeCollider.bounds.size.y;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Y = Random.Range((range_Y / 2) * -1, range_Y / 2);

        return originPosition + new Vector3(range_X, range_Y, 0f);
    }
}

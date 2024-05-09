using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MonsterSpawn : MonoBehaviour
{
    private float greenspawnTimer;
    private float greenspawnDelay;

    private float whitespawnTimer;
    private float whitespawnDelay;

    private float tombspawnTimer;
    private float tombspawnDelay;

    [SerializeField] private Monster m;
    [SerializeField] private Player player;
    [SerializeField] private Exp[] exps;

    void Start()
    {
        greenspawnDelay = Random.Range(1, 3);
        whitespawnDelay = Random.Range(3, 5);
        tombspawnDelay = Random.Range(5, 10);
    }

    void Update()
    {
        if (GameManager.instance != null && GameManager.instance.state != GameState.Play)
            return;

        greenspawnTimer += Time.deltaTime;
        whitespawnTimer += Time.deltaTime;
        tombspawnTimer += Time.deltaTime;


        if (greenspawnTimer >= greenspawnDelay)
        {
            greenspawnTimer = 0;
            greenspawnDelay = Random.Range(3, 5);
            GreenSpawn();
        }
     
        if (GameManager.instance.UI.topUI.Level >= 5 && whitespawnTimer >= whitespawnDelay)
        {
            whitespawnTimer = 0;
            whitespawnDelay = Random.Range(3, 10);
            WhiteSpawn();
        }

        if(GameManager.instance.UI.topUI.Level >= 10 && tombspawnTimer >= tombspawnDelay)
        {
            tombspawnTimer = 0;
            tombspawnDelay = Random.Range(5, 12);
            TombSpawn();
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

    public void GreenSpawn()
    {
        Monster mon = MonsterPooling.Instance.GetPGreenZombie();
        mon.GetComponent<Collider2D>().enabled = true;
        mon.transform.position = Return_RandomPosition();
        mon.SetTarget(player.transform);
        mon.SetExp(exps);
    }

    public void WhiteSpawn()
    {
        Monster mon = MonsterPooling.Instance.GetPWhiteZombie();
        mon.GetComponent<Collider2D>().enabled = true;
        mon.transform.position = Return_RandomPosition();
        mon.SetTarget(player.transform);
        mon.SetExp(exps);
    }

    public void TombSpawn()
    {
        Monster mon = MonsterPooling.Instance.GetPTombZombie();
        mon.GetComponent<Collider2D>().enabled = true;
        mon.transform.position = Return_RandomPosition();
        mon.SetTarget(player.transform);
        mon.SetExp(exps);
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

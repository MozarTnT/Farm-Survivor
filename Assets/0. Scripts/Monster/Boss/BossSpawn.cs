using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    float spawnTimer; // 스폰 타이머
    float spawnDelay; // 스폰 지연 시간

    [SerializeField] private Player player; // 플레이어
    [SerializeField] private Exp[] exps; // 경험치 배열
    [SerializeField] private KingSlime ks; // 킹 슬라임

    private bool slimeSpawn = false; // 슬라임 스폰 여부

    void Start()
    {
        spawnDelay = Random.Range(1, 3); // 랜덤 스폰 지연 시간 설정
    }

    void Update()
    {
        if (GameManager.instance != null && GameManager.instance.state != GameState.Play)
            return; // 게임 상태 확인

        spawnTimer += Time.deltaTime; // 타이머 증가

        if (GameManager.instance.UI.topUI.Level == 10 && !slimeSpawn) // 레벨 10일 때
        {
            slimeSpawn = true; // 슬라임 스폰 설정
            Debug.Log(GameManager.instance.UI.topUI.Level); // 레벨 로그
            SpawnKS(); // 킹 슬라임 스폰

            GameManager.instance.UI.uiAnimation.gameObject.SetActive(true); // UI 애니메이션 활성화
            GameManager.instance.UI.bossGameObject.gameObject.SetActive(true); // 보스 오브젝트 활성화
            GameManager.instance.UI.bossHP.gameObject.SetActive(true); // 보스 HP UI 활성화
        }
    }

    public GameObject rangeObject; // 스폰 범위 오브젝트
    Collider2D rangeCollider; // 범위 콜라이더

    private void Awake()
    {
        rangeCollider = rangeObject.GetComponent<Collider2D>(); // 콜라이더 가져오기
    }

    public void SpawnKS() // 킹 슬라임 스폰
    {
        KingSlime kingSlime = Instantiate(ks, Return_RandomPosition(), Quaternion.identity); // 킹 슬라임 인스턴스화
        kingSlime.SetTarget(player.transform); // 타겟 설정
        kingSlime.SetExp(exps); // 경험치 설정
    }

    Vector3 Return_RandomPosition() // 랜덤 위치 반환
    {
        Vector3 originPosition = rangeObject.transform.position; // 원래 위치
        float range_X = rangeCollider.bounds.size.x; // X 범위
        float range_Y = rangeCollider.bounds.size.y; // Y 범위

        range_X = Random.Range((range_X / 2) * -1, range_X / 2); // 랜덤 X 위치
        range_Y = Random.Range((range_Y / 2) * -1, range_Y / 2); // 랜덤 Y 위치

        return originPosition + new Vector3(range_X, range_Y, 0f); // 최종 위치 반환
    }
}

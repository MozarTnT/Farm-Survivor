using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boss : MonoBehaviour
{
    public enum State // 상태 열거형
    {
        Run,
        Hit,
        Dead,
    }

    [System.Serializable]
    public class Data // 보스 데이터 클래스
    {
        public int Level { get; set; } // 레벨
        public float HP { get; set; } // 체력
        public int Power { get; set; } // 공격력
        public float Defence { get; set; } // 방어력
        public float Speed { get; set; } // 이동 속도
        public float HitDelay { get; set; } // 맞은 후 지연 시간
        public float AttDelay { get; set; } // 공격 지연 시간
    }

    [SerializeField] protected List<Sprite> run; // 달리기 스프라이트
    [SerializeField] protected List<Sprite> hit; // 맞았을 때 스프라이트
    [SerializeField] protected List<Sprite> dead; // 죽었을 때 스프라이트

    private Exp[] exps; // 경험치 배열
    public Data data = new Data(); // 보스 데이터

    private SpriteRenderer sr; // 스프라이트 렌더러
    private SpriteAnimation sa; // 스프라이트 애니메이션
    private State state = State.Run; // 초기 상태

    public WaterPop wpop; // 물 효과

    private float attTimer; // 공격 타이머
    public static Vector3 waterPopVec = new Vector3(); // 물 효과 위치

    public Transform target; // 타겟

    public void SetTarget(Transform target) // 타겟 설정
    {
        this.target = target;
    }

    public void SetExp(Exp[] exps) // 경험치 설정
    {
        this.exps = exps;
    }

    public virtual void Init() // 초기화
    {
        sr = GetComponent<SpriteRenderer>(); // 스프라이트 렌더러 가져오기
        sa = GetComponent<SpriteAnimation>(); // 스프라이트 애니메이션 가져오기
        sa.SetSprite(run, 0.4f / data.Speed); // 달리기 애니메이션 설정
    }

    void Update() // 업데이트
    {
        if (GameManager.instance != null && GameManager.instance.state != GameState.Play)
            return; // 게임 상태 확인

        if (target == null || data.HP <= 0)
            return; // 타겟이 없거나 체력이 0 이하일 때 종료

        if (data.HitDelay >= 0) // 맞은 후 지연 시간 처리
        {
            data.HitDelay -= Time.deltaTime;
            return;
        }
        else if (state == State.Hit) // 맞았을 때 상태 변경
        {
            state = State.Run;
            sa.SetSprite(run, 0.3f / data.Speed); // 달리기 애니메이션 설정
        }

        Direction(); // 방향 설정
    }

    void Direction() // 방향 설정
    {
        float distance = Vector3.Distance(target.position, transform.position); // 거리 계산
        if (distance > 1.0f) // 타겟과의 거리 확인
        {
            Vector2 dis = target.position - transform.position; // 방향 벡터
            Vector2 dir = dis.normalized * Time.deltaTime * data.Speed; // 이동 방향
            transform.Translate(dir); // 이동

            if (dir.normalized.x != 0) // 방향에 따라 스프라이트 반전
            {
                sr.flipX = dir.normalized.x < 0 ? false : true;
            }
        }
        else // 타겟과 가까워졌을 때
        {
            attTimer += Time.deltaTime; // 공격 타이머 증가
            if (attTimer >= data.AttDelay) // 공격 지연 시간 확인
            {
                attTimer = 0; // 타이머 초기화
                target.GetComponent<Player>().Hit(data.Power); // 플레이어 공격
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision) // 충돌 처리
    {
        if (data.HP < 0) // 체력이 0 이하일 때
        {
            return;
        }
        if (collision.CompareTag("pBullet")) // 총알과 충돌
        {
            Bullet b = collision.GetComponent<Bullet>(); // 총알 가져오기
            data.HP -= GameManager.instance.P.data.Power * (1 - data.Defence); // 체력 감소
            Debug.Log($"현재 HP = {data.HP}"); // 현재 체력 로그
            state = State.Hit; // 맞은 상태로 변경
            data.HitDelay = 0.3f; // 맞은 후 지연 시간 설정
            sa.SetSprite(hit, 0.3f); // 맞았을 때 애니메이션 설정

            if (data.HP <= 0) // 체력이 0 이하일 때
            {
                GetComponent<Collider2D>().enabled = false; // 충돌체 비활성화
                tag = "Untagged"; // 태그 변경
                sa.SetSprite(dead, 0.1f, 1.0f, End); // 죽었을 때 애니메이션 설정
                GameManager.instance.killCount++; // 처치 카운트 증가
            }

            BulletPooling.Instance.AddpBullet(b); // 총알 풀에 추가
        }

        if (collision.CompareTag("Bible")) // 삽과 충돌
        {
            data.HP -= GameManager.instance.P.data.BiblePower * (1 - data.Defence); // 체력 감소
            Debug.Log($"현재 HP = {data.HP}"); // 현재 체력 로그

            state = State.Hit; // 맞은 상태로 변경
            data.HitDelay = 0.3f; // 맞은 후 지연 시간 설정
            sa.SetSprite(hit, 0.3f); // 맞았을 때 애니메이션 설정

            if (data.HP <= 0) // 체력이 0 이하일 때
            {
                GetComponent<Collider2D>().enabled = false; // 충돌체 비활성화
                tag = "Untagged"; // 태그 변경
                sa.SetSprite(dead, 0.3f, 1.0f, End); // 죽었을 때 애니메이션 설정
                GameManager.instance.killCount++; // 처치 카운트 증가
            }
        }

        if (collision.CompareTag("Trident")) // 삼지창과 충돌
        {
            data.HP -= (int)GameManager.instance.P.data.TridentPower; // 체력 감소
            Debug.Log($"현재 HP = {data.HP}"); // 현재 체력 로그

            state = State.Hit; // 맞은 상태로 변경
            data.HitDelay = 0.3f; // 맞은 후 지연 시간 설정
            sa.SetSprite(hit, 0.3f); // 맞았을 때 애니메이션 설정

            if (data.HP <= 0) // 체력이 0 이하일 때
            {
                GetComponent<Collider2D>().enabled = false; // 충돌체 비활성화
                tag = "Untagged"; // 태그 변경
                sa.SetSprite(dead, 0.3f, 1.0f, End); // 죽었을 때 애니메이션 설정
                GameManager.instance.killCount++; // 처치 카운트 증가
            }
        }

        if (collision.CompareTag("Water")) // 물과 충돌
        {
            data.HP -= GameManager.instance.P.data.WaterPower * (1 - data.Defence); // 체력 감소

            state = State.Hit; // 맞은 상태로 변경
            data.HitDelay = 0.1f; // 맞은 후 지연 시간 설정
            sa.SetSprite(hit, 0.2f); // 맞았을 때 애니메이션 설정

            Destroy(collision.gameObject); // 물 오브젝트 파괴
            waterPopVec = collision.transform.position; // 물 효과 위치 설정

            float aX = Mathf.Abs(waterPopVec.x); // X 좌표 절대값
            float aY = Mathf.Abs(waterPopVec.y); // Y 좌표 절대값

            if (data.HP <= 0) // 체력이 0 이하일 때
            {
                GetComponent<Collider2D>().enabled = false; // 충돌체 비활성화
                tag = "Untagged"; // 태그 변경
                sa.SetSprite(dead, 0.1f, 1.0f, End); // 죽었을 때 애니메이션 설정
                GameManager.instance.killCount++; // 처치 카운트 증가
            }
        }
    }

    private float damageInterval = 0.5f; // 피해 간격
    private float timeSinceLastDamage = 0f; // 마지막 피해 이후 시간

    private void OnTriggerStay2D(Collider2D collision) // 충돌 중일 때
    {
        if (collision.CompareTag("WaterPop")) // 물 효과와 충돌
        {
            timeSinceLastDamage += Time.deltaTime; // 시간 증가

            if (timeSinceLastDamage >= damageInterval) // 피해 간격 확인
            {
                data.HP -= GameManager.instance.P.data.WaterPopPower * (1 - data.Defence); // 체력 감소

                Debug.Log(data.HP); // 현재 체력 로그

                timeSinceLastDamage = 0f; // 시간 초기화

                state = State.Hit; // 맞은 상태로 변경
                data.HitDelay = 0.1f; // 맞은 후 지연 시간 설정
                sa.SetSprite(hit, 0.1f); // 맞았을 때 애니메이션 설정

                if (data.HP <= 0) // 체력이 0 이하일 때
                {
                    GetComponent<Collider2D>().enabled = false; // 충돌체 비활성화
                    tag = "Untagged"; // 태그 변경
                    sa.SetSprite(dead, 0.1f, 1.0f, End); // 죽었을 때 애니메이션 설정
                    GameManager.instance.killCount++; // 처치 카운트 증가
                }
            }
        }
    }

    void End() // 종료 처리
    {
        int expIndex = data.Level <= 2 ? 0 : data.Level <= 5 ? Random.Range(0, 2) : Random.Range(0, 3); // 경험치 인덱스 결정
        Gold gc = ItemPooling.Instance.GetPGoldCoin(); // 골드 코인 가져오기
        gc.transform.position = transform.position; // 위치 설정
        gc.transform.rotation = transform.rotation; // 회전 설정 

        Destroy(gameObject); // 보스 오브젝트 파괴
        GameManager.instance.UI.bossHP.gameObject.SetActive(false); // 보스 HP UI 비활성화
    }
}

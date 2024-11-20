using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class BattleSceneManager : MonoBehaviour
{
    public static BattleSceneManager Instance; // 인스턴스

    void Start()
    {
        Instance = this;

        Camera.main.transform.position = new Vector3(-13.8f, 0.2f, -10f);
    }

    public IEnumerator MoveCamera(Vector3 destination) // 카메라 이동
    {
        // 현재 위치와 목표 위치 간의 거리 계산
        float distance = Vector3.Distance(Camera.main.transform.position, destination);

        // 카메라가 목표 위치에 도달할 때까지 이동
        while (distance > 0.01f)
        {
            // 카메라 위치 이동
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, destination, 25.0f * Time.deltaTime);

            // 다음 프레임 대기
            yield return null;

            // 거리 다시 계산
            distance = Vector3.Distance(Camera.main.transform.position, destination);
        }
    }

    public void FadeInLoadCharSelectScene() // FadeIn 
    {
        StartCoroutine(DoFadeInAndLoadCharSelectScene());
    }

    IEnumerator DoFadeInAndLoadCharSelectScene()
    {
        yield return StartCoroutine(Fader.Instance.FadeIn()); // FadeIn 후 캐릭터 선택 씬 로드
        SceneManager.LoadScene("CharacterSelect");
    }
}

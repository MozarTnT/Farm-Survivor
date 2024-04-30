using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class BattleSceneManager : MonoBehaviour
{
    public static BattleSceneManager Instance;

    Vector3 playerVector = new Vector3(-13.8f, 0.2f, -10f);
    Vector3 monsterVector = new Vector3(14.83f, 0.2f, -10f);
    void Start()
    {
        Instance = this;

        Camera.main.transform.position = new Vector3(-13.8f, 0.2f, -10f);
    }



    public IEnumerator MoveCamera(Vector3 destination) // 카메라 이동
    {
        // 현재 카메라의 위치와 목표 위치 사이의 거리를 계산합니다.
        float distance = Vector3.Distance(Camera.main.transform.position, destination);

        // 카메라를 목표 위치로 이동시키는 동안 반복합니다.
        while (distance > 0.01f)
        {
            // 현재 위치에서 목표 위치로 이동합니다.
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, destination, 25.0f * Time.deltaTime);

            // 다음 프레임까지 대기합니다.
            yield return null;

            // 카메라의 현재 위치와 목표 위치 사이의 거리를 다시 계산합니다.
            distance = Vector3.Distance(Camera.main.transform.position, destination);
        }
    }


    public void FadeInLoadCharSelectScene()
    {
        StartCoroutine(DoFadeInAndLoadCharSelectScene());
    }

    IEnumerator DoFadeInAndLoadCharSelectScene()
    {
        yield return StartCoroutine(Fader.Instance.FadeIn()); // FadeIn 코루틴이 끝날 때까지 기다립니다.
        SceneManager.LoadScene("CharacterSelect");
    }
}

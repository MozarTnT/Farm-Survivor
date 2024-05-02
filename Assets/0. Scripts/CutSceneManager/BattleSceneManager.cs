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



    public IEnumerator MoveCamera(Vector3 destination) // ī�޶� �̵�
    {
        // ���� ī�޶��� ��ġ�� ��ǥ ��ġ ������ �Ÿ��� ����մϴ�.
        float distance = Vector3.Distance(Camera.main.transform.position, destination);

        // ī�޶� ��ǥ ��ġ�� �̵���Ű�� ���� �ݺ��մϴ�.
        while (distance > 0.01f)
        {
            // ���� ��ġ���� ��ǥ ��ġ�� �̵��մϴ�.
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, destination, 25.0f * Time.deltaTime);

            // ���� �����ӱ��� ����մϴ�.
            yield return null;

            // ī�޶��� ���� ��ġ�� ��ǥ ��ġ ������ �Ÿ��� �ٽ� ����մϴ�.
            distance = Vector3.Distance(Camera.main.transform.position, destination);
        }
    }


    public void FadeInLoadCharSelectScene()
    {
        StartCoroutine(DoFadeInAndLoadCharSelectScene());
    }

    IEnumerator DoFadeInAndLoadCharSelectScene()
    {
        yield return StartCoroutine(Fader.Instance.FadeIn()); // FadeIn �ڷ�ƾ�� ���� ������ ��ٸ��ϴ�.
        SceneManager.LoadScene("CharacterSelect");
    }
}

using DG.Tweening;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class BattleTalkManager : MonoBehaviour
{
    public static BattleTalkManager Instance;
    public class DialogueData // 대화 데이터 클래스
    {
        public int scene; // 장면
        public int index; // 인덱스
        public string name; // 이름
        public DialogueLine[] lines; // 대화 줄 배열

        [System.Serializable]
        public class DialogueLine // 대화 줄 클래스
        {
            public string line; // 대화 내용
        }
    }

    private DialogueData dialogueData = new DialogueData();

    [SerializeField] private GameObject chattingObject;
    [SerializeField] private Text talkText;

    private float delay = 0.07f;
    private string text;
    private int chattingIndex = 0;
    public int ChattingIndex
    {
        get { return chattingIndex; }
        set
        {
            chattingIndex = value;
        }
    }
    private bool clickAble = false;

    Vector3 playerVector = new Vector3(-13.8f, 0.2f, -10f);
    Vector3 monsterVector = new Vector3(14.83f, 0.2f, -10f);

    void Start()
    {
        // 인스턴스 설정
        Instance = this;

        // 캐릭터 이동 불가
        CutSceneCharacterManager.Instance.canMove = false;

        // 대화창 위치 이동
        chattingObject.transform.DOMoveY(88f, 0.5f).SetDelay(0.5f).SetEase(Ease.Linear);

        // JSON 파일 경로
        string filePath = "Assets/Resources/Json/battletalk.json";

        // JSON 파일 읽기
        string jsonString = File.ReadAllText(filePath);

        // JSON 파싱
        JSONNode json = JSON.Parse(jsonString);

        // DialogueData 객체 초기화
        dialogueData.scene = json["scene"].AsInt;
        dialogueData.index = json["index"].AsInt;
        dialogueData.name = json["name"];

        // lines 배열 초기화
        JSONArray linesArray = json["lines"].AsArray;
        dialogueData.lines = new DialogueData.DialogueLine[linesArray.Count];
        for (int i = 0; i < linesArray.Count; i++)
        {
            dialogueData.lines[i] = new DialogueData.DialogueLine();
            dialogueData.lines[i].line = linesArray[i]["line"];
        }

        // lines 배열 출력
        foreach (JSONNode lineNode in linesArray)
        {
            string line = lineNode["line"];
            Debug.Log(line);
        }

        // 텍스트 초기화
        talkText.text = "";
        text = dialogueData.lines[chattingIndex].line;
        text = text.Replace("\\n", "\n");

        // 텍스트 출력 시작
        StartCoroutine(textPrint(delay));
    }

    void Update()
    {
        // Ŭ Ȯ
        if ((Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.B)) && clickAble && chattingIndex <= dialogueData.lines.Length)
        {
            NextText();
        }
    }

    IEnumerator textPrint(float d) // 텍스트 출력
    {
        int count = 0;

        while (count != text.Length)
        {
            if (count < text.Length)
            {
                talkText.text += text[count].ToString();
                count++;
            }
            yield return new WaitForSeconds(delay);
        }

        clickAble = true;
    }

    void NextText() // 다음 텍스트
    {
        clickAble = false;
        chattingIndex++;

        talkText.text = "";
        text = dialogueData.lines[chattingIndex].line;
        text = text.Replace("\\n", "\n");

        StartCoroutine(textPrint(delay));
        Debug.Log(chattingIndex);

        if (chattingIndex == 5)
        {
            StartCoroutine(BattleSceneManager.Instance.MoveCamera(monsterVector));
            chattingIndex = 6;
        }
        else if (chattingIndex >= 8 && chattingIndex <= 10)
        {
            StartCoroutine(BattleSceneManager.Instance.MoveCamera(playerVector));
        }
        else if (chattingIndex > 10)
        {
            ChattingDown();
            clickAble = false;
            BattleSceneManager.Instance.FadeInLoadCharSelectScene();
        }
    }

    void ChattingDown() // 대화창 내리기
    {
        chattingObject.transform.DOMoveY(-300f, 0.5f).SetEase(Ease.Linear);
    }

}

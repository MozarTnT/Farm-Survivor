using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using SimpleJSON;

public class TalkManager : MonoBehaviour
{
    public static TalkManager Instance; // 인스턴스
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

    private DialogueData dialogueData = new DialogueData(); // 대화 데이터 초기화

    [SerializeField] private GameObject chattingObject; // 대화 오브젝트
    [SerializeField] private Text talkText; // 대화 텍스트
    [SerializeField] GameObject speechBubble; // 말풍선 오브젝트

    private float delay = 0.07f; // 텍스트 출력 지연
    private string text; // 현재 대화 텍스트
    private int chattingIndex = 0; // 대화 인덱스
    private bool clickAble = false; // 클릭 가능 여부

    void Start()
    {
        Instance = this; // 인스턴스 설정

        CutSceneCharacterManager.Instance.canMove = false; // 캐릭터 이동 불가

        chattingObject.transform.DOMoveY(88f, 0.5f).SetDelay(0.5f).SetEase(Ease.Linear); // 대화창 위치 이동

        string filePath = "Assets/Resources/Json/talk.json"; // JSON 파일 경로

        string jsonString = File.ReadAllText(filePath); // JSON 파일 읽기

        JSONNode json = JSON.Parse(jsonString); // JSON 파싱

        // 대화 데이터 초기화
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

        // 대화 내용 출력
        foreach (JSONNode lineNode in linesArray)
        {
            string line = lineNode["line"];
            Debug.Log(line);
        }

        talkText.text = ""; // 텍스트 초기화
        text = dialogueData.lines[chattingIndex].line; // 현재 대화 텍스트 설정
        text = text.Replace("\\n", "\n"); // 줄바꿈 처리

        StartCoroutine(textPrint(delay)); // 텍스트 출력 시작
    }

    private void Update()
    {
        // 클릭 입력 처리
        if ((Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.B)) && clickAble && chattingIndex < dialogueData.lines.Length - 1)
        {
            NextText(); // 다음 대화로 이동
        }
    }

    IEnumerator textPrint(float d) // 텍스트 출력 코루틴
    {
        int count = 0;

        while (count != text.Length)
        {
            if (count < text.Length)
            {
                talkText.text += text[count].ToString(); // 텍스트 추가
                count++;
            }
            yield return new WaitForSeconds(delay); // 지연
        }

        clickAble = true; // 클릭 가능
    }

    void NextText() // 다음 텍스트
    {
        clickAble = false; // 클릭 불가
        chattingIndex++; // 인덱스 증가

        talkText.text = ""; // 텍스트 초기화
        text = dialogueData.lines[chattingIndex].line; // 다음 대화 텍스트 설정
        text = text.Replace("\\n", "\n"); // 줄바꿈 처리

        StartCoroutine(textPrint(delay)); // 텍스트 출력 시작
        Debug.Log(chattingIndex); // 인덱스 로그

        if (chattingIndex == 5)
        {
            Camera.main.DOShakePosition(3.0f, 1.0f); // 카메라 흔들기

            speechBubble.SetActive(true); // 말풍선 활성화
            speechBubble.transform.DOMoveY(5.0f, 0.1f).SetEase(Ease.Linear).SetDelay(0.3f).
                OnComplete(() => 
                {
                    speechBubble.transform.DOMoveY(3.6f, 0.1f).SetEase(Ease.Linear); // 말풍선 위치 이동
                }
                );

            chattingIndex = 6; // 인덱스 조정
        }
        else if (chattingIndex > 8)
        {
            ChattingDown(); // 대화창 내리기
            clickAble = false; // 클릭 불가
            CutSceneCharacterManager.Instance.canMove = true; // 캐릭터 이동 가능
            speechBubble.SetActive(false); // 말풍선 비활성화
        }
    }

    void ChattingDown() // 대화창 내리기
    {
        chattingObject.transform.DOMoveY(-300f, 0.5f).SetEase(Ease.Linear); // 대화창 위치 이동
    }
}

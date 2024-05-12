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
    public static TalkManager Instance;
    public class DialogueData // 대화 Json 파일 class
    {
        public int scene;
        public int index;
        public string name;
        public DialogueLine[] lines;

        [System.Serializable]
        public class DialogueLine
        {
            public string line;
        }
    }

    private DialogueData dialogueData = new DialogueData();

    [SerializeField] private GameObject chattingObject;
    [SerializeField] private Text talkText;
    [SerializeField] GameObject speechBubble;

    private float delay = 0.07f; // 한글자씩 글자 출력용 Delay
    private string text;
    private int chattingIndex = 0;
   
    private bool clickAble = false;
    void Start()
    {
        Instance = this;

        CutSceneCharacterManager.Instance.canMove = false; // 움직임 고정

        chattingObject.transform.DOMoveY(88f, 0.5f).SetDelay(0.5f).SetEase(Ease.Linear); // 채팅창 올리기

        string filePath = "Assets/Resources/Json/talk.json"; // json 파일 주소값

        string jsonString = File.ReadAllText(filePath); // 읽어오기

        JSONNode json = JSON.Parse(jsonString); // 파싱

        // DialogueData 객체 생성 후 값 할당
        dialogueData.scene = json["scene"].AsInt;
        dialogueData.index = json["index"].AsInt;
        dialogueData.name = json["name"];

        // lines 배열 처리
        JSONArray linesArray = json["lines"].AsArray;
        dialogueData.lines = new DialogueData.DialogueLine[linesArray.Count];
        for (int i = 0; i < linesArray.Count; i++)
        {
            dialogueData.lines[i] = new DialogueData.DialogueLine();
            dialogueData.lines[i].line = linesArray[i]["line"];
        }

        // lines 배열 순회하며 값 출력
        foreach (JSONNode lineNode in linesArray)
        {
            string line = lineNode["line"];
            Debug.Log(line);
        }

        talkText.text = "";
        text = dialogueData.lines[chattingIndex].line;
        text = text.Replace("\\n", "\n"); // \n 중복 인식 변환

        StartCoroutine(textPrint(delay)); // 한글자씩 출력
    }

    private void Update()
    {
        // 클릭 검사
        if ((Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.B)) && clickAble && chattingIndex < dialogueData.lines.Length - 1)
        {
            NextText(); // 다음 채팅 출력
        }
    }


    IEnumerator textPrint(float d) // 한 글자씩 출력
    {
        int count = 0;

        while (count != text.Length)
        {
            if(count < text.Length)
            {
                talkText.text += text[count].ToString();
                count++;
            }
            yield return new WaitForSeconds(delay);
        }

        clickAble = true;
    }

    void NextText() // 다음 지문으로 넘어감
    {
        clickAble = false;
        chattingIndex++;

        talkText.text = "";
        text = dialogueData.lines[chattingIndex].line;
        text = text.Replace("\\n", "\n");

        StartCoroutine(textPrint(delay));
        Debug.Log(chattingIndex);

        if(chattingIndex == 5)
        {
            Camera.main.DOShakePosition(3.0f, 1.0f);

            speechBubble.SetActive(true);
            speechBubble.transform.DOMoveY(5.0f, 0.1f).SetEase(Ease.Linear).SetDelay(0.3f).
                OnComplete(() => 
                {
                    speechBubble.transform.DOMoveY(3.6f, 0.1f).SetEase(Ease.Linear);
                }
                );

            chattingIndex = 6;
        }
        else if (chattingIndex > 8)
        {
            ChattingDown();
            clickAble = false;
            CutSceneCharacterManager.Instance.canMove = true;
            speechBubble.SetActive(false);
        }
        else
        {
            
        }
    }

    void ChattingDown() // 채팅창 내리기
    {
        chattingObject.transform.DOMoveY(-300f, 0.5f).SetEase(Ease.Linear);
    }
}

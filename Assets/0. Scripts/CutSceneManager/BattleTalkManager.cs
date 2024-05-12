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
    public class DialogueData // 대화 JSon 파일용 class
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
        Instance = this;

        CutSceneCharacterManager.Instance.canMove = false; // 움직임 고정

        chattingObject.transform.DOMoveY(88f, 0.5f).SetDelay(0.5f).SetEase(Ease.Linear); // 대화창 올리기

        string filePath = "Assets/Resources/Json/battletalk.json"; // Json 파일 주소값

        string jsonString = File.ReadAllText(filePath); // Json 파일 읽어오기

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
        text = text.Replace("\\n", "\n");

        StartCoroutine(textPrint(delay)); // 한글자씩 시간 간격 두고 프린트
    }

    void Update()
    {
        // 클릭 확인
        if ((Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.B)) && clickAble && chattingIndex <= dialogueData.lines.Length)
        {
            NextText();
        }
    }

    IEnumerator textPrint(float d) // 한 글자씩 출력
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

    void NextText() // 다음 지문으로 넘어감
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
            //chattingIndex = 9;
        }
        else if (chattingIndex > 10)
        {
            ChattingDown();
            clickAble = false;
            BattleSceneManager.Instance.FadeInLoadCharSelectScene();
        }
        else
        {

        }
    }

    void ChattingDown() // 대화 종료 후 채팅창 내리기
    {
        chattingObject.transform.DOMoveY(-300f, 0.5f).SetEase(Ease.Linear);
    }

}

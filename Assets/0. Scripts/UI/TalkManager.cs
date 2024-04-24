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
    public class DialogueData
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

    private float delay = 0.07f;
    private string text;
    private int chattingIndex = 0;
    public int ChattingIndex 
    {
        get  { return chattingIndex; } 
        set 
        {  
            chattingIndex = value; 

        }
    }
    private bool clickAble = false;
    public bool canMove = false;
    void Start()
    {

        Instance = this;

        canMove = false;

        chattingObject.transform.DOMoveY(88f, 0.5f).SetDelay(0.5f).SetEase(Ease.Linear);

        string filePath = "Assets/Resources/Json/talk.json";

        string jsonString = File.ReadAllText(filePath);

        JSONNode json = JSON.Parse(jsonString);

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

        Debug.Log(dialogueData.lines.Length);

        talkText.text = "";
        text = dialogueData.lines[chattingIndex].line;
        text = text.Replace("\\n", "\n");

        StartCoroutine(textPrint(delay));
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && clickAble && chattingIndex < dialogueData.lines.Length - 1)
        {
            NextText();
        }
        Debug.Log(clickAble);
    }


    IEnumerator textPrint(float d)
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

    void NextText()
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

        if (chattingIndex > 8)
        {
            ChattingDown();
            clickAble = false;
            canMove = true;
            speechBubble.SetActive(false);
        }
    }

    void ChattingDown()
    {
        chattingObject.transform.DOMoveY(-300f, 0.5f).SetEase(Ease.Linear);
    }


}

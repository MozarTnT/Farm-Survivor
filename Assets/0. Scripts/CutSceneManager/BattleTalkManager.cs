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
    public class DialogueData // ��ȭ JSon ���Ͽ� class
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

        CutSceneCharacterManager.Instance.canMove = false; // ������ ����

        chattingObject.transform.DOMoveY(88f, 0.5f).SetDelay(0.5f).SetEase(Ease.Linear); // ��ȭâ �ø���

        string filePath = "Assets/Resources/Json/battletalk.json"; // Json ���� �ּҰ�

        string jsonString = File.ReadAllText(filePath); // Json ���� �о����

        JSONNode json = JSON.Parse(jsonString); // �Ľ�

        // DialogueData ��ü ���� �� �� �Ҵ�
        dialogueData.scene = json["scene"].AsInt;
        dialogueData.index = json["index"].AsInt;
        dialogueData.name = json["name"];

        // lines �迭 ó��
        JSONArray linesArray = json["lines"].AsArray;
        dialogueData.lines = new DialogueData.DialogueLine[linesArray.Count];
        for (int i = 0; i < linesArray.Count; i++)
        {
            dialogueData.lines[i] = new DialogueData.DialogueLine();
            dialogueData.lines[i].line = linesArray[i]["line"];
        }

        // lines �迭 ��ȸ�ϸ� �� ���
        foreach (JSONNode lineNode in linesArray)
        {
            string line = lineNode["line"];
            Debug.Log(line);
        }

        talkText.text = "";
        text = dialogueData.lines[chattingIndex].line;
        text = text.Replace("\\n", "\n");

        StartCoroutine(textPrint(delay)); // �ѱ��ھ� �ð� ���� �ΰ� ����Ʈ
    }

    void Update()
    {
        // Ŭ�� Ȯ��
        if ((Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.B)) && clickAble && chattingIndex <= dialogueData.lines.Length)
        {
            NextText();
        }
    }

    IEnumerator textPrint(float d) // �� ���ھ� ���
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

    void NextText() // ���� �������� �Ѿ
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

    void ChattingDown() // ��ȭ ���� �� ä��â ������
    {
        chattingObject.transform.DOMoveY(-300f, 0.5f).SetEase(Ease.Linear);
    }

}

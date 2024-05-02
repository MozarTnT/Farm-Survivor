using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    public Texture2D fadeOutTexture; // �������� �ؽ���
    public float fadeSpeed = 0.8f; // ���̵� �ӵ�

    private int drawDeath = -10000;    // �׸��� ���� �������� �ؽ�ó�� �����Դϴ�.���ڰ� ������ �� ���� �������˴ϴ�.
    private float alpha = 1.0f;        // 0���� 1 ������ �ؽ�ó�� ���� ��
    private int fadeDir = -1;          // ���̵��ϴ� ���� : in = -1 �Ǵ� out = 1

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnGUI()
    {
        // ���̵� �ƿ� / ���� ������ ����, �ӵ� �� Time.deltatime�� ����Ͽ� �۾��� �� ������ ��ȯ�մϴ�.
        alpha += fadeDir * fadeSpeed * Time.deltaTime;
        // GUI.color�� 0�� 1 ������ ���� ���� ����ϱ� ������ 0�� 1 ������ ���� ������(Ŭ����)�մϴ�.
        alpha = Mathf.Clamp01(alpha);

        // GUI�� ������ �����մϴ�(�� ��� �ؽ�ó).��� ���� ���� �����ϰ� �����ǰ� ���Ĵ� ���� ������ �����˴ϴ�.
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);               // ���� ���� �����մϴ�.
        GUI.depth = drawDeath;                                                             // ���� �� �ؽ�ó�� �� ���� �������մϴ�(�������� �׷��� ��).
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);
    }
}

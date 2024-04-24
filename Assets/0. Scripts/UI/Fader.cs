using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    public Texture2D fadeOutTexture; // 오버레이 텍스쳐
    public float fadeSpeed = 0.8f; // 페이드 속도

    private int drawDeath = -10000;    // 그리기 계층 구조에서 텍스처의 순서입니다.숫자가 낮으면 맨 위에 렌더링됩니다.
    private float alpha = 1.0f;        // 0에서 1 사이의 텍스처의 알파 값
    private int fadeDir = -1;          // 페이드하는 방향 : in = -1 또는 out = 1

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnGUI()
    {
        // 페이드 아웃 / 알파 값에서 방향, 속도 및 Time.deltatime을 사용하여 작업을 초 단위로 변환합니다.
        alpha += fadeDir * fadeSpeed * Time.deltaTime;
        // GUI.color가 0과 1 사이의 알파 값을 사용하기 때문에 0과 1 사이의 수를 강제로(클램프)합니다.
        alpha = Mathf.Clamp01(alpha);

        // GUI의 색상을 설정합니다(이 경우 텍스처).모든 색상 값은 동일하게 유지되고 알파는 알파 변수로 설정됩니다.
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);               // 알파 값을 설정합니다.
        GUI.depth = drawDeath;                                                             // 검은 색 텍스처를 맨 위에 렌더링합니다(마지막에 그려야 함).
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);
    }
}

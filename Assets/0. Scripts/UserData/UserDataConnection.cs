using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserDataConnection : MonoBehaviour
{
    [SerializeField] private TMP_Text idtext;
    [SerializeField] private TMP_InputField pwtext;

    [SerializeField] private GameObject loginAnnouncement;
    [SerializeField] private Text loginMessage;
    [SerializeField] private Text loginMessageWhy;
    public class myParams
    {
        public string id { get; set; }
        public string pw { get; set; }
    }

    public class MyRequest
    {
        public string api { get; set; }
        public myParams @params { get; set; }
    }

    public async void CreateAccountOnClicked()
    {
        string str_id = idtext.text;
        string str_pw = pwtext.text;

        Debug.Log(str_id);
        Debug.Log(str_pw);

        await api_create_account(str_id, str_pw); // 비동기 완료
    }


    public async Task api_create_account(string id, string pw) // 비동기 대기 (백그라운드에서 우선적으로 시행되는 방식)
    {
        // POST할 URL
        string url = "http://ted-rpi4-dev.duckdns.org:62431/api/create_account";

        try
        {

            // 보낼 json 데이터 생성
            MyRequest postdata = new MyRequest()
            {
                api = "create_account",
                @params = new myParams()
                {
                    id = id,
                    pw = pw
                },
            };

            // json 데이터를 문자열로 변환
            string json = JsonConvert.SerializeObject(postdata);

            // JSON 데이터를 StringContent로 변환
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // HttpClient 인스턴스 생성
            using (HttpClient client = new HttpClient())
            {
                // POST 요청 보내기
                HttpResponseMessage response = await client.PostAsync(url, content);

                // 응답 확인
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Debug.Log("서버 응답:");
                    Debug.Log(responseBody);

                    loginAnnouncement.SetActive(true);
                    loginMessage.text = "계정 생성이 완료되었습니다.";
                    loginMessageWhy.text = "";
                }
                else
                {
                    Debug.Log("서버 오류: " + response.StatusCode);
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Debug.Log("서버 응답:");
                    Debug.Log(responseBody);

                    loginAnnouncement.SetActive(true);
                    loginMessage.text = "계정 생성이 실패하였습니다.";
                    //if(responseBody.Contains("Primary"))
                    //{
                    //    loginMessageWhy.text = "이미 존재하는 아이디입니다.";
                    //}
                    //else if (responseBody.Contains("too long"))
                    //{
                    //    loginMessageWhy.text = "아이디나 비밀번호가 너무 깁니다.";
                    //}
                    //else
                    //{
                    //    loginMessageWhy.text = "";

                    //}

                }



            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("에러 발생: " + ex.Message);
        }
    }
}

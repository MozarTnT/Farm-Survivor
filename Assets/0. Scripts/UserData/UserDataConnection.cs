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
using Newtonsoft.Json.Linq;
using static UserDataConnection;

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
        public int score { get; set; }
        public string token { get; set; }
    }

    myParams myparams;

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

    public async void LoginOnClicked()
    {
        string str_id = idtext.text;
        string str_pw = pwtext.text;

        Debug.Log(str_id);
        Debug.Log(str_pw);

        await api_login(str_id, str_pw); // 비동기 완료

       // Debug.Log(myparams.score);
       // Debug.Log(myparams.token);
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


    public async Task api_login(string id, string pw) // 비동기 대기 (백그라운드에서 우선적으로 시행되는 방식)
    {
        // POST할 URL
        string url = "http://ted-rpi4-dev.duckdns.org:62431/api/login";

        try
        {

            // 보낼 json 데이터 생성
            MyRequest postdata = new MyRequest()
            {
                api = "login",
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

                    JObject responseData = JObject.Parse(responseBody);
                    JToken paramsToken = responseData;
                    if (paramsToken != null)
                    {
                        // score 확인
                        JToken scoreToken = paramsToken["score"];
                        if (scoreToken != null)
                        {
                            if (int.TryParse(scoreToken.ToString(), out int scoreValue))
                            {
                                myparams.score = scoreValue;
                            }
                            else
                            {
                                // score를 정수로 변환할 수 없는 경우 예외 처리
                                Debug.Log("Score를 정수로 변환할 수 없습니다.");
                            }
                        }
                        else
                        {
                            // "score" 키가 없는 경우 예외 처리
                            Debug.Log("Score 키가 존재하지 않습니다.");
                        }

                        // token 확인
                        JToken tokenToken = paramsToken["token"];
                        if (tokenToken != null)
                        {
                            myparams.token = tokenToken.ToString();
                        }
                        else
                        {
                            // "token" 키가 없는 경우 예외 처리
                            Debug.Log("Token 키가 존재하지 않습니다.");
                        }
                    }
                    else
                    {
                        // "params" 키가 없는 경우 예외 처리
                        Debug.Log("Params 키가 존재하지 않습니다.");
                    }

                    Debug.Log($" Score = {myparams.score.ToString()}");
                    Debug.Log($" Token = {myparams.token}");

                    

                    loginMessage.text = "로그인이 완료되었습니다.";
                }
                else
                {
                    Debug.Log("서버 오류: " + response.StatusCode);
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Debug.Log("서버 응답:");
                    Debug.Log(responseBody);

                    loginMessage.text = "로그인에 실패하였습니다.";

                }


                loginAnnouncement.SetActive(true);

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("에러 발생: " + ex.Message);
        }
    }
}

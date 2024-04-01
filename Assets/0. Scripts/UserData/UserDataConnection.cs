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

        await api_create_account(str_id, str_pw); // �񵿱� �Ϸ�
    }

    public async void LoginOnClicked()
    {
        string str_id = idtext.text;
        string str_pw = pwtext.text;

        Debug.Log(str_id);
        Debug.Log(str_pw);

        await api_login(str_id, str_pw); // �񵿱� �Ϸ�

       // Debug.Log(myparams.score);
       // Debug.Log(myparams.token);
    }


    public async Task api_create_account(string id, string pw) // �񵿱� ��� (��׶��忡�� �켱������ ����Ǵ� ���)
    {
        // POST�� URL
        string url = "http://ted-rpi4-dev.duckdns.org:62431/api/create_account";

        try
        {

            // ���� json ������ ����
            MyRequest postdata = new MyRequest()
            {
                api = "create_account",
                @params = new myParams()
                {
                    id = id,
                    pw = pw
                },
            };

            // json �����͸� ���ڿ��� ��ȯ
            string json = JsonConvert.SerializeObject(postdata);

            // JSON �����͸� StringContent�� ��ȯ
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // HttpClient �ν��Ͻ� ����
            using (HttpClient client = new HttpClient())
            {
                // POST ��û ������
                HttpResponseMessage response = await client.PostAsync(url, content);

                // ���� Ȯ��
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Debug.Log("���� ����:");
                    Debug.Log(responseBody);

                    loginAnnouncement.SetActive(true);
                    loginMessage.text = "���� ������ �Ϸ�Ǿ����ϴ�.";
                    loginMessageWhy.text = "";
                }
                else
                {
                    Debug.Log("���� ����: " + response.StatusCode);
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Debug.Log("���� ����:");
                    Debug.Log(responseBody);

                    loginAnnouncement.SetActive(true);
                    loginMessage.text = "���� ������ �����Ͽ����ϴ�.";
                    //if(responseBody.Contains("Primary"))
                    //{
                    //    loginMessageWhy.text = "�̹� �����ϴ� ���̵��Դϴ�.";
                    //}
                    //else if (responseBody.Contains("too long"))
                    //{
                    //    loginMessageWhy.text = "���̵� ��й�ȣ�� �ʹ� ��ϴ�.";
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
            Console.WriteLine("���� �߻�: " + ex.Message);
        }
    }


    public async Task api_login(string id, string pw) // �񵿱� ��� (��׶��忡�� �켱������ ����Ǵ� ���)
    {
        // POST�� URL
        string url = "http://ted-rpi4-dev.duckdns.org:62431/api/login";

        try
        {

            // ���� json ������ ����
            MyRequest postdata = new MyRequest()
            {
                api = "login",
                @params = new myParams()
                {
                    id = id,
                    pw = pw
                },
            };

            // json �����͸� ���ڿ��� ��ȯ
            string json = JsonConvert.SerializeObject(postdata);

            // JSON �����͸� StringContent�� ��ȯ
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // HttpClient �ν��Ͻ� ����
            using (HttpClient client = new HttpClient())
            {
                // POST ��û ������
                HttpResponseMessage response = await client.PostAsync(url, content);

                // ���� Ȯ��
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Debug.Log("���� ����:");
                    Debug.Log(responseBody);

                    JObject responseData = JObject.Parse(responseBody);
                    JToken paramsToken = responseData;
                    if (paramsToken != null)
                    {
                        // score Ȯ��
                        JToken scoreToken = paramsToken["score"];
                        if (scoreToken != null)
                        {
                            if (int.TryParse(scoreToken.ToString(), out int scoreValue))
                            {
                                myparams.score = scoreValue;
                            }
                            else
                            {
                                // score�� ������ ��ȯ�� �� ���� ��� ���� ó��
                                Debug.Log("Score�� ������ ��ȯ�� �� �����ϴ�.");
                            }
                        }
                        else
                        {
                            // "score" Ű�� ���� ��� ���� ó��
                            Debug.Log("Score Ű�� �������� �ʽ��ϴ�.");
                        }

                        // token Ȯ��
                        JToken tokenToken = paramsToken["token"];
                        if (tokenToken != null)
                        {
                            myparams.token = tokenToken.ToString();
                        }
                        else
                        {
                            // "token" Ű�� ���� ��� ���� ó��
                            Debug.Log("Token Ű�� �������� �ʽ��ϴ�.");
                        }
                    }
                    else
                    {
                        // "params" Ű�� ���� ��� ���� ó��
                        Debug.Log("Params Ű�� �������� �ʽ��ϴ�.");
                    }

                    Debug.Log($" Score = {myparams.score.ToString()}");
                    Debug.Log($" Token = {myparams.token}");

                    

                    loginMessage.text = "�α����� �Ϸ�Ǿ����ϴ�.";
                }
                else
                {
                    Debug.Log("���� ����: " + response.StatusCode);
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Debug.Log("���� ����:");
                    Debug.Log(responseBody);

                    loginMessage.text = "�α��ο� �����Ͽ����ϴ�.";

                }


                loginAnnouncement.SetActive(true);

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("���� �߻�: " + ex.Message);
        }
    }
}

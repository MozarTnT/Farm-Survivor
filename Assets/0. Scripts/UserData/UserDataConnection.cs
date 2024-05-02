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
    public static UserDataConnection instance;

    [SerializeField] private TMP_Text idtext;
    [SerializeField] private TMP_InputField pwtext;

    [SerializeField] private GameObject loginAnnouncement;
    [SerializeField] private Text loginMessage;
    [SerializeField] private Text loginMessageWhy;

    public bool isLogin = false;

    //public class UserData
    //{
    //    public string UserID { get; set; }
    //    public string UserPW { get; set; }
    //    public string UserToken { get; set; }
    //    public int UserScore { get; set; }
    //}
    //public UserData userdata;

    public string userID;

    public class loginParams // create, login
    {
        public string id { get; set; }
        public string pw { get; set; }

    }

    public class loginRequest // create, login
    {
        public string api { get; set; }
        public loginParams @params { get; set; }
    }

    public class MyLoginResponse
    {
        public string api { get; set; }
        public int code { get; set; }

        public MyLoginResponseData data;

    }

    public class MyLoginResponseData
    {
        public string token { get; set; }
        public int score { get; set; }
        public string msg { get; set; }
       
    }
    public MyLoginResponseData myLoginResponseData;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
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

        myLoginResponseData = new MyLoginResponseData();

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
            loginRequest postdata = new loginRequest()
            {
                api = "create_account",
                @params = new loginParams()
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

                    loginMessage.text = "���� ������ �Ϸ�Ǿ����ϴ�.";
                    loginMessageWhy.text = "";
                }
                else
                {
                    Debug.Log("���� ����: " + response.StatusCode);
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Debug.Log("���� ����:");
                    Debug.Log(responseBody);

                    loginMessage.text = "���� ������ �����Ͽ����ϴ�.";
                }
                loginAnnouncement.SetActive(true);
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
            loginRequest postdata = new loginRequest()
            {
                api = "login",
                @params = new loginParams()
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

                    // JSON�� ��ü�� ������ȭ
                    MyLoginResponse responseData = JsonConvert.DeserializeObject<MyLoginResponse>(responseBody);

                    myLoginResponseData.token = responseData.data.token;
                    myLoginResponseData.score = responseData.data.score;
                    // �߰��� �޾ƿ� token�� score ���
                    userID = id;

                    Debug.Log("Given Score: " + responseData.data.score.ToString());
                    Debug.Log("Current Score: " + myLoginResponseData.score.ToString());


                    Debug.Log("Given Token: " + responseData.data.token);
                    Debug.Log("Current Token: " + myLoginResponseData.token);

                    Debug.Log(userID);

                    loginMessage.text = "�α��ο� �����Ͽ����ϴ�.";
                    isLogin = true;
                    loginAnnouncement.SetActive(true);

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
            Debug.Log("���� �߻�: " + ex.Message);
        }
    }
}

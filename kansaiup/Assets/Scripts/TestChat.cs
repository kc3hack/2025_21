using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.UIElements;
public class TestChat : MonoBehaviour
{
    public GameObject inputf;
    private Text chatText;

    #region 必要なクラスの定義など
    [System.Serializable]
    public class MessageModel
    {
        public string role;
        public string content;
    }
    [System.Serializable]
    public class CompletionRequestModel
    {
        public string model;
        public List<MessageModel> messages;
    }

    [System.Serializable]

    //ChatGPTから返信されるデータ
    public class ChatGPTRecieveModel
    {
        public string id;
        public string @object;
        public int created;
        public Choice[] choices;
        public Usage usage;

        [System.Serializable]
        public class Choice
        {
            public int index;
            public MessageModel message;
            public string finish_reason;
        }

        [System.Serializable]
        public class Usage
        {
            public int prompt_tokens;
            public int completion_tokens;
            public int total_tokens;
        }
    }
    #endregion


    private MessageModel assistantModel = new()
    {
        role = "system",
        content = $@"今から関西周辺を舞台を題材にしたアスレチックゲームを行うので指定したことについて一言ぐらいで紹介してください。"
    };
    
    
    private readonly string apiKey = Environment.GetEnvironmentVariable("gpt-apiKey");
    private List<MessageModel> communicationHistory = new();

    void Start()
    {
        chatText = inputf.GetComponent<Text>();
        communicationHistory.Add(assistantModel);
        //MessageSubmit("はじめまして");
        MessageSubmit("今からゲームを始めます");
    }

    private void Communication(string newMessage, Action<MessageModel> result)
    {
        Debug.Log(newMessage);

//メッセージを追加
        communicationHistory.Add(new MessageModel()
        {
            role = "user",
            content = newMessage
        });

//JSONオプションの設定
        var apiUrl = "https://api.openai.com/v1/chat/completions";
        var jsonOptions = JsonUtility.ToJson(
            new CompletionRequestModel()
            {
                model = "gpt-3.5-turbo",
                messages = communicationHistory
            }, true);

//APIリクエストのヘッダーの設定
        var headers = new Dictionary<string, string>
            {
                {"Authorization", "Bearer " + apiKey},
                {"Content-type", "application/json"},
                {"X-Slack-No-Retry", "1"}
            };

//APIリクエストを送信
        var request = new UnityWebRequest(apiUrl, "POST")
        {
            uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonOptions)),
            downloadHandler = new DownloadHandlerBuffer()
        };

//APIリクエストにヘッダーを追加
        foreach (var header in headers)
        {
            request.SetRequestHeader(header.Key, header.Value);
        }

        var operation = request.SendWebRequest();

        operation.completed += _ =>
        {
            if (operation.webRequest.result == UnityWebRequest.Result.ConnectionError ||
                       operation.webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(operation.webRequest.error);
                throw new Exception();
            }
            else
            {
                var responseString = operation.webRequest.downloadHandler.text;
                var responseObject = JsonUtility.FromJson<ChatGPTRecieveModel>(responseString);
                communicationHistory.Add(responseObject.choices[0].message);
                chatText.text = responseObject.choices[0].message.content.ToString();
                
                Debug.Log(responseObject.choices[0].message.content);
            }
            request.Dispose();

        };
    }

    public void MessageSubmit(string sendMessage)
    {
        Communication(sendMessage, (result) =>
        {
            Debug.Log( result.content );
        });
    }
}
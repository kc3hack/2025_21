using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger: MonoBehaviour {
    public string SceneName;

    // Use this for initialization
    void Start () {
    }
    
    // Update is called once per frame
    void Update () {
        
    }

    public void ChangeScene(string SceneName)
    {
        Debug.Log("ロードするシーン名: " + SceneName);
        SceneManager.LoadScene(SceneName);
    }
}
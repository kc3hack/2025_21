using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPoint : MonoBehaviour
{
    private GameManager GM;
    [SerializeField] private string chat;
    public TestChat testChat;
    public Text gptText;
    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GM.CheckPointPos = transform.position;
            Debug.Log(GM.CheckPointPos);
            testChat.MessageSubmit(chat);
            StartCoroutine(TextReset());
        }
    }

    IEnumerator TextReset()
    {
        yield return new WaitForSeconds(5f);
        gptText.text ="";

    }
}

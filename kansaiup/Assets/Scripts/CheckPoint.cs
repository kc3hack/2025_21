using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CheckPoint : MonoBehaviourPunCallbacks
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

    void OnTriggerEnter(Collider other)
    {
        PhotonView playerPhotonView = other.GetComponent<PhotonView>(); // スコープ内で変数を定義
        if(other.gameObject.CompareTag("Player") && playerPhotonView.IsMine)
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

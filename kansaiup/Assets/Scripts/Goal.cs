using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Goal : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player"))
        {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.Disconnect();
            
            // タイトルシーンを読み込む
            PhotonNetwork.LoadLevel("Title");
        }
    }

}

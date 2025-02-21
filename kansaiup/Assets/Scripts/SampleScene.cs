using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class SampleScene : MonoBehaviourPunCallbacks
{
    private void Start() {
        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();
    }

    // マスターサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnConnectedToMaster() {
        // "Room"という名前のルームに参加する（ルームが存在しなければ作成して参加する）
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
    }

    // ゲームサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnJoinedRoom() {
        // ランダムな座標に自身のアバター（ネットワークオブジェクト）を生成する
        //var position = new Vector3(Random.Range(-2f, 2f), Random.Range(18f, 20f),Random.Range(-415f,-417f));
        var position = new Vector3(0,74,-178);
        PhotonNetwork.Instantiate("GamePlayer", position, Quaternion.identity);
    }

    // 退出ボタンが押された時に呼ばれる
public void ExitGameRoom()
{
    PhotonNetwork.LeaveRoom();
    PhotonNetwork.Disconnect();
    
    // タイトルシーンを読み込む
    PhotonNetwork.LoadLevel("Title");
}
}
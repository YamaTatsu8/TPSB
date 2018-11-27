using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Network : Photon.MonoBehaviour {

    private const string ROOM_NAME = "RoomA";

    GameObject go;

    public int ID
    {
        get
        {
            if(go != null)
            {
                return go.GetComponent<PhotonView>().ownerId;
            }
            return 0;
        }
    }

    void Start () {
        // PhotonServerへの接続
        PhotonNetwork.ConnectUsingSettings("1.0");
        PhotonNetwork.sendRate = 120;
        PhotonNetwork.sendRateOnSerialize = 120;

    }

    private void OnGUI()
    {
        // 状態の表示
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    /// <summary>
    /// Lobbyに入ったら呼ばれる関数
    /// </summary>
    void OnJoinedLobby()
    {
        Debug.Log("Lobbyに入りました");

        // ルームが一つもなかったらルームを作成、そうでなかったらルームに入る（指定のルーム）
        if(PhotonNetwork.GetRoomList().Length == 0)
        {
            CreateRoom();
        }
        else
        {
            PhotonNetwork.JoinRoom(ROOM_NAME);
        }
    }

    /// <summary>
    /// Roomに入ったら呼ばれる関数
    /// </summary>
    void OnJoinedRoom()
    {
        Debug.Log("Roomには入りました");
        go = PhotonNetwork.Instantiate("NetworkPlayer", Vector3.zero, Quaternion.identity, 0);
        go.name = "Player" + go.GetComponent<PhotonView>().ownerId.ToString();
    }

    /// <summary>
    /// ルームを作成する関数
    /// </summary>
    void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2; // ルームに入れる人数を2人に指定
        // 指定した名前、オプションのルームを作成
        PhotonNetwork.JoinOrCreateRoom(ROOM_NAME, roomOptions, null);

    }
}

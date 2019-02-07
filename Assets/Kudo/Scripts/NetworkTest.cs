using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkTest: Photon.MonoBehaviour {

    private const string ROOM_NAME = "RoomA";

    void Start () {
        // PhotonServerへの接続
        ConectNetwork();
    }

    private void OnGUI()
    {
        // 状態の表示
        GUILayout.Label("");
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    /// <summary>
    /// Lobbyに入ったら呼ばれる関数
    /// </summary>
    void OnJoinedLobby()
    {
        Debug.Log("Lobbyに入りました");

        //ルームが一つもなかったらルームを作成、そうでなかったらルームに入る（指定のルーム）
        if (PhotonNetwork.GetRoomList().Length == 0)
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

        PlayerInstantiate();
    }

    /// <summary>
    /// PhotonSeverに接続する関数
    /// </summary>
    public void ConectNetwork()
    {
        PhotonNetwork.ConnectUsingSettings("1.0");
        PhotonNetwork.sendRate = 120;
        PhotonNetwork.sendRateOnSerialize = 120;

    }
    /// <summary>
    /// ルームを作成する関数
    /// </summary>
    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2; // ルームに入れる人数を2人に指定
        // 指定した名前、オプションのルームを作成
        PhotonNetwork.JoinOrCreateRoom(ROOM_NAME,roomOptions, null);

    }

    /// <summary>
    /// playerをInstanceする関数
    /// </summary>
    public void PlayerInstantiate()
    {
        Vector3 pos = new Vector3(2, 21, -15);
        PlayerSystem playerSystem = GameObject.FindObjectOfType<PlayerSystem>();
        GameObject go = PhotonNetwork.Instantiate("Prefabs/PlayerModel/" + playerSystem.getChar(), Vector3.zero, Quaternion.identity, 0);
        if (go.GetComponent<PhotonView>().ownerId != 1)
        {
            //pos.x *= -1;
            pos.z *= -1;
        }

        go.transform.position = pos;
    }

    /// <summary>
    /// ルームを探してjoinする関数
    /// </summary>
    public void JoinRoom()
    {
        if (PhotonNetwork.GetRoomList().Length != 0)
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }
}

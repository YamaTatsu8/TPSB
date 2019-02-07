using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Network : Photon.MonoBehaviour {

    private bool _joinFlag = false;

    public bool JoinFlag
    {
        get
        {
            return _joinFlag;
        }
        set
        {
            _joinFlag = value;
        }
    }

    void Start () {
        // PhotonServerへの接続
        //PhotonNetwork.ConnectUsingSettings("1.0");
        //PhotonNetwork.sendRate = 120;
        //PhotonNetwork.sendRateOnSerialize = 120;

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

        //JoinRoom();
        // ルームが一つもなかったらルームを作成、そうでなかったらルームに入る（指定のルーム）
        //if(PhotonNetwork.GetRoomList().Length == 0)
        //{
        //    CreateRoom();
        //}
        //else
        //{
        //    PhotonNetwork.JoinRoom(ROOM_NAME);
        //}
    }

    /// <summary>
    /// Roomに入ったら呼ばれる関数
    /// </summary>
    void OnJoinedRoom()
    {
        Debug.Log("Roomには入りました");

        PlayerInstantiate();

        StageSystem system = GameObject.FindObjectOfType<StageSystem>();
        if (system == null)
        {
            GameObject go = PhotonNetwork.Instantiate("StageSystem", Vector3.zero, Quaternion.identity, 0);
            go.name = "StageSystem" + go.GetComponent<PhotonView>().ownerId;
        }

        _joinFlag = true;
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
        string text = "Room" + PhotonNetwork.GetRoomList().Length;
        //roomOptions.IsVisible = true;
        //roomOptions.IsOpen = true;
        roomOptions.MaxPlayers = 2; // ルームに入れる人数を2人に指定
        // 指定した名前、オプションのルームを作成
        PhotonNetwork.JoinOrCreateRoom(text, roomOptions, null);

    }

    /// <summary>
    /// playerをInstanceする関数
    /// </summary>
    public void PlayerInstantiate()
    {
        Vector3 pos = new Vector3(2, 21, -36);
        PlayerSystem playerSystem = GameObject.FindObjectOfType<PlayerSystem>();
        GameObject go = PhotonNetwork.Instantiate("Prefabs/PlayerModel/Network" + playerSystem.getChar(), Vector3.zero, Quaternion.identity, 0);
        if (go.GetComponent<PhotonView>().ownerId != 1)
        {
            //pos.x *= -1;
            pos.z *= -1;
        }

        go.transform.position = pos;
    }

    /// <summary>
    /// PlayerManagerをInstanceする関数
    /// </summary>
    public void PlayerManagerInstantiate()
    {
        NetworkPlayerReady playerReady = GameObject.FindObjectOfType<NetworkPlayerReady>();
        if(playerReady == null)
        {
            PhotonNetwork.Instantiate("NetworkPlayerManager", Vector3.zero, Quaternion.identity, 0);
        }
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

    public void CreateOrJoinRoom()
    {
        // ルームが一つもなかったらルームを作成、そうでなかったらルームに入る（指定のルーム）
        if (PhotonNetwork.GetRoomList().Length == 0)
        {
            CreateRoom();
        }
        else
        {
            PhotonNetwork.JoinRandomRoom();
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
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
=======

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
>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f
        }
    }

    void Start () {
        // PhotonServerへの接続
<<<<<<< HEAD
        //PhotonNetwork.ConnectUsingSettings("1.0");
        //PhotonNetwork.sendRate = 120;
        //PhotonNetwork.sendRateOnSerialize = 120;
=======
        PhotonNetwork.ConnectUsingSettings("1.0");
        PhotonNetwork.sendRate = 120;
        PhotonNetwork.sendRateOnSerialize = 120;
>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f

    }

    private void OnGUI()
    {
        // 状態の表示
<<<<<<< HEAD
        GUILayout.Label("");
=======
>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    /// <summary>
    /// Lobbyに入ったら呼ばれる関数
    /// </summary>
    void OnJoinedLobby()
    {
        Debug.Log("Lobbyに入りました");

<<<<<<< HEAD
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
=======
        // ルームが一つもなかったらルームを作成、そうでなかったらルームに入る（指定のルーム）
        if(PhotonNetwork.GetRoomList().Length == 0)
        {
            CreateRoom();
        }
        else
        {
            PhotonNetwork.JoinRoom(ROOM_NAME);
        }
>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f
    }

    /// <summary>
    /// Roomに入ったら呼ばれる関数
    /// </summary>
    void OnJoinedRoom()
    {
        Debug.Log("Roomには入りました");
<<<<<<< HEAD

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
        InputField inputName = GameObject.Find("RoomNameInput").GetComponent<InputField>();
        string text = inputName.text;
        if (text == "")
        {
            text = "Room" + PhotonNetwork.GetRoomList().Length;
        }
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;
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
        PhotonNetwork.Instantiate("NetworkPlayerManager", Vector3.zero, Quaternion.identity, 0);
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
=======
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
>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f
}

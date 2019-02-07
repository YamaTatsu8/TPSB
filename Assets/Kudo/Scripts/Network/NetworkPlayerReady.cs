using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NetworkPlayerReady : MonoBehaviour {

    private bool _isRedey = false;

    private PhotonView _photonView;
    private int _id = 0;

    private bool _isNextScene = false;
    private bool[] _playerFlag = new bool[2];

    private static NetworkPlayerReady _playerManager;            //　複数生成されないようのオブジェクト

    public bool ReadyFlag
    {
        get
        {
            return _isRedey;
        }
        set
        {
            _isRedey = value;
        }
    }

    public bool NextSceneFlag
    {
        get
        {
            return _isNextScene;
        }
        set
        {
            _isNextScene = value;
        }
    }

    public int ID
    {
        get
        {
            return _id;
        }
    }

    private void OnGUI()
    {
        // 状態の表示
        GUILayout.Label("");
        GUILayout.Label("");
        GUILayout.Label("");
        GUILayout.Label("");

    }

    void Awake () {

        _photonView = GetComponent<PhotonView>();

        _id = _photonView.ownerId;

        //　シーンオブサーバーが１つしか存在しないようにする
        if (_playerManager == null)
        {
            _playerManager = FindObjectOfType<NetworkPlayerReady>() as NetworkPlayerReady;
            DontDestroyOnLoad(_playerManager);
        }

    }

    void Update () {
		
        if(!_photonView.isMine)
        {
            return;
        }

        int flagCnt = 0;
        for(int i = 0; i < PhotonNetwork.playerList.Length; i++)
        {
            GameObject manager = GameObject.Find("NetworkPlayerManager");
            switch (i)
            {
                case 0:
                    if(manager != null && manager.GetComponent<NetworkPlayerReady>().ID == 1)
                    {
                        _playerFlag[i] = manager.GetComponent<NetworkPlayerReady>().ReadyFlag;
                    }
                    break;
                case 1:
                    if (manager != null && manager.GetComponent<NetworkPlayerReady>().ID == 2)
                    {
                        _playerFlag[i] = manager.GetComponent<NetworkPlayerReady>().ReadyFlag;
                    }
                    break;
                default:
                    break;
            }

            if(_playerFlag[i])
            {
                flagCnt++;
            }

            if(flagCnt >= 2)
            {
                _isNextScene = true;
            }
        }
	}

    public void SetPlayerReady(bool flag)
    {
        if (!_photonView.isMine)
        {
            return;
        }

        var properties = new ExitGames.Client.Photon.Hashtable();
        properties.Add("Flag", flag);

        PhotonNetwork.player.SetCustomProperties(properties);

    }

    public void OnPhotonPlayerPropertiesChanged(object[] i_playerAndUpdatedProps)
    {
        if (_isNextScene)
        {
            return;
        }

        var player = i_playerAndUpdatedProps[0] as PhotonPlayer;
        var properties = i_playerAndUpdatedProps[1] as ExitGames.Client.Photon.Hashtable;

        object flagvalue = null;
        if (properties.TryGetValue("Flag", out flagvalue))
        {
            bool receiveflag = (bool)flagvalue;
            var playerObjects = GameObject.FindGameObjectsWithTag("PlayerManager");
            var playerObject = playerObjects.FirstOrDefault(obj => obj.GetComponent<PhotonView>().ownerId == player.ID);

            playerObject.GetComponent<NetworkPlayerReady>().ReadyFlag = receiveflag;

            return;
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (_isNextScene)
        {
            return;
        }

        if (stream.isWriting)
        {
            //データの送信
            stream.SendNext(_playerFlag[0]);
            stream.SendNext(_playerFlag[1]);
            Debug.Log("データを送信しました！");
        }
        else
        {
            //データの受信
            bool player1Flag = (bool)stream.ReceiveNext();
            bool player2Flag = (bool)stream.ReceiveNext();
            this._playerFlag[0] = player1Flag;
            this._playerFlag[1] = player2Flag;
            Debug.Log("データを受信しました！");
        }
    }

}


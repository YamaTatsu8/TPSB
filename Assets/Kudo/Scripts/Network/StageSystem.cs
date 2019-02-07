using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StageSystem : Util.SingletonMonoBehaviour<StageSystem> {

    //ステージオブジェクト
    private GameObject _stage;

    //マテリアル
    private Material _skybox;

    [SerializeField]
    private bool _isCreate = false;
    [SerializeField]
    private string _stageName = "";
    [SerializeField]
    private static StageSystem _playerManager;            //　複数生成されないようのオブジェクト

    private PhotonView _photonView;

    public string StageName
    {
        get
        {
            return _stageName;
        }
        set
        {
            _stageName = value;
        }
    }

    public bool IsCreate
    {
        get
        {
            return _isCreate;
        }
        set
        {
            _isCreate = value;
        }
    }

    void Awake()
    {
        _photonView = GetComponent<PhotonView>();

        if (_playerManager == null)
        {
            _playerManager = FindObjectOfType<StageSystem>() as StageSystem;
            DontDestroyOnLoad(_playerManager);
        }

    }

    public void SetStageName()
    {
        if (_isCreate)
        {
            return;
        }
        GameObject obj = GameObject.Find("SceneManagerObject");
        StageSelectManager ssm = obj.GetComponent<NetworkSceneObserver>().GetStageSelectSceneData();

        _stageName = ssm.GetSelectStageName();

    }

    public void StageSpawn()
    {
        if(_isCreate)
        {
            return;
        }

        //オブサーバーを探す
        //GameObject obj = GameObject.Find("SceneManagerObject");
        GameObject obj = GameObject.Find("GameManager");
        //オブサーバーをゲットコンポーネント
        //StageSelectManager test = obj.GetComponent<NetworkSceneObserver>().GetStageSelectSceneData();
        string test = _stageName;

        _stage = PhotonNetwork.Instantiate("Prefabs/Stages/Network" + test, Vector3.zero, Quaternion.identity, 0);

        _skybox = (Material)Resources.Load("Material/" + test + "BackGround");

        RenderSettings.skybox = _skybox;

        _isCreate = true;
    }

    //public void SetStageName(string text)
    //{
    //    if (!_photonView.isMine)
    //    {
    //        return;
    //    }

    //    var properties = new ExitGames.Client.Photon.Hashtable();
    //    properties.Add("Flag", text);

    //    PhotonNetwork.player.SetCustomProperties(properties);

    //}

    //public void OnPhotonPlayerPropertiesChanged(object[] i_playerAndUpdatedProps)
    //{

    //    var player = i_playerAndUpdatedProps[0] as PhotonPlayer;
    //    var properties = i_playerAndUpdatedProps[1] as ExitGames.Client.Photon.Hashtable;

    //    object flagvalue = null;
    //    if (properties.TryGetValue("Flag", out flagvalue))
    //    {
    //        bool receiveflag = (bool)flagvalue;
    //        var playerObjects = GameObject.FindGameObjectsWithTag("StageSystem");
    //        var playerObject = playerObjects.FirstOrDefault(obj => obj.GetComponent<PhotonView>().ownerId == player.ID);

    //        playerObject.GetComponent<NetworkPlayerReady>().ReadyFlag = receiveflag;

    //        return;
    //    }
    //}

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        if (stream.isWriting)
        {
            //データの送信
            stream.SendNext(_stageName);
            Debug.Log("データを送信しました！");
        }
        else
        {
            //データの受信
            string text = (string)stream.ReceiveNext();
            this._stageName = text;
            Debug.Log("データを受信しました！");
        }
    }

}

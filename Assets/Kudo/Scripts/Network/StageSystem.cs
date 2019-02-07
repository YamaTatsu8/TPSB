using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Awake()
    {
        //　シーンオブサーバーが１つしか存在しないようにする
        if (_playerManager == null)
        {
            _playerManager = FindObjectOfType<StageSystem>() as StageSystem;
            DontDestroyOnLoad(_playerManager);
        }

    }

    public void SetStageName()
    {
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

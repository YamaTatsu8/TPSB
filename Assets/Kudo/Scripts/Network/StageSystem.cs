﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSystem : Util.SingletonMonoBehaviour<StageSystem> {

    //ステージオブジェクト
    private GameObject _stage;

    //マテリアル
    private Material _skybox;

    private bool _isCreate = false;

    private string _stageName = "";

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

}

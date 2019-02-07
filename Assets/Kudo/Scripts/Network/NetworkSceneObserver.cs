﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkSceneObserver : MonoBehaviour
{ 
    //　シーン
    enum SCENE_STATE
    {
        TitleScene,
        ModeMenuScene,
        RoomSetting,
        RoomCheck,
        CustomizeScene,
        StageSelectScene,
        GamePlayScene,
        ResultScene
    }
    private int _nowScene = (int)SCENE_STATE.TitleScene;    //　現在のシーン

    private TitleSceneManager _title;                       //　タイトルシーンのスクリプト
    private ModeMenuManager _modeMenu;                      // -モードメニューシーンのスクリプト
    private RoomChackerManager _roomJoin;                   // -クライアント側のルームチェックシーン
    private StageSelectManager _stageSelect;                //　ステージセレクトシーンのスクリプト
    private ResultSceneManager _result;                     //　リザルトシーンのスクリプト

    private static NetworkSceneObserver _sceneObservar;            //　複数生成されないようのオブジェクト

    // -ネットワーク
    private Network _network;

	// Use this for initialization
	void Start ()
    {
        Initialize();
	}

    /// <summary>
    /// 初期化処理
    /// </summary>
    public void Initialize()
    {
        _title = new TitleSceneManager();
        _title.Initialize();
        _modeMenu = new ModeMenuManager();
        _stageSelect = new StageSelectManager();
        _result = new ResultSceneManager();

        _network = new Network();

        //　シーンオブサーバーが１つしか存在しないようにする
        if (_sceneObservar == null)
        {
            _sceneObservar = FindObjectOfType<NetworkSceneObserver>() as NetworkSceneObserver;

            DontDestroyOnLoad(_sceneObservar);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        //　シーン更新
        SceneUpdate();
	}

    /// <summary>
    /// 現在のシーンによって更新を行う
    /// </summary>
    private void SceneUpdate()
    {
        switch (_nowScene)
        {
            //　タイトルシーンの更新処理
            case (int)SCENE_STATE.TitleScene:
                if (!_title.SceneUpdate())
                {
                    _nowScene = (int)SCENE_STATE.ModeMenuScene;
                    _modeMenu.Initialize();
                    ChangeScene(SCENE_STATE.ModeMenuScene.ToString());
                }
                break;
            // -モードメニューシーンの更新処理
            case (int)SCENE_STATE.ModeMenuScene:
                GameObject modeMenu = GameObject.Find("ModeMenu");
                if (modeMenu == null) { return; }
                else
                {
                    GameObject load = GameObject.Find("LoadCanvas");
                    if (load != null)
                    {
                        load.GetComponent<Loading>().FinalReset();
                    }
                }
                if (modeMenu.GetComponent<ModeMenu>().GetNextFlag())
                {
                    string selectName = modeMenu.GetComponent<ModeMenu>().SelectedSceneName();

                    Loading load = new Loading();
                    GameObject loadObj = load.CreateLoading();

                    if (selectName == SCENE_STATE.RoomSetting.ToString())
                    {
                        _network.ConectNetwork();

                        _nowScene = (int)SCENE_STATE.StageSelectScene;
                        _stageSelect.Initialize();
                        ChangeScene("Network" + SCENE_STATE.StageSelectScene.ToString());
                    }
                    if (selectName == SCENE_STATE.RoomCheck.ToString())
                    {
                        _network.ConectNetwork();

                        _nowScene = (int)SCENE_STATE.CustomizeScene;
                        ChangeScene("NetworkCustomizeWindow");
                    }
                    //if (selectName == SCENE_STATE.TrainingRoom.ToString())
                    //{
                    //    _nowScene = (int)SCENE_STATE.CustomizeWindow;
                    //    loadObj.GetComponent<Loading>().NextScene(SCENE_STATE.CustomizeWindow.ToString());
                    //    //ChangeScene("CustomizeWindow");
                    //}
                }

                //if (!_modeMenu.SceneUpdate())
                //{
                //    switch (_modeMenu.State)
                //    {
                //        case 0:
                //                _network.ConectNetwork();

                //                _nowScene = (int)SCENE_STATE.StageSelectScene;
                //                _stageSelect.Initialize();
                //                ChangeScene("Network" + SCENE_STATE.StageSelectScene.ToString());
                //            break;
                //        case 1:
                //            _network.ConectNetwork();

                //            _nowScene = (int)SCENE_STATE.CustomizeScene;
                //            ChangeScene("NetworkCustomizeWindow");
                //            break;
                //        case 2:
                //            _nowScene = (int)SCENE_STATE.CustomizeScene;
                //            ChangeScene("CustomizeWindow");
                //            break;
                //    }
                //}
                break;

            //　ルームセッティングの更新処理
            case (int)SCENE_STATE.RoomSetting:
                GameObject ssp_rs = GameObject.Find("StageSelectParent");
                if (ssp_rs.GetComponent<ChangeSprite>().ReturnToMenu())
                {

                    _nowScene = (int)SCENE_STATE.GamePlayScene;
                    ChangeScene("NetworkPlayScene");
                    _network.CreateRoom();
                }

                break;

            //// -ルームチェックシーンの更新処理
            //case (int)SCENE_STATE.RoomJoinScene:
            //    if(!_roomJoin.SceneUpdate())
            //    {
            //        switch (_roomJoin.State)
            //        {
            //            case 0:
            //                _nowScene = (int)SCENE_STATE.CustomizeScene;
            //                ChangeScene("NetworkCustomizeWindow");
            //                break;
            //            case 1:
            //                _nowScene = (int)SCENE_STATE.ModeMenuScene;
            //                _modeMenu.Initialize();
            //                ChangeScene(SCENE_STATE.ModeMenuScene.ToString());
            //                break;
            //            default:
            //                break;
            //        }
            //    }
            //    break;

            //　カスタマイズシーンの更新処理
            case (int)SCENE_STATE.CustomizeScene:
                GameObject cusObj = GameObject.Find("BackGround");

                if(cusObj.GetComponent<Equipment>().GetNextFlag())
                {
                    _nowScene = (int)SCENE_STATE.GamePlayScene;
                    ChangeScene("NetworkPlayScene");
                    _network.CreateRoom();

                    //_nowScene = (int)SCENE_STATE.GamePlayScene;
                    //ChangeScene("Test");
                }
                break;

            //　ステージセレクトシーンの更新処理
            case (int)SCENE_STATE.StageSelectScene:
                if(!_stageSelect.SceneUpdate())
                {
                    _nowScene = (int)SCENE_STATE.CustomizeScene;
                    ChangeScene("NetworkCustomizeWindow");
                }
                break;

            //　ゲームプレイシーン
            case (int)SCENE_STATE.GamePlayScene:
                GameObject gameManaObj = GameObject.Find("GameManager");

                StageSystem system = GameObject.FindObjectOfType<StageSystem>();
                system.SetStageName();

                if (gameManaObj.GetComponent<NetworkPlayScene>().getFlag())
                {
                    _nowScene = (int)SCENE_STATE.ResultScene;
                    _result.Initialize();
                    _result.SetBattleResult(gameManaObj.GetComponent<NetworkPlayScene>().getFightFlag());
                    ChangeScene(SCENE_STATE.ResultScene.ToString());
                }
                break;

            //　リザルトシーン
            case (int)SCENE_STATE.ResultScene:
                if (!_result.SceneUpdate())
                {
                    _nowScene = (int)SCENE_STATE.TitleScene;
                    _title.Initialize();
                    ChangeScene("Network" + SCENE_STATE.TitleScene.ToString());
                }
                break;
        }
    }
   
    /// <summary>
    /// 次のシーンへ遷移する
    /// </summary>
    /// <param name="sceneName">遷移したいScene名</param>
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //---------------------------------------------------------------------------------
    //　各シーンの情報を共有するGetter
    //---------------------------------------------------------------------------------
    /// <summary>
    /// タイトルシーンのデータ(publicのみ)を取得する権限を付与
    /// </summary>
    /// <returns>タイトルシーンの権限</returns>
    public TitleSceneManager GetTitleSceneData()
    {
        return _title;
    }
    /// <summary>
    /// ステージセレクトシーン(publicのみ)のデータを取得する権限を付与
    /// </summary>
    /// <returns>ステージセレクトシーンの権限</returns>
    public StageSelectManager GetStageSelectSceneData()
    {
        return _stageSelect;
    }
    /// <summary>
    /// リザルトシーンのデータ(publicのみ)を取得する権限を付与
    /// </summary>
    /// <returns>リザルトシーンの権限</returns>
    public ResultSceneManager GetResultSceneData()
    {
        return _result;
    }
}

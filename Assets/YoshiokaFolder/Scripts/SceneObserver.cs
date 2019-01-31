using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneObserver : MonoBehaviour
{ 
    //　シーン
    enum SCENE_STATE
    {
        TitleScene,
        ModeMenuScene,
        RoomSetting,
        RoomCheck,
        TrainingRoom,
        CustomizeScene,
        StageSelectScene,
        PlayScene,
        ResultScene
    }
    private int _nowScene = (int)SCENE_STATE.TitleScene;    //　現在のシーン

    private TitleSceneManager _title;                       //　タイトルシーンのスクリプト
    private StageSelectManager _stageSelect;                //　ステージセレクトシーンのスクリプト
    private ResultSceneManager _result;                     //　リザルトシーンのスクリプト

    private static SceneObserver _sceneObservar;            //　複数生成されないようのオブジェクト


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
        _stageSelect = new StageSelectManager();
        _result = new ResultSceneManager();

        //　シーンオブサーバーが１つしか存在しないようにする
        if (_sceneObservar == null)
        {
            _sceneObservar = FindObjectOfType<SceneObserver>() as SceneObserver;

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
                }
                break;

            //　モードメニューシーンの更新処理
            case (int)SCENE_STATE.ModeMenuScene:
                GameObject modeMenu = GameObject.Find("ModeMenu");
                if (modeMenu == null) { return; }
                else
                {
                    GameObject load = GameObject.Find("LoadCanvas");
                    load.GetComponent<Loading>().FinalReset();
                }
                if (modeMenu.GetComponent<ModeMenu>().GetNextFlag())
                {
                    string selectName = modeMenu.GetComponent<ModeMenu>().SelectedSceneName();

                    if (selectName == SCENE_STATE.RoomSetting.ToString())
                    {
                        _nowScene = (int)SCENE_STATE.RoomSetting;
                        ChangeScene(selectName);
                    }
                    if (selectName == SCENE_STATE.RoomCheck.ToString())
                    {
                        _nowScene = (int)SCENE_STATE.RoomCheck;
                        ChangeScene(selectName);
                    }
                    if (selectName == SCENE_STATE.TrainingRoom.ToString())
                    {
                        _nowScene = (int)SCENE_STATE.CustomizeScene;
                        ChangeScene("CustomizeWindow");
                    }
                }
                break;

            //　ルームセッティングの更新処理
            case (int)SCENE_STATE.RoomSetting:
                break;
            
            //　ルームチェックの更新処理
            case (int)SCENE_STATE.RoomCheck:
                break;

            //　トレーニングルームの更新処理
            case (int)SCENE_STATE.TrainingRoom:
                break;


            //　カスタマイズシーンの更新処理
            case (int)SCENE_STATE.CustomizeScene:
                GameObject cusObj = GameObject.Find("BackGround");

                if (cusObj == null) { return; }
                else
                {
                    GameObject load = GameObject.Find("LoadCanvas");
                    load.GetComponent<Loading>().FinalReset();
                }
                if (cusObj.GetComponent<Equipment>().GetNextFlag())
                {
                    _nowScene = (int)SCENE_STATE.TrainingRoom;
                    ChangeScene(SCENE_STATE.TrainingRoom.ToString());
                    //_nowScene = (int)SCENE_STATE.StageSelectScene;
                    //_stageSelect.Initialize();
                    //ChangeScene(SCENE_STATE.StageSelectScene.ToString());
                    //_nowScene = (int)SCENE_STATE.GamePlayScene;
                    //ChangeScene("Test");
                }
                break;                

            //　ステージセレクトシーンの更新処理
            case (int)SCENE_STATE.StageSelectScene:
                if(!_stageSelect.SceneUpdate())
                {
                    _nowScene = (int)SCENE_STATE.PlayScene;
                    ChangeScene(SCENE_STATE.PlayScene.ToString());
                }
                break;

            //　ゲームプレイシーン
            case (int)SCENE_STATE.PlayScene:
                GameObject gameManaObj = GameObject.Find("GameManager");
                if (gameManaObj.GetComponent<PlayScene>().getFlag())
                {
                    _nowScene = (int)SCENE_STATE.ResultScene;
                    _result.Initialize();
                    _result.SetBattleResult(gameManaObj.GetComponent<PlayScene>().getFightFlag());
                    ChangeScene(SCENE_STATE.ResultScene.ToString());
                }
                break;

            //　リザルトシーン
            case (int)SCENE_STATE.ResultScene:
                if (!_result.SceneUpdate())
                {
                    _nowScene = (int)SCENE_STATE.TitleScene;
                    _title.Initialize();
                    ChangeScene(SCENE_STATE.TitleScene.ToString());
                }
                break;

            default:
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

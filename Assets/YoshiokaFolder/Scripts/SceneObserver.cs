using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneObserver : MonoBehaviour
{ 
    //　シーン
    enum SCENE_STATE
    {
        TitleScene,
        CustomizeScene,
        StageSelectScene,
        GamePlayScene,
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

    //　初期化処理
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

    private void SceneUpdate()
    {
        switch (_nowScene)
        {
            //　タイトルシーンの更新処理
            case (int)SCENE_STATE.TitleScene:
                if (!_title.SceneUpdate())
                {
                    _nowScene = (int)SCENE_STATE.CustomizeScene;
                    ChangeScene("CustomizeWindow");
                }
                break;

            //　カスタマイズシーンの更新処理
            case (int)SCENE_STATE.CustomizeScene:
                GameObject cusObj = GameObject.Find("BackGround");

                if(cusObj.GetComponent<Equipment>().GetNextFlag())
                {
                    //_nowScene = (int)SCENE_STATE.StageSelectScene;
                    //_stageSelect.Initialize();
                    //ChangeScene(SCENE_STATE.StageSelectScene.ToString());
                    _nowScene = (int)SCENE_STATE.GamePlayScene;
                    ChangeScene("Test");
                }
                break;

            //　ステージセレクトシーンの更新処理
            case (int)SCENE_STATE.StageSelectScene:
                if(!_stageSelect.SceneUpdate())
                {
                    _nowScene = (int)SCENE_STATE.GamePlayScene;
                    ChangeScene("Test");
                }
                break;

            //　ゲームプレイシーン
            case (int)SCENE_STATE.GamePlayScene:
                GameObject gameManaObj = GameObject.Find("GameManager");

                if (gameManaObj.GetComponent<PlayScene>().getFlag())
                {
                    _nowScene = (int)SCENE_STATE.ResultScene;
                    _result.Initialize();
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
        }
    }
   

    //　指定されたシーンに遷移する
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

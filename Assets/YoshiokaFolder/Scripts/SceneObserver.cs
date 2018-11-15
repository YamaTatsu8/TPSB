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
        PlayScene,
        ResultScene
    }
    private int _nowScene = (int)SCENE_STATE.TitleScene;    //　現在のシーン

    private TitleSceneManager _title;
    private PlaySceneManager _play;

    private GameObject _fadeObj;            //　フェードに使用するパネル

	// Use this for initialization
	void Start ()
    {
        Initialize();
	}

    public void Initialize()
    {
        _title = new TitleSceneManager();
        _title.Initialize();
        _play = new PlaySceneManager();
        _play.Initialize();
        DontDestroyOnLoad(gameObject);
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
            case (int)SCENE_STATE.TitleScene:
                _title.SceneUpdate();
                if (_title.EndedScene())
                {
                    _nowScene = (int)SCENE_STATE.PlayScene;
                    _play.Initialize();
                    ChangeScene(SCENE_STATE.PlayScene.ToString());
                }
                break;

            case (int)SCENE_STATE.PlayScene:
                _play.SceneUpdate();
                if(_play.EndedScene())
                {
                    _nowScene = (int)SCENE_STATE.TitleScene;
                    _title.Initialize();
                    ChangeScene(SCENE_STATE.TitleScene.ToString());
                }
                break;

            case (int)SCENE_STATE.ResultScene:
                break;
        }
    }
   

    //　指定されたシーンに遷移する
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySceneManager : MonoBehaviour
{
    private GameController _controller;     //　ゲームコントローラー
    private GameObject _fadeObj;

    private bool _isStartFade = false;      //　フェードが開始されているかチェックするフラグ
    private bool _isEndedScene = false;

    // Use this for initialization
    void Start ()
    { 
        Initialize();	
	}

    public void Initialize()
    {
        _controller = GameController.Instance;
        _isStartFade = false;
        _isEndedScene = false;
    }

    // Update is called once per frame
    void Update()
    {
        // シーン更新
        SceneUpdate();
    }

    //　コントローラー更新処理
    private void ControllerUpdate()
    {
        _controller.ControllerUpdate();

        if (_controller.ButtonDown(Button.A) && !_isStartFade)
        {
            Fade fade = new Fade();
            _fadeObj = fade.CreateFade();
            _fadeObj.GetComponentInChildren<Fade>().FadeOut();
            _isStartFade = true;
        }
    }

    //　シーン更新処理
    public void SceneUpdate()
    {
        ControllerUpdate();

        //　ステージが遷移されていたら
        if (_fadeObj == null)
        {
            Fade fade = new Fade();
            _fadeObj = fade.CreateFade();
            _fadeObj.GetComponentInChildren<Fade>().FadeIn();
        }
        //　フェードが終了していたらシーンを変更する
        if (_fadeObj.GetComponentInChildren<Fade>().isCheckedFadeOut() && _isStartFade)
        {
            _isStartFade = false;
            _isEndedScene = true;
        }
    }

    public bool EndedScene()
    {
        return _isEndedScene;
    }
}

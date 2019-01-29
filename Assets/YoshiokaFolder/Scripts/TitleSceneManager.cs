using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneManager : MonoBehaviour
{
    private GameController _controller;     //　ゲームコントローラー
    private GameObject _fadeObj;            //　fadeobj

    private bool _isStartFade;              //　フェードが開始されているかチェックするフラグ

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
        _controller = GameController.Instance;
        _isStartFade = false;
    }

    // Update is called once per frame
    void Update ()
    {
        // シーン更新
        SceneUpdate();
    }

    /// <summary>
    /// シーンの更新処理
    /// </summary>
    /// <returns>true:シーンが終了,シーンが終了していない</returns>
    public bool SceneUpdate()
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
        GameObject loadObj = GameObject.Find("LoadCanvas");
        if (_fadeObj.GetComponentInChildren<Fade>().isCheckedFadeOut() && _isStartFade)
        {
            _isStartFade = false;

            loadObj.GetComponent<Loading>().NextScene();
        }
        if (loadObj.GetComponent<Loading>().IsFinished())
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// 入力関係更新処理
    /// </summary>
    private void ControllerUpdate()
    {
        //　シーン遷移が開始されてた場合以下の処理を行わない
        if (_isStartFade)
        {
            return;
        }

        _controller.ControllerUpdate();

        //　ボタンが押されたら
        if ((_controller.ButtonDown(Button.A))
            || (_controller.ButtonDown(Button.B))
            || (_controller.ButtonDown(Button.X))
            || (_controller.ButtonDown(Button.Y))
            || (_controller.ButtonDown(Button.START)))
        {
            //　フェードアウトを開始する
            if (_fadeObj == null)
            {
                Fade fade = new Fade();
                _fadeObj = fade.CreateFade();
            }
            _fadeObj.GetComponentInChildren<Fade>().FadeOut();
            _isStartFade = true;
        }
    }
}

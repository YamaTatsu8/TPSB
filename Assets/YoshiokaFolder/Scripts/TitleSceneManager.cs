﻿using System.Collections;
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

    //　シーン更新処理
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
        if (_fadeObj.GetComponentInChildren<Fade>().isCheckedFadeOut() && _isStartFade)
        {
            _isStartFade = false;
            return false;
        }
        return true;
    }

    //　コントローラー更新処理
    private void ControllerUpdate()
    {
        _controller.ControllerUpdate();

        if (_controller.ButtonDown(Button.A) && !_isStartFade)
        {
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

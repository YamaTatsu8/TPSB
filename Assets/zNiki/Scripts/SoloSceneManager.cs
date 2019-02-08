using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoloSceneManager : MonoBehaviour {

    private GameController _controller;     //　ゲームコントローラー
    public bool _isFinish;

    public bool _isWin = false;

    private GameObject _player;

    private GameObject _boss;
    private GameObject _fadeObj;            //　フェード
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

        _player = GameObject.FindGameObjectWithTag("Player");

        _boss = GameObject.FindGameObjectWithTag("Enemy");
    }

    // Update is called once per frame
    void Update ()
    {
        // シーン更新
        SceneUpdate();
    }

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
        if (_boss.GetComponent<EnemyController>()._enemyHealth == 0)
        {
            _isWin = true;

            _isFinish = true;
        }
        else if (_player.GetComponent<Status>().getHP() == 0)
        {
            _isFinish = true;
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

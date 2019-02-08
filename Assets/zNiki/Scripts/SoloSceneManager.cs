using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoloSceneManager : MonoBehaviour {

    private GameController _controller;     //　ゲームコントローラー
    public bool _isFinish = false;

    public bool _isWin = false;

    private GameObject _player;

    private GameObject _boss;
    private GameObject _fadeObj;            //　フェード
    private bool _isStartFade;              //　フェードが開始されているかチェックするフラグ
    private Material _stageSkybox;          //　スカイボックス保存用
    private GameObject _tank;

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

    public bool SceneUpdate()
    {
        ControllerUpdate();

        //　ステージが遷移されていたら
        if (_fadeObj == null)
        {
            Fade fade = new Fade();
            _fadeObj = fade.CreateFade();
            _fadeObj.GetComponentInChildren<Fade>().FadeIn();
            _player = GameObject.FindGameObjectWithTag("Player");
            _tank = GameObject.Find("Enemy");
            _boss = GameObject.FindGameObjectWithTag("Enemy");

            _stageSkybox = (Material)Instantiate(Resources.Load("Material/" + "Stage2" + "BackGround"));
            RenderSettings.skybox = _stageSkybox;
        }
        //　フェードが終了していたらシーンを変更する
        if (_fadeObj.GetComponentInChildren<Fade>().isCheckedFadeOut() && _isStartFade)
        {
            _isStartFade = false;
            _isFinish = true;
            return false;
        }
        if (_boss.GetComponent<Status>().getHP() <= 0)
        {
            _isWin = true;
            //　フェードアウトを開始する
            if (_fadeObj == null)
            {
                Fade fade = new Fade();
                _fadeObj = fade.CreateFade();
            }
            _fadeObj.GetComponentInChildren<Fade>().FadeOut();
            _isStartFade = true;
            Destroy(_tank);
        }
        if (_player.GetComponent<Status>().getHP() <= 0)
        {
            //　フェードアウトを開始する
            if (_fadeObj == null)
            {
                Fade fade = new Fade();
                _fadeObj = fade.CreateFade();
            }
            _fadeObj.GetComponentInChildren<Fade>().FadeOut();
            _isStartFade = true;
            Destroy(_player);
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
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectManager : MonoBehaviour
{
    private GameObject _fadeObj;            //　フェード
    private GameController _controller;     //　ゲームコントローラー
    private string _stageName;

    private bool _isStartFade = false;      //　True:フェード開始、False:フェード終了中

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

        _stageName = "Stage1";
    }

    /// <summary>
    /// ヒエラルキー関係の初期化処理
    /// </summary>
    private void localInitialize()
    {
        //　フェード開始
        Fade fade = new Fade();
        _fadeObj = fade.CreateFade();
        _fadeObj.GetComponentInChildren<Fade>().FadeIn();
    }

	// Update is called once per frame
	void Update ()
    {
        //　シーン更新
        SceneUpdate();	
	}

    /// <summary>
    /// シーンの更新処理
    /// </summary>
    /// <returns>true:シーンが終了した,false:シーンが終了していない</returns>
    public bool SceneUpdate()
    {
        ControllerUpdate();

        //　シーンが遷移されていたら
        if (_fadeObj == null)
        {
            localInitialize();
        }
        //　フェードが終了していたらシーンを変更する
        if (_fadeObj.GetComponentInChildren<Fade>().isCheckedFadeOut() && _isStartFade)
        {
            _isStartFade = false;
            return false;
        }
        return true;
    }

    /// <summary>
    /// 入力関係の更新処理
    /// </summary>
    private void ControllerUpdate()
    {
        //　シーン遷移が開始されてた場合以下の処理を行わない
        if (_isStartFade)
        {
            return;
        }

        //　コントローラー更新
        _controller.ControllerUpdate();

        //　Aボタンが押されたらフェードアウトを開始する
        if (_controller.ButtonDown(Button.A))
        {
            //　フェード開始
            Fade fade = new Fade();
            _fadeObj = fade.CreateFade();
            _fadeObj.GetComponentInChildren<Fade>().FadeOut();
            _isStartFade = true;
        }
    }

    //---------------------------------------------------------------------------------
    //　Getter
    //---------------------------------------------------------------------------------
    /// <summary>
    /// 選択されているステージ名の取得
    /// </summary>
    /// <returns></returns>
    public string GetSelectStageName()
    {
        return _stageName;
    }

    public void SetSelectStageName(string name)
    {
        _stageName = name;
    }
}

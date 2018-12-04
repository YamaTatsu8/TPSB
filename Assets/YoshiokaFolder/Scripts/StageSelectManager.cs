using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectManager : MonoBehaviour
{
    //　定数
    private const int MAX_STAGE = 5;        //　最大ステージ数
    private const int MIN_STAGE = 1;        //　最小ステージ数

    private GameController _controller;     //　ゲームコントローラー
    private GameObject _fadeObj;            //　フェード

    private GameObject _stage;              //　ステージ管理者
    private string _stageName;              //　ステージ名
    private int _stageNumber;               //　ステージ番号

    private GameObject _cursor;             //　カーソル
    private string _cursorName;             //　カーソルネーム

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

        _stageName = "Stage";
        _stageNumber = 1;

        _cursorName = "StageImage";
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

        _stage = GameObject.Find("Stage");
        _cursor = GameObject.Find("CursorImage");

        GameObject obj = GameObject.Find(_cursorName + _stageNumber.ToString());
        _cursor.transform.position = obj.transform.position;
        obj.GetComponent<Animator>().SetBool("isStart", true);
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

        //　上十字キー及び上左スティック
        if ((_controller.CheckDirectionOnce(Direction.Front, StickType.LEFTSTICK)) ||
            (_controller.CheckDirectionOnce(Direction.Front, StickType.CLOSS)))
        {
            //　カーソルが一番上を選択していたら一番下にする
            if (_stageNumber == MIN_STAGE)
            {
                GameObject beforeObj = GameObject.Find(_cursorName + _stageNumber.ToString());
                beforeObj.GetComponent<Animator>().SetBool("isStart", false);
                _stageNumber = MAX_STAGE;
            }
            else
            {//　カーソルを一つ上にずらす
                GameObject beforeObj = GameObject.Find(_cursorName + _stageNumber.ToString());
                beforeObj.GetComponent<Animator>().SetBool("isStart", false);
                _stageNumber--;
            }
            //　描画するステージを変更する
            _stage.GetComponent<StagePreview>().SetStage(_stageName + _stageNumber.ToString());

            //　カーソルを上にずらす
            GameObject obj = GameObject.Find(_cursorName + _stageNumber.ToString());
            _cursor.transform.position = obj.transform.position;
            obj.GetComponent<Animator>().SetBool("isStart", true);
        }
        //　下十字キー及び下左スティック
        if ((_controller.CheckDirectionOnce(Direction.Back, StickType.LEFTSTICK)) ||
            (_controller.CheckDirectionOnce(Direction.Back, StickType.CLOSS)))
        {
            //　カーソルが一番下を選択していたら一番上にする
            if (_stageNumber == MAX_STAGE)
            {
                GameObject beforeObj = GameObject.Find(_cursorName + _stageNumber.ToString());
                beforeObj.GetComponent<Animator>().SetBool("isStart", false);
                _stageNumber = MIN_STAGE;
            }
            else
            {//　カーソルを下にずらす
                GameObject beforeObj = GameObject.Find(_cursorName + _stageNumber.ToString());
                beforeObj.GetComponent<Animator>().SetBool("isStart", false);
                _stageNumber++;
            }
            //　描画するステージを変更する
            _stage.GetComponent<StagePreview>().SetStage(_stageName + _stageNumber.ToString());

            //　カーソルを下にずらす
            GameObject obj = GameObject.Find(_cursorName + _stageNumber.ToString());
            _cursor.transform.position = obj.transform.position;
            obj.GetComponent<Animator>().SetBool("isStart", true);
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
        return _stageName + _stageNumber.ToString();
    }
}

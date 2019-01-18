using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeMenuManager : MonoBehaviour {

    // 選択できるモード
    enum MODE_STATE
    {
        CreateRoom,
        JoinRoom,
        OffLine
    }

    // コントローラー
    private GameController _controller;

    private GameObject _fadeObj;            //　フェード

    // カーソル
    private GameObject _cursor;

    private int _state = 0;

    private bool _isStartFade = false;      //　True:フェード開始、False:フェード終了中

    public int State
    {
        get
        {
            return _state;
        }
    }

    void Start ()
    {
        Initialize();
	}

    void Update ()
    {
        SceneUpdate();
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    public void Initialize()
    {
        // コントローラーのインスタンス
        _controller = GameController.Instance;

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

        // コンポーネント
        _cursor = GameObject.Find("CursorImage");

        CursorPosition(_state);
    }

    /// <summary>
    /// シーンの更新処理
    /// </summary>
    /// <returns>true:シーンが終了した,false:シーンが終了していない</returns>
    public bool SceneUpdate()
    {
        ControllerUpdate();

        //　ステージが遷移されていたら
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
        _controller.ControllerUpdate();

        //　Aボタンが押されたらフェードアウトを開始する
        if (_controller.ButtonDown(Button.A) && !_isStartFade)
        {
            //　フェード開始
            Fade fade = new Fade();
            _fadeObj = fade.CreateFade();
            _fadeObj.GetComponentInChildren<Fade>().FadeOut();
            _isStartFade = true;
        }

        // 上十字キー及び上左スティック
        if ((_controller.CheckDirectionOnce(Direction.Front, StickType.LEFTSTICK)) ||
           (_controller.CheckDirectionOnce(Direction.Front, StickType.CLOSS)))
        {
            if(_state == (int)MODE_STATE.CreateRoom)
            {
                _state = (int)MODE_STATE.OffLine;
            }
            else
            {
                _state--;
            }

            CursorPosition(_state);

        }

        // 上十字キー及び上左スティック
        if ((_controller.CheckDirectionOnce(Direction.Back, StickType.LEFTSTICK)) ||
           (_controller.CheckDirectionOnce(Direction.Back, StickType.CLOSS)))
        {
            if (_state == (int)MODE_STATE.OffLine)
            {
                _state = (int)MODE_STATE.CreateRoom;
            }
            else
            {
                _state++;
            }

            CursorPosition(_state);

        }

    }

    void CursorPosition(int state)
    {
        GameObject obj = null;

        switch (_state)
        {
            case (int)MODE_STATE.CreateRoom:
                obj = GameObject.Find(MODE_STATE.CreateRoom.ToString());
                break;
            case (int)MODE_STATE.JoinRoom:
                obj = GameObject.Find(MODE_STATE.JoinRoom.ToString());
                break;
            case (int)MODE_STATE.OffLine:
                obj = GameObject.Find(MODE_STATE.OffLine.ToString());
                break;
            default:
                break;
        }

        _cursor.transform.position = obj.transform.position;

    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChackerManager : MonoBehaviour {

    // 選択できるものの名前
    enum SELECT_STATE
    {
        RoomCheck,
        BackModeSelect
    }

    // コントローラー
    private GameController _controller;

    private GameObject _fadeObj;            //　フェード
    private bool _isStartFade = false;      //　True:フェード開始、False:フェード終了中

    // カーソル
    private GameObject _cursor;

    private int _state = 0;

    private Network _network;
    private int _connectCount = 0;
    private bool _isConnecting = false;
    // ルームに入ったかをフラグで管理
    private bool _isJoinRoom = false;

    public bool JoinFlag
    {
        get
        {
            return _isJoinRoom;
        }
        set
        {
            _isJoinRoom = value;
        }
    }

    public int State
    {
        get
        {
            return _state;
        }
    }

    void Start()
    {
        Initialize();
    }

    void Update()
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
        _network = GameObject.FindObjectOfType<Network>();

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

    private void ControllerUpdate()
    {
        _controller.ControllerUpdate();

        if (_isJoinRoom)
        {
            //　フェード開始
            Fade fade = new Fade();
            _fadeObj = fade.CreateFade();
            _fadeObj.GetComponentInChildren<Fade>().FadeOut();
            _isStartFade = true;

        }
        else
        {
            //　Aボタンが押されたらフェードアウトを開始する
            if (_controller.ButtonDown(Button.A) && !_isStartFade)
            {
                switch (_state)
                {
                    case (int)SELECT_STATE.RoomCheck:
                        _network.JoinRoom();
                        _isConnecting = true;
                        break;
                    case (int)SELECT_STATE.BackModeSelect:
                        //　フェード開始
                        Fade fade = new Fade();
                        _fadeObj = fade.CreateFade();
                        _fadeObj.GetComponentInChildren<Fade>().FadeOut();
                        _isStartFade = true;
                        break;
                    default:
                        break;
                }
            }

            if (!_isConnecting)
            {
                // 上十字キー及び上左スティック
                if ((_controller.CheckDirectionOnce(Direction.Front, StickType.LEFTSTICK)) ||
                   (_controller.CheckDirectionOnce(Direction.Front, StickType.CLOSS)))
                {
                    if (_state == (int)SELECT_STATE.RoomCheck)
                    {
                        _state = (int)SELECT_STATE.BackModeSelect;
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
                    if (_state == (int)SELECT_STATE.BackModeSelect)
                    {
                        _state = (int)SELECT_STATE.RoomCheck;
                    }
                    else
                    {
                        _state++;
                    }

                    CursorPosition(_state);

                }

            }
            else
            {
                _connectCount++;

                if (_connectCount >= 300)
                {
                    _isConnecting = false;
                    _connectCount = 0;
                }
            }

        }

    }

    void CursorPosition(int state)
    {
        GameObject obj = null;

        switch (_state)
        {
            case (int)SELECT_STATE.RoomCheck:
                obj = GameObject.Find(SELECT_STATE.RoomCheck.ToString());
                break;
            case (int)SELECT_STATE.BackModeSelect:
                obj = GameObject.Find(SELECT_STATE.BackModeSelect.ToString());
                break;
            default:
                break;
        }

        _cursor.transform.position = obj.transform.position;

    }

}

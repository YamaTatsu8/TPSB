using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSettingManager : MonoBehaviour {

    // 選択できるものの名前
    enum SELECT_STATE
    {
        RoomName,
        CreateRoom
    }
    
    // コントローラー
    private GameController _controller;

    private GameObject _fadeObj;            //　フェード
    private bool _isStartFade = false;      //　True:フェード開始、False:フェード終了中

    // カーソル
    private GameObject _cursor;

    private int _state = 0;
    private bool _isInput = false;

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

            switch (_state)
            {
                case (int)SELECT_STATE.RoomName:
                    InputManager input = GameObject.Find("RoomNameInput").GetComponent<InputManager>();
                    if(_isInput)
                    {
                        input.FinInputField();
                        _isInput = false;
                    }
                    else
                    {
                        input.InitInputField();

                        _isInput = true;
                    }
                    break;
                case (int)SELECT_STATE.CreateRoom:
                    Network _network = GameObject.FindObjectOfType<Network>();
                    _network.CreateRoom();
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

        if(!_isInput)
        {
            // 上十字キー及び上左スティック
            if ((_controller.CheckDirectionOnce(Direction.Front, StickType.LEFTSTICK)) ||
               (_controller.CheckDirectionOnce(Direction.Front, StickType.CLOSS)))
            {
                if (_state == (int)SELECT_STATE.RoomName)
                {
                    _state = (int)SELECT_STATE.CreateRoom;
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
                if (_state == (int)SELECT_STATE.CreateRoom)
                {
                    _state = (int)SELECT_STATE.RoomName;
                }
                else
                {
                    _state++;
                }

                CursorPosition(_state);

            }

        }

    }

    void CursorPosition(int state)
    {
        GameObject obj = null;

        switch (_state)
        {
            case (int)SELECT_STATE.RoomName:
                obj = GameObject.Find(SELECT_STATE.RoomName.ToString());
                break;
            case (int)SELECT_STATE.CreateRoom:
                obj = GameObject.Find(SELECT_STATE.CreateRoom.ToString());
                break;
            default:
                break;
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeMenu : MonoBehaviour {

    //コントローラ
    private GameController _controller;

    //フェード
    private GameObject _fadeOut;

    //Canvas
    [SerializeField]
    private Canvas _canvas;

    //fadeが終わったかのフラグ
    private bool _fadeFlag = false;

    //シーンが終わった時のフラグ
    private bool _nextFlag;

    //Image
    [SerializeField]
    private Text[] _info;

    //bar
    [SerializeField]
    private RectTransform _createRoom;
    [SerializeField]
    private RectTransform _roomJoin;
    [SerializeField]
    private RectTransform _trainingRoom;
    [SerializeField]
    private RectTransform _exit;
    [SerializeField]
    private RectTransform _solo;

    private AudioManager _audioManager;

    //シーン名前
    private string _sceneName = "";
    

    //モード一覧
    private enum MODE_SELECT
    {
        CREATEROOM,
        ROOMJOIN,
        TRAININGROOM,
        SOLOMODE,
        EXIT
    }

    //モード選択
    private enum MODE
    {
        CREATE,
        JOIN,
        TRAINIG,
        SOLO,
        EXIT
    }

    //どこを選択しているか
    private int _modeNum = 0;

    //
    private int _state = 0;

    //Sprite
    [SerializeField]
    private Sprite _sprite;

    [SerializeField]
    private Sprite _sprite2;

	// Use this for initialization
	void Start () {

        _controller = GameController.Instance;

        Fade fade = new Fade();

        _fadeOut = fade.CreateFade();

        _fadeOut.GetComponentInChildren<Fade>().FadeIn();

        //全て非表示にする
        for (int i = 0; i < 4; i++)
        {
            _info[i].enabled = false;
        }

        //Barを探す
        _createRoom = GameObject.Find("CreateRoom").GetComponent<RectTransform>();
        _roomJoin = GameObject.Find("RoomJoin").GetComponent<RectTransform>();
        _trainingRoom = GameObject.Find("TrainingRoom").GetComponent<RectTransform>();
        _exit = GameObject.Find("Exit").GetComponent<RectTransform>();
        _solo = GameObject.Find("SoloMode").GetComponent<RectTransform>();

        _audioManager = AudioManager.Instance;

        _audioManager.PlayBGM("custma");

        _nextFlag = false;

        _fadeFlag = false;

    }
	
	// Update is called once per frame
	void Update () {

        //コントローラアップデート
        _controller.ControllerUpdate();

        //
        if (_fadeFlag == false)
        {
            _state = ChooseState(_state);
        }

        //CreateRoomより上に行ったらTrainingroomに行く
        if(_state < (int)MODE_SELECT.CREATEROOM)
        {
            _state = (int)MODE_SELECT.EXIT;
        }

        //Trainingroomより下に行ったらCreateRoomに行く
        if(_state > (int)MODE_SELECT.EXIT)
        {
            _state = (int)MODE_SELECT.CREATEROOM;
        }

        //選択
        switch(_state)
        {
            case (int)MODE_SELECT.CREATEROOM:
                _createRoom.GetComponent<Image>().sprite = _sprite2;
                _createRoom.localScale = new Vector3(1.2f, 1.2f, 1);
                _roomJoin.GetComponent<Image>().sprite = _sprite;
                _roomJoin.localScale = new Vector3(1, 1, 1);
                _trainingRoom.GetComponent<Image>().sprite = _sprite;
                _trainingRoom.localScale = new Vector3(1, 1, 1);
                _solo.GetComponent<Image>().sprite = _sprite;
                _solo.localScale = new Vector3(1, 1, 1);
                _exit.GetComponent<Image>().sprite = _sprite;
                _exit.localScale = new Vector3(1.0f, 1.0f, 1);
                break;
            case (int)MODE_SELECT.ROOMJOIN:
                _createRoom.GetComponent<Image>().sprite = _sprite;
                _createRoom.localScale = new Vector3(1, 1, 1);
                _roomJoin.GetComponent<Image>().sprite = _sprite2;
                _roomJoin.localScale = new Vector3(1.2f, 1.2f, 1);
                _trainingRoom.GetComponent<Image>().sprite = _sprite;
                _trainingRoom.localScale = new Vector3(1, 1, 1);
                _solo.GetComponent<Image>().sprite = _sprite;
                _solo.localScale = new Vector3(1, 1, 1);
                _exit.GetComponent<Image>().sprite = _sprite;
                _exit.localScale = new Vector3(1.0f, 1.0f, 1);
                break;
            case (int)MODE_SELECT.TRAININGROOM:
                _createRoom.GetComponent<Image>().sprite = _sprite;
                _createRoom.localScale = new Vector3(1, 1, 1);
                _roomJoin.GetComponent<Image>().sprite = _sprite;
                _roomJoin.localScale = new Vector3(1, 1, 1);
                _trainingRoom.GetComponent<Image>().sprite = _sprite2;
                _trainingRoom.localScale = new Vector3(1.2f, 1.2f, 1);
                _solo.GetComponent<Image>().sprite = _sprite;
                _solo.localScale = new Vector3(1, 1, 1);
                _exit.GetComponent<Image>().sprite = _sprite;
                _exit.localScale = new Vector3(1.0f, 1.0f, 1);
                break;
            case (int)MODE_SELECT.SOLOMODE:
                _createRoom.GetComponent<Image>().sprite = _sprite;
                _createRoom.localScale = new Vector3(1, 1, 1);
                _roomJoin.GetComponent<Image>().sprite = _sprite;
                _roomJoin.localScale = new Vector3(1, 1, 1);
                _trainingRoom.GetComponent<Image>().sprite = _sprite;
                _trainingRoom.localScale = new Vector3(1, 1, 1);
                _solo.GetComponent<Image>().sprite = _sprite2;
                _solo.localScale = new Vector3(1.2f, 1.2f, 1);
                _exit.GetComponent<Image>().sprite = _sprite;
                _exit.localScale = new Vector3(1.0f, 1.0f, 1);
                break;
            case (int)MODE_SELECT.EXIT:
                _createRoom.GetComponent<Image>().sprite = _sprite;
                _createRoom.localScale = new Vector3(1, 1, 1);
                _roomJoin.GetComponent<Image>().sprite = _sprite;
                _roomJoin.localScale = new Vector3(1, 1, 1);
                _trainingRoom.GetComponent<Image>().sprite = _sprite;
                _trainingRoom.localScale = new Vector3(1.0f, 1.0f, 1);
                _solo.GetComponent<Image>().sprite = _sprite;
                _solo.localScale = new Vector3(1, 1, 1);
                _exit.GetComponent<Image>().sprite = _sprite2;
                _exit.localScale = new Vector3(1.2f, 1.2f, 1);
                break;
        }

        //_stateに合わせて説明のテキストを変更
        for (int i = 0; i < 4; i++)
        {
            if (_state == i)
            {
                _info[i].enabled = true;
            }
            else
            {
                _info[i].enabled = false;
            }
        }

        //決定
        if(_controller.ButtonDown(Button.A))
        {
            _audioManager.PlaySE("select09");
            switch (_state)
            {
                case (int)MODE_SELECT.CREATEROOM:
                    //次のシーンに移動    
                    Fade fade = new Fade();
                    _fadeOut = fade.CreateFade();
                    _fadeOut.GetComponentInChildren<Fade>().FadeOut();
                    _fadeFlag = true;
                    _modeNum = (int)MODE.CREATE;
                    break;
                case (int)MODE_SELECT.ROOMJOIN:
                    //次のシーンに移動    
                    Fade fade2 = new Fade();
                    _fadeOut = fade2.CreateFade();
                    _fadeOut.GetComponentInChildren<Fade>().FadeOut();
                    _fadeFlag = true;
                    _modeNum = (int)MODE.JOIN;
                    break;
                case (int)MODE_SELECT.TRAININGROOM:
                    //次のシーンに移動    
                    Fade fade3 = new Fade();
                    _fadeOut = fade3.CreateFade();
                    _fadeOut.GetComponentInChildren<Fade>().FadeOut();
                    _fadeFlag = true;
                    _modeNum = (int)MODE.TRAINIG;
                    break;
                case (int)MODE_SELECT.SOLOMODE:
                    //次のシーンに移動    
                    Fade fade4 = new Fade();
                    _fadeOut = fade4.CreateFade();
                    _fadeOut.GetComponentInChildren<Fade>().FadeOut();
                    _fadeFlag = true;
                    _modeNum = (int)MODE.SOLO;
                    break;
                case (int)MODE_SELECT.EXIT:
                    #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                    #elif UNITY_STANDALONE
                    Application.Quit();
                    #endif
                    break;
            }
        }

        //fadeが終わったら次のシーンへ移動する
        if (_fadeOut.GetComponentInChildren<Fade>().isCheckedFadeOut() && _fadeFlag == true)
        {
            switch (_modeNum)
            {
                case (int)MODE.CREATE:
                    _sceneName = "RoomSetting";
                    _nextFlag = true;
                    break;
                case (int)MODE.JOIN:
                    _sceneName = "RoomCheck";
                    _nextFlag = true;
                    break;
                case (int)MODE.TRAINIG:
                    _sceneName = "TrainingRoom";
                    _nextFlag = true;
                    break;
                case (int)MODE.SOLO:
                    _sceneName = "Customaize";
                    _nextFlag = true;
                    break;
            }

        }

    }

    //選択する関数
    private int ChooseState(int mstate)
    {
        int state = mstate;
        

        if (_controller.CheckDirectionOnce(Direction.Left, StickType.LEFTSTICK))
        {
            _audioManager.PlaySE("select09");
            state -= 1;
        }
        else if (_controller.CheckDirectionOnce(Direction.Right, StickType.LEFTSTICK))
        {
            _audioManager.PlaySE("select09");
            state += 1;
        }
        else if (_controller.CheckDirectionOnce(Direction.Front, StickType.LEFTSTICK))
        {
            _audioManager.PlaySE("select09");
            state -= 1;
        }
        else if (_controller.CheckDirectionOnce(Direction.Back, StickType.LEFTSTICK))
        {
            _audioManager.PlaySE("select09");
            state += 1;
        }

        return state;
    }

    public bool GetNextFlag()
    {
        return _nextFlag;
    }

    public string SelectedSceneName()
    {
        return _sceneName;
    }

}

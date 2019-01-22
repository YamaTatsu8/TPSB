using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeMenu : MonoBehaviour {

    //コントローラ
    private GameController _controller;

    //Canvas
    [SerializeField]
    private Canvas _canvas;

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

    //モード一覧
    private enum MODE_SELECT
    {
        CREATEROOM,
        ROOMJOIN,
        TRAININGROOM
    }

    //モードステート
    private int _state = 0;

    //Sprite
    [SerializeField]
    private Sprite _sprite;

    [SerializeField]
    private Sprite _sprite2;

	// Use this for initialization
	void Start () {

        _controller = GameController.Instance;

        //全て非表示にする
        for(int i = 0; i < 3; i++)
        {
            _info[i].enabled = false;
        }

        //Barを探す
        _createRoom = GameObject.Find("CreateRoom").GetComponent<RectTransform>();
        _roomJoin = GameObject.Find("RoomJoin").GetComponent<RectTransform>();
        _trainingRoom = GameObject.Find("TrainingRoom").GetComponent<RectTransform>();

	}
	
	// Update is called once per frame
	void Update () {

        //コントローラアップデート
        _controller.ControllerUpdate();

        //
        _state = ChooseState(_state);

        //CreateRoomより上に行ったらTrainingroomに行く
        if(_state < (int)MODE_SELECT.CREATEROOM)
        {
            _state = (int)MODE_SELECT.TRAININGROOM;
        }

        //Trainingroomより下に行ったらCreateRoomに行く
        if(_state > (int)MODE_SELECT.TRAININGROOM)
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
                break;
            case (int)MODE_SELECT.ROOMJOIN:
                _createRoom.GetComponent<Image>().sprite = _sprite;
                _createRoom.localScale = new Vector3(1, 1, 1);
                _roomJoin.GetComponent<Image>().sprite = _sprite2;
                _roomJoin.localScale = new Vector3(1.2f, 1.2f, 1);
                _trainingRoom.GetComponent<Image>().sprite = _sprite;
                _trainingRoom.localScale = new Vector3(1, 1, 1);
                break;
            case (int)MODE_SELECT.TRAININGROOM:
                _createRoom.GetComponent<Image>().sprite = _sprite;
                _createRoom.localScale = new Vector3(1, 1, 1);
                _roomJoin.GetComponent<Image>().sprite = _sprite;
                _roomJoin.localScale = new Vector3(1, 1, 1);
                _trainingRoom.GetComponent<Image>().sprite = _sprite2;
                _trainingRoom.localScale = new Vector3(1.2f, 1.2f, 1);
                break;
        }

        //_stateに合わせて説明のテキストを変更
        for (int i = 0; i < 3; i++)
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
            switch (_state)
            {
                case (int)MODE_SELECT.CREATEROOM:
                 
                    break;
                case (int)MODE_SELECT.ROOMJOIN:
           
                    break;
                case (int)MODE_SELECT.TRAININGROOM:
                   
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
            state -= 1;
        }
        else if (_controller.CheckDirectionOnce(Direction.Right, StickType.LEFTSTICK))
        {
            state += 1;
        }
        else if (_controller.CheckDirectionOnce(Direction.Front, StickType.LEFTSTICK))
        {
            state -= 1;
        }
        else if (_controller.CheckDirectionOnce(Direction.Back, StickType.LEFTSTICK))
        {
            state += 1;
        }

        return state;
    }

}

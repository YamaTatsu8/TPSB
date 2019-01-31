using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainingPoseMenu : MonoBehaviour {

    //Canvas
    [SerializeField]
    private Canvas _canvas;

    //Popバー
    private RectTransform _pop;

    //メニュー一覧
    private RectTransform _contineBar;

    private RectTransform _npcBar;

    private RectTransform _exitMenu;

    //Popフラグ
    private bool _popFlag;

    //選択しているmenu
    private int _menuState = 0;

    //NPCのステート
    private int _npcState = 0;

    //PoseMenuのフラグ
    private bool _menuFlag;

    private bool _fadeFlag;

    private GameObject _pauseManager;

    //フェード
    private GameObject _fadeOut;

    //シーンが終わった時のフラグ
    private bool _nextFlag;

    //メニューでNPCを選択しているときのフラグ
    private bool _npcFlag;

    //ポーズメニュー一覧
    private enum POSE_MENU
    {
        CONTINUE,
        NPC,
        EXIT
    }

    //NPCの設定
    private enum NPC_OPTION
    {
        IDLE,
        ATTACK
    }

    //NPC
    private GameObject _npc;

    //コントローラー
    private GameController _controller;

	// Use this for initialization
	void Start () {

        //初期化
        _controller = GameController.Instance;

        _popFlag = false;

        _menuFlag = false;

        _npcFlag = false;

        _nextFlag = false;

        _fadeFlag = false;

        Fade fade = new Fade();

        _fadeOut = fade.CreateFade();

        _fadeOut.GetComponentInChildren<Fade>().FadeIn();

        //Find
        _pop = GameObject.Find("Pop").GetComponent<RectTransform>();

        _contineBar = GameObject.Find("Continue").GetComponent<RectTransform>();

        _npcBar = GameObject.Find("NPC").GetComponent<RectTransform>();

        _exitMenu = GameObject.Find("EXIT").GetComponent<RectTransform>();

        _pauseManager = GameObject.Find("PauseManager");

	}
	
	// Update is called once per frame
	void Update () {

        _controller.ControllerUpdate();

        //startボタンが押された時ポップを出す
        if(_controller.ButtonDown(Button.START))
        {
            _popFlag = true;
            _menuFlag = true;
        }

        if(_popFlag)
        {
            //popを表示
        }

        if(_menuFlag)
        {
            if (_npcFlag == false)
            {
                //メニュー操作
                _menuState = ChooseStateRL(_menuState);

                if(_menuState > 2)
                {
                    _menuState = 0;
                }
                else if(_menuState < 0)
                {
                    _menuState = 2;
                }

                switch (_menuState)
                {
                    case (int)POSE_MENU.CONTINUE:
                        _contineBar.GetComponent<Image>().color = new Color(255, 255, 255);
                        _npcBar.GetComponent<Image>().color = new Color(0, 0, 0);
                        _exitMenu.GetComponent<Image>().color = new Color(0, 0, 0);
                        break;
                    case (int)POSE_MENU.NPC:
                        _contineBar.GetComponent<Image>().color = new Color(0, 0, 0);
                        _npcBar.GetComponent<Image>().color = new Color(255, 255, 255);
                        _exitMenu.GetComponent<Image>().color = new Color(0, 0, 0);
                        break;
                    case (int)POSE_MENU.EXIT:
                        _contineBar.GetComponent<Image>().color = new Color(0, 0, 0);
                        _npcBar.GetComponent<Image>().color = new Color(0, 0, 0);
                        _exitMenu.GetComponent<Image>().color = new Color(255, 255, 255);
                        break;
                }

                //Aボタンが押された時選択しているステートの処理をする
                if(_controller.ButtonDown(Button.A))
                {
                    switch (_menuState)
                    {
                        case (int)POSE_MENU.CONTINUE:
                            //メニュー画面を閉じる
                            _menuFlag = false;
                            _popFlag = false;
                            break;
                        case (int)POSE_MENU.NPC:
                            _npcFlag = true;
                            //NPC_OPTIONを開く
                            break;
                        case (int)POSE_MENU.EXIT:
                            //タイトルに戻る
                            Fade fade = new Fade();
                            _fadeOut = fade.CreateFade();
                            _fadeOut.GetComponentInChildren<Fade>().FadeOut();
                            _fadeFlag = true;
                            break;
                    }
                }

            }
            else
            {
                //NPCのモード設定
                _npcState = ChooseStateRL(_npcState);

                switch (_npcState)
                {
                    case (int)NPC_OPTION.IDLE:
                        break;
                    case (int)NPC_OPTION.ATTACK:
                        break;
                }

            }
            
        }

        //fadeが終わったら次のシーンへ移動する
        if (_fadeOut.GetComponentInChildren<Fade>().isCheckedFadeOut() && _fadeFlag == true)
        {
        
                switch (_menuState)
            {
                case (int)POSE_MENU.CONTINUE:
                    break;
                case (int)POSE_MENU.NPC:
                    break;
                case (int)POSE_MENU.EXIT:
                    //タイトルに戻る
                    _nextFlag = true;
                    break;
            }
        }

    }

    //選択場面のステート
    private int ChooseStateUpDown(int state)
    {
        int _state = state;
        
        //スティックを上下に動かしたら±1する
        if (_controller.CheckDirectionOnce(Direction.Front, StickType.LEFTSTICK))
        {
            _state -= 1;
        }
        else if (_controller.CheckDirectionOnce(Direction.Back, StickType.LEFTSTICK))
        {
            _state += 1;
        }

        return _state;
    }

    //選択場面のステート左右ver
    private int ChooseStateRL(int state)
    {
        int _state = state;

        //スティックを左右に動かしたら±1する
        if (_controller.CheckDirectionOnce(Direction.Left, StickType.LEFTSTICK))
        {
            _state -= 1;
        }
        else if (_controller.CheckDirectionOnce(Direction.Right, StickType.LEFTSTICK))
        {
            _state += 1;
        }

        return _state;
    }

    //次のシーンへ移動するかのフラグ
    public  bool GetNextFlag()
    {
        return _nextFlag;
    }

}


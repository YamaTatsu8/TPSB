using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class Equipment : MonoBehaviour {

    //モデルオブジェクト
    [SerializeField]
    private GameObject _modelObj;

    //フェード
    GameObject _fadeOut;

    //コントローラのスクリプト
    private GameController _controller;

    //Bar
    private RectTransform _bar;

    //モデルバー
    private RectTransform _modelBar;

    private RectTransform _modelImage;

    private RectTransform _mainWeapon1;
    
    private RectTransform _mainWeapon2;
    
    private RectTransform _subWeapon;
    
    private RectTransform _start;

    private RectTransform _backMenu;

    private RectTransform _ready;

    //ポップ  
    private RectTransform _pop;

    //stateの値
    private int _state = 0;

    //
    [SerializeField]
    private int _mainState = 0;

    //モデルのステート
    private int _modelState = 0;
                                                    
    //
    [SerializeField]
    private GameObject canvas;

    [SerializeField]
    private GameObject _modelBarCan;

    //PlayerSystem
    [SerializeField]
    private GameObject _playerSystem;

    //Barのフラグ
    private bool _barFlag = false;

    //メインのフラグ
    private bool _mainFlag = false;

    //武器リスト
    private List<string[]> _weaponList = new List<string[]>();

    //モデルリスト
    private List<string[]> _modelList = new List<string[]>();

    //武器の画像リスト
    private RectTransform[] _weaponImage = new RectTransform[5];

    //武器の画像リスト
    private RectTransform[] _modelImage2 = new RectTransform[6];

    //
    [SerializeField]
    private RectTransform _cusor2;

    //
    private RectTransform _cusor3;

    private RectTransform _cusor4;

    //
    private RectTransform _yes;

    private RectTransform _no;

    //武器数
    private int _weaponNum = 0;

    //モデル数
    private int _modelNum = 0;

    //モデルに装備
    [SerializeField]
    private GameObject _model;

    //オーディオソース
    private AudioSource _audioSource;

    //SE
    //決定音
    public AudioClip _decision;
    //セレクト音
    public AudioClip _select;
    //キャンセル音
    public AudioClip _cansel;
    //ぶっぶー音
    public AudioClip _bub;

    //シーン移動のフラグ
    private bool _sceneNextFlag = false;

    //どこを選んでいるかのState
    private enum EQUIPMENT_STATE
    {
        MODEL,
        MAIN_WEAPON1,
        MAIN_WEAPON2
    }

    //武器一覧
    private enum WEAPON_STATE
    {
        WEAPON1,
        WEAPON2,
        WEAPON3,
        WEAPON4
    }

    //装備のところ
    private enum WEAPON_MAIN
    {
        MAIN1,
        MAIN2,
        SUB
    }

    //モデル
    private enum MODEL_STATE
    {
        UNITY,
        ION,
        Queendiva,
        Misyara,
        Noah,
        Anathema
    }

    //
    [SerializeField]
    private int _weaponState = 0;

    //次へのフラグ
    private bool _nextFlag = false;
    //ポップアップするフラグ
    private bool _popFlag = false;

    //
    private bool _selectFlag;

    private int _nextState = 0;

    //fade終わったかのフラグ
    private bool _fadeFlag = false;

    //モデルのバーに対してのフラグ
    private bool _modelFlag = false;

    //バーのスプライト
    [SerializeField]
    private Sprite _sprite;

    [SerializeField]
    private Sprite _sprite2;

    //前のシーンへ戻るフラグ
    private bool _backFlag;

    private enum NEXT_STATE
    {
        YES,
        NO
    }

    // Use this for initialization
    void Start ()
    {

        Fade fade = new Fade();

        _fadeOut = fade.CreateFade();

        _fadeOut.GetComponentInChildren<Fade>().FadeIn();

        //初期化
        _controller = GameController.Instance;
        _sceneNextFlag = false;

        //audioコンポーネント
        _audioSource = gameObject.GetComponent<AudioSource>();

        //コンポーネント
        _modelImage = GameObject.Find("Model").GetComponent<RectTransform>();
        _mainWeapon1 = GameObject.Find("MainWeapon1").GetComponent<RectTransform>();
        _mainWeapon2 = GameObject.Find("MainWeapon2").GetComponent<RectTransform>();
     
        _bar = GameObject.Find("Bar").GetComponent<RectTransform>();

        _modelBar = GameObject.Find("ModelBar").GetComponent<RectTransform>();

        _pop = GameObject.Find("Pop").GetComponent<RectTransform>();

        _yes = GameObject.Find("Yes").GetComponent<RectTransform>();

        _no = GameObject.Find("No").GetComponent<RectTransform>();

        _backMenu = GameObject.Find("BackMenu").GetComponent<RectTransform>();

        _ready = GameObject.Find("Ready").GetComponent<RectTransform>();

        //popのスケールは最初は0にする
        _pop.localScale = new Vector3(0, 0, 0);

        //最初バーのスケールは0にする
        _bar.localScale = new Vector3(1.5f, 0, 1);

        //モデルバーのスケール
        _modelBar.localScale = new Vector3(1.5f, 0, 1);

        //メイン武器のリスト作成
        WeaponAdd("WeaponList", _weaponList);

        //モデルのリスト
        ModelAdd("ModelList", _modelList);

        _playerSystem = GameObject.Find("PlayerSystem");

        _playerSystem.GetComponent<PlayerSystem>().setChar("Unity-Chan");

        Vector3 rePos;

        for (int i = 0; i < _weaponNum; i++)
        {
            //Resources/Imagesから一致するものを探してくる
            GameObject img = (GameObject)Instantiate(Resources.Load("Images/" + _weaponList[i][0].ToString()));
            img.transform.SetParent(canvas.transform, false);
            img.GetComponent<WeaponName>().setName(_weaponList[i][0].ToString());
            _weaponImage[i] = img.GetComponent<RectTransform>();

            rePos = canvas.GetComponent<RectTransform>().position;

            _weaponImage[i].localPosition = new Vector3(50, 20 * (i + 1), 0);
        }

        GameObject cusor = (GameObject)Instantiate(Resources.Load("Images/Cusor2"));

        cusor.transform.SetParent(canvas.transform, false);

        _cusor2 = cusor.GetComponent<RectTransform>();

        rePos = _weaponImage[0].localPosition;

        _cusor2.localPosition = rePos;
    
        _cusor2.transform.rotation = Quaternion.Euler(180.0f, 0.0f, 0.0f);

        rePos = _yes.localPosition;

        Vector3 rePos2;

        for (int i = 0; i < _modelNum; i++)
        {
            //Resources/Imagesから一致するものを探してくる
            GameObject img = (GameObject)Instantiate(Resources.Load("Images/" + _modelList[i][0].ToString()));
            img.transform.SetParent(_modelBarCan.transform, false);
            img.GetComponent<WeaponName>().setName(_modelList[i][0].ToString());
            _modelImage2[i] = img.GetComponent<RectTransform>();

            rePos2 = _modelBarCan.GetComponent<RectTransform>().position;

            _modelImage2[i].localPosition = new Vector3(50, 15 * (i + 1), 0);
        }

        GameObject cusor1 = (GameObject)Instantiate(Resources.Load("Images/Cusor3"));

        cusor1.transform.SetParent(_modelBarCan.transform, false);

        _cusor4 = cusor1.GetComponent<RectTransform>();

        rePos2 = _modelImage2[0].localPosition;

        _cusor4.localPosition = rePos2;

        _cusor4.transform.rotation = Quaternion.Euler(180.0f, 0.0f, 0.0f);

        rePos2 = _yes.localPosition;

        _model = GameObject.Find("PlayerModel");

        _backFlag = false;

        _selectFlag = false;

    }
	
	// Update is called once per frame
	void Update () {

        _controller.ControllerUpdate();

        //どこを選択しているかのState
        if (_nextFlag == false)
        {
           
            if (_mainFlag == false)
                {
                    _state = ChooseState(_state);

                if (_state < (int)EQUIPMENT_STATE.MODEL)
                {
                    _state = (int)EQUIPMENT_STATE.MAIN_WEAPON2;
                }

                if (_state > (int)EQUIPMENT_STATE.MAIN_WEAPON2)
                {
                    _state = (int)EQUIPMENT_STATE.MODEL;
                }

                //Bボタンが押されたらタイトルシーンに戻る
                if (_controller.ButtonDown(Button.B))
                {
                    //タイトルシーンに遷移
                    _popFlag = true;
                    _selectFlag = true;
                }

                //ModelからMainWeaponまでの選択
                switch (_state)
                {
                    case (int)EQUIPMENT_STATE.MODEL:
                        _mainWeapon2.GetComponent<Image>().sprite = _sprite;
                        _mainWeapon2.localScale = new Vector3(1.2f, 1.2f, 1);
                        _mainWeapon1.GetComponent<Image>().sprite = _sprite;
                        _mainWeapon1.localScale = new Vector3(1.2f, 1.2f, 1);
                        _modelImage.GetComponent<Image>().sprite = _sprite2;
                        _modelImage.localScale = new Vector3(1.5f, 1.5f, 1);
                        break;
                    case (int)EQUIPMENT_STATE.MAIN_WEAPON1:
                        _modelImage.GetComponent<Image>().sprite = _sprite;
                        _modelImage.localScale = new Vector3(1.2f, 1.2f, 1);
                        _mainWeapon2.localScale = new Vector3(1.2f, 1.2f, 1);
                        _mainWeapon2.GetComponent<Image>().sprite = _sprite;
                        _mainWeapon1.GetComponent<Image>().sprite = _sprite2;
                        _mainWeapon1.localScale = new Vector3(1.5f, 1.5f, 1);
                        break;
                    case (int)EQUIPMENT_STATE.MAIN_WEAPON2:
                        _modelImage.GetComponent<Image>().sprite = _sprite;
                        _modelImage.localScale = new Vector3(1.2f, 1.2f, 1);
                        _mainWeapon1.GetComponent<Image>().sprite = _sprite;
                        _mainWeapon1.localScale = new Vector3(1.2f, 1.2f, 1);
                        _mainWeapon2.GetComponent<Image>().sprite = _sprite2;
                        _mainWeapon2.localScale = new Vector3(1.5f, 1.5f, 1);
                        break;
                }

                if (_controller.ButtonDown(Button.A) && _barFlag == false && _modelFlag == false)
                {
                    ChooseMenu((EQUIPMENT_STATE)_state);
                    _audioSource.PlayOneShot(_decision);
                }

            }
            else if (_mainFlag == true && _barFlag == true)
            {

                //コントローラ操作
                _mainState = ChooseState(_mainState);

                if (_mainState < (int)WEAPON_STATE.WEAPON1)
                {
                    _mainState = (int)WEAPON_STATE.WEAPON4;
                }

                if (_mainState > (int)WEAPON_STATE.WEAPON4)
                {
                    _mainState = (int)WEAPON_STATE.WEAPON1;
                }

                if (_weaponState == 0)
                {
                    switch (_mainState)
                    {
                        case (int)WEAPON_STATE.WEAPON1:
                            _cusor2.localPosition = _weaponImage[_mainState].localPosition;
                            break;
                        case (int)WEAPON_STATE.WEAPON2:
                            _cusor2.localPosition = _weaponImage[_mainState].localPosition;
                            break;
                        case (int)WEAPON_STATE.WEAPON3:
                            _cusor2.localPosition = _weaponImage[_mainState].localPosition;
                            break;
                        case (int)WEAPON_STATE.WEAPON4:
                            _cusor2.localPosition = _weaponImage[_mainState].localPosition;
                            break;
                    }
                }
                else if (_weaponState == 1)
                {
                    switch (_mainState)
                    {
                        case (int)WEAPON_STATE.WEAPON1:
                            _cusor2.localPosition = _weaponImage[_mainState].localPosition;
                            break;
                        case (int)WEAPON_STATE.WEAPON2:
                            _cusor2.localPosition = _weaponImage[_mainState].localPosition;
                            break;
                        case (int)WEAPON_STATE.WEAPON3:
                            _cusor2.localPosition = _weaponImage[_mainState].localPosition;
                            break;
                        case (int)WEAPON_STATE.WEAPON4:
                            _cusor2.localPosition = _weaponImage[_mainState].localPosition;
                            break;
                    }
                }

                if (_controller.ButtonDown(Button.A))
                {
                    _barFlag = false;
                    switch (_weaponState)
                    {
                        case (int)WEAPON_MAIN.MAIN1:
                            //選んだ武器を装備
                            if (_playerSystem.GetComponent<PlayerSystem>().getMain1() != _playerSystem.GetComponent<PlayerSystem>().getMain2() && _playerSystem.GetComponent<PlayerSystem>().getMain2() != _weaponList[_mainState][0].ToString())
                            {
                                _audioSource.PlayOneShot(_decision);
                                _playerSystem.GetComponent<PlayerSystem>().setMain1(_weaponList[_mainState][0].ToString());
                                _mainWeapon1.GetComponent<WeaponName>().setName(_weaponList[_mainState][0].ToString());
                            }
                            else
                            {
                                //キャンセル音
                                _audioSource.PlayOneShot(_bub);
                            }
                            break;
                        case (int)WEAPON_MAIN.MAIN2:
                            //選んだ武器を装備
                            if (_playerSystem.GetComponent<PlayerSystem>().getMain1() != _playerSystem.GetComponent<PlayerSystem>().getMain2() && _playerSystem.GetComponent<PlayerSystem>().getMain1() != _weaponList[_mainState][0].ToString())
                            {
                                _audioSource.PlayOneShot(_decision);
                                _playerSystem.GetComponent<PlayerSystem>().setMain2(_weaponList[_mainState][0].ToString());
                                _mainWeapon2.GetComponent<WeaponName>().setName(_weaponList[_mainState][0].ToString());
                            }
                            else
                            {
                                //キャンセル音
                                _audioSource.PlayOneShot(_bub);
                            }
                            break;
                    }

                }

                if (_controller.ButtonDown(Button.B))
                {
                    _audioSource.PlayOneShot(_cansel);
                    _barFlag = false;
                }

            }
            else if (_mainFlag == true && _modelFlag == true)
            {
                //コントローラ操作
                _modelState = ChooseState(_modelState);

                //カーソルが一番上までいったら一番下にする
                if (_modelState < (int)MODEL_STATE.UNITY)
                {
                    _modelState = (int)MODEL_STATE.Anathema;
                }
                else if (_modelState > (int)MODEL_STATE.Anathema)
                {
                    _modelState = (int)MODEL_STATE.UNITY;
                }

                //ステートに合わせてキャラ選択を変える
                switch (_modelState)
                {
                    case (int)MODEL_STATE.UNITY:
                        _cusor4.localPosition = _modelImage2[_modelState].localPosition;
                        break;
                    case (int)MODEL_STATE.ION:
                        _cusor4.localPosition = _modelImage2[_modelState].localPosition;
                        break;
                    case (int)MODEL_STATE.Queendiva:
                        _cusor4.localPosition = _modelImage2[_modelState].localPosition;
                        break;
                    case (int)MODEL_STATE.Misyara:
                        _cusor4.localPosition = _modelImage2[_modelState].localPosition;
                        break;
                    case (int)MODEL_STATE.Noah:
                        _cusor4.localPosition = _modelImage2[_modelState].localPosition;
                        break;
                    case (int)MODEL_STATE.Anathema:
                        _cusor4.localPosition = _modelImage2[_modelState].localPosition;
                        break;
                }

                if (_controller.ButtonDown(Button.A))
                {
                    //PlayerSystemにモデルの情報を挿入、モデルセレクトにキャラ情報を入れる
                    _modelFlag = false;
                    switch (_modelState)
                    {
                        case (int)MODEL_STATE.UNITY:
                            _audioSource.PlayOneShot(_decision);
                            _model.GetComponent<ModelSelect>().SetModel(_modelList[_modelState][0].ToString());
                            _modelImage.GetComponent<WeaponName>().setName(_modelList[_modelState][0].ToString());
                            _playerSystem.GetComponent<PlayerSystem>().setChar(_modelList[_modelState][0].ToString());
                            Debug.Log(_playerSystem.GetComponent<PlayerSystem>().getChar());
                            break;
                        case (int)MODEL_STATE.ION:
                            _audioSource.PlayOneShot(_decision);
                            _model.GetComponent<ModelSelect>().SetModel(_modelList[_modelState][0].ToString());
                            _modelImage.GetComponent<WeaponName>().setName(_modelList[_modelState][0].ToString());
                            _playerSystem.GetComponent<PlayerSystem>().setChar(_modelList[_modelState][0].ToString());
                            Debug.Log(_playerSystem.GetComponent<PlayerSystem>().getChar());
                            break;
                        case (int)MODEL_STATE.Queendiva:
                            _audioSource.PlayOneShot(_decision);
                            _model.GetComponent<ModelSelect>().SetModel(_modelList[_modelState][0].ToString());
                            _modelImage.GetComponent<WeaponName>().setName(_modelList[_modelState][0].ToString());
                            _playerSystem.GetComponent<PlayerSystem>().setChar(_modelList[_modelState][0].ToString());
                            Debug.Log(_playerSystem.GetComponent<PlayerSystem>().getChar());
                            break;
                        case (int)MODEL_STATE.Misyara:
                            _audioSource.PlayOneShot(_decision);
                            _model.GetComponent<ModelSelect>().SetModel(_modelList[_modelState][0].ToString());
                            _modelImage.GetComponent<WeaponName>().setName(_modelList[_modelState][0].ToString());
                            _playerSystem.GetComponent<PlayerSystem>().setChar(_modelList[_modelState][0].ToString());
                            Debug.Log(_playerSystem.GetComponent<PlayerSystem>().getChar());
                            break;
                        case (int)MODEL_STATE.Noah:
                            _audioSource.PlayOneShot(_decision);
                            _model.GetComponent<ModelSelect>().SetModel(_modelList[_modelState][0].ToString());
                            _modelImage.GetComponent<WeaponName>().setName(_modelList[_modelState][0].ToString());
                            _playerSystem.GetComponent<PlayerSystem>().setChar(_modelList[_modelState][0].ToString());
                            Debug.Log(_playerSystem.GetComponent<PlayerSystem>().getChar());
                            break;
                        case (int)MODEL_STATE.Anathema:
                            _audioSource.PlayOneShot(_decision);
                            _model.GetComponent<ModelSelect>().SetModel(_modelList[_modelState][0].ToString());
                            _modelImage.GetComponent<WeaponName>().setName(_modelList[_modelState][0].ToString());
                            _playerSystem.GetComponent<PlayerSystem>().setChar(_modelList[_modelState][0].ToString());
                            Debug.Log(_playerSystem.GetComponent<PlayerSystem>().getChar());
                            break;
                    }
                }

                //Bボタンを押されたらキャンセル
                if (_controller.ButtonDown(Button.B))
                {
                    _audioSource.PlayOneShot(_cansel);
                    _modelFlag = false;

                }

            }
        }
        else
        {
            //コントローラ操作
            _nextState = ChooseState(_nextState);

            if(_selectFlag == false)
            {
                _ready.GetComponent<Image>().enabled = true;
                _backMenu.GetComponent<Image>().enabled = false;
            }
            else if(_selectFlag == true)
            {
                _ready.GetComponent<Image>().enabled = false;
                _backMenu.GetComponent<Image>().enabled = true;
            }

            if (_nextState < (int)NEXT_STATE.YES)
            {
                _nextState = (int)NEXT_STATE.NO;
            }

            if (_nextState > (int)NEXT_STATE.NO)
            {
                _nextState = (int)NEXT_STATE.YES;
            }

            switch (_nextState)
            {
                case (int)NEXT_STATE.YES:
                    _yes.GetComponent<Image>().color = new Color(255, 255, 255);
                    _no.GetComponent<Image>().color = new Color(0, 0, 0);
                    break;
                case (int)NEXT_STATE.NO:
                    _yes.GetComponent<Image>().color = new Color(0, 0, 0);
                    _no.GetComponent<Image>().color = new Color(255, 255, 255);
                    break;
            }

            //fadeを表示させる
            if (_controller.ButtonDown(Button.A) && _fadeFlag == false)
            {           
                    _audioSource.PlayOneShot(_decision);
                    switch (_nextState)
                    {
                        case (int)NEXT_STATE.YES:
                            //次のシーンに移動    
                            Fade fade = new Fade();

                            _fadeOut = fade.CreateFade();

                            _fadeOut.GetComponentInChildren<Fade>().FadeOut();

                            _fadeFlag = true;
                            break;
                        case (int)NEXT_STATE.NO:
                            _popFlag = false;
                            _nextFlag = false;
                            _ready.GetComponent<Image>().enabled = false;
                            _backMenu.GetComponent<Image>().enabled = false;
                        break;
                    }
            }

            if(_controller.ButtonDown(Button.B))
            {
                _audioSource.PlayOneShot(_cansel);
                _popFlag = false;
                _nextFlag = false;
                _ready.GetComponent<Image>().enabled = false;
                _backMenu.GetComponent<Image>().enabled = false;
            }

        }

        //確認場面
        if (_popFlag == true)
        {
            PopZoom();
            _nextFlag = true;
        }
        else
        {
            PopBack();
        }

        //バーのフラグ
        if(_barFlag == true)
        {
            BarZoom(_bar);
            _mainFlag = true;
        }
        else
        {
            _mainState = 0;
            ZoomBack(_bar);
            _mainFlag = false;
        }

        //モデルのバーフラグ
        if(_modelFlag == true)
        {
            BarZoom(_modelBar);
            _mainFlag = true;
        }
        else if(_modelFlag == false && _barFlag == false)
        {
            _mainState = 0;
            ZoomBack(_modelBar);
            _mainFlag = false;
        }

        //startが押された時ポップを表示させる
        if (_controller.ButtonDown(Button.START))
        {
            if (_playerSystem.GetComponent<PlayerSystem>().getMain1() != "Main1" && _playerSystem.GetComponent<PlayerSystem>().getMain2() != "Main2")
            {
                _popFlag = true;
                _selectFlag = false;
            }
        }

        if (_fadeOut.GetComponentInChildren<Fade>().isCheckedFadeOut() && _fadeFlag == true)
        {
            if (_selectFlag == false)
            {
                _sceneNextFlag = true;
            }
            else if(_selectFlag == true)
            {
                _backFlag = true;
            }
        }



        //デバッグ終了キー
        if (Input.GetKey(KeyCode.Escape))
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #elif UNITY_STANDALONE
            Application.Quit();
            #endif
        }

    }

    //選択しているボタンが押された時
    private void ChooseMenu(EQUIPMENT_STATE d)
    {
        switch (d)
        {
            case EQUIPMENT_STATE.MODEL:
                _modelBar.position = _modelImage.position + new Vector3(151, 21, 0);
                _modelFlag = true;
                break;
            case EQUIPMENT_STATE.MAIN_WEAPON1:
                _bar.position = _modelImage.position + new Vector3(151,21,0);
                _weaponState = 0;
                _barFlag = true;
                break;
            case EQUIPMENT_STATE.MAIN_WEAPON2:
                _bar.position = _modelImage.position + new Vector3(151, 21, 0);
                _weaponState = 1;
                _barFlag = true;
                break;
        }
    }

    //バーの拡大
    private void BarZoom(RectTransform rect)
    {
        if (rect.localScale.y > -2.5)
        {
            rect.localScale += new Vector3(0, -0.3f, 0);
        }
    }

    private void ZoomBack(RectTransform rect)
    {
        if (rect.localScale.y < 0)
        {
            rect.localScale = new Vector3(1.5f, 0, 1);
        }
    }

    //Popの拡縮
    private void PopZoom()
    {
        if(_pop.localScale.y < 0.3f)
        {
            _pop.localScale += new Vector3(0.3f, 0.05f, 0.1f);
        }
    }

    private void PopBack()
    {
        _pop.localScale = new Vector3(0, 0, 0);
    }

    //csvファイルから読み込んだ武器名をリストに入れる関数
    private void WeaponAdd(string name,List<string[]> list)
    {
        string filename = name;

        var csvFile = Resources.Load("CSV/" + filename) as TextAsset;

        //csvの内容をStringReaderに変換
        var reader = new StringReader(csvFile.text);

        //csvの内容を最後まで取得
        while(reader.Peek() > -1)
        {
            //1行読む
            var lineData = reader.ReadLine();

            //カンマ区切りのデータを文字列の配列に変換
            var weaponName = lineData.Split(',');

            //リストに追加
            list.Add(weaponName);

            _weaponNum++;
        }

    }

    private void ModelAdd(string name, List<string[]> list)
    {
        string filename = name;

        var csvFile = Resources.Load("CSV/" + filename) as TextAsset;

        //csvの内容をStringReaderに変換
        var reader = new StringReader(csvFile.text);

        //csvの内容を最後まで取得
        while (reader.Peek() > -1)
        {
            //1行読む
            var lineData = reader.ReadLine();

            //カンマ区切りのデータを文字列の配列に変換
            var weaponName = lineData.Split(',');

            //リストに追加
            list.Add(weaponName);

            _modelNum++;
        }

    }

    //選択場面のステート
    private int ChooseState(int state)
    {
        int _state = state;

        if(_controller.CheckDirectionOnce(Direction.Left, StickType.LEFTSTICK))
        {
            _audioSource.PlayOneShot(_select);
            _state -= 1;
        }
        else if(_controller.CheckDirectionOnce(Direction.Right, StickType.LEFTSTICK))
        {
            _audioSource.PlayOneShot(_select);
            _state += 1;
        }
        else if (_controller.CheckDirectionOnce(Direction.Front, StickType.LEFTSTICK))
        {
            _audioSource.PlayOneShot(_select);
            _state -= 1;
        }
        else if (_controller.CheckDirectionOnce(Direction.Back, StickType.LEFTSTICK))
        {
            _audioSource.PlayOneShot(_select);
            _state += 1;
        }

        return _state;
    }

    //次のシーンへ移動するフラグ
    public bool GetNextFlag()
    {
        return _sceneNextFlag;
    }

    public bool GetBackFlag()
    {
        return _backFlag;
    }

}

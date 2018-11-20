using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class Equipment : MonoBehaviour {

    //フェード
    GameObject _fadeOut;

    //コントローラのスクリプト
    private GameController _controller;

    //Bar
    private RectTransform _bar;

    //カーソル
    private RectTransform _cursor;

    private RectTransform _mainWeapon1;
    
    private RectTransform _mainWeapon2;
    
    private RectTransform _subWeapon;
    
    private RectTransform _start;

    //ポップ
    
    private RectTransform _pop;

    //stateの値
    [SerializeField]
    private int _state = 0;

    //
    [SerializeField]
    private int _mainState = 0;

    [SerializeField]
    private Vector3 _pos = new Vector3(-200, 0, 0);

    //
    [SerializeField]
    private GameObject canvas;

    //PlayerSystem
    [SerializeField]
    private GameObject _playerSystem;

    //Barのフラグ
    private bool _barFlag = false;

    //メインのフラグ
    private bool _mainFlag = false;

    //武器リスト
    private List<string[]> _weaponList = new List<string[]>();

    //サブ武器リスト
    private List<string[]> _subWeaponList = new List<string[]>();

    //武器の画像リスト
    private RectTransform[] _weaponImage = new RectTransform[5];

    //サブ武器の画像リスト
    private RectTransform[] _subWeaponImage = new RectTransform[5];

    //
    [SerializeField]
    private RectTransform _cusor2;

    //
    private RectTransform _cusor3;

    //
    private RectTransform _yes;

    private RectTransform _no;

    //武器数
    private int _weaponNum = 0;

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


    //シーン移動のフラグ
    private bool _sceneNextFlag = false;

    //どこを選んでいるかのState
    private enum EQUIPMENT_STATE
    {
        MAIN_WEAPON1,
        MAIN_WEAPON2,
        //SUB_WEAPON,
        NEXT
    }

    //武器一覧
    private enum WEAPON_STATE
    {
        WEAPON1,
        WEAPON2
    }

    //装備のところ
    private enum WEAPON_MAIN
    {
        MAIN1,
        MAIN2,
        SUB
    }

    [SerializeField]
    private int _weaponState = 0;

    private bool _nextFlag = false;

    private bool _popFlag = false;

    private int _nextState = 0;

    //fade終わったかのフラグ
    private bool _fadeFlag = false;


    private enum NEXT_STATE
    {
        YES,
        NO
    }

    IEnumerator GeneratePulseNoise(RectTransform rec)
    {
        for (int i = 0; i <= 180; i += 30)
        {
            rec.GetComponent<Image>().material.SetFloat("_Amount", 0.2f * Mathf.Sin(i * Mathf.Deg2Rad));
            yield return null;
        }
    }

    // Use this for initialization
    void Start () {

        Fade fade = new Fade();

        _fadeOut = fade.CreateFade();

        _fadeOut.GetComponentInChildren<Fade>().FadeIn();

        //初期化
        _controller = GameController.Instance;
        _sceneNextFlag = false;

        //audioコンポーネント
        _audioSource = gameObject.GetComponent<AudioSource>();

        //コンポーネント
        _cursor = GameObject.Find("Cursor").GetComponent<RectTransform>();
        _mainWeapon1 = GameObject.Find("MainWeapon1").GetComponent<RectTransform>();
        _mainWeapon2 = GameObject.Find("MainWeapon2").GetComponent<RectTransform>();
        //_subWeapon = GameObject.Find("SubWeapon").GetComponent<RectTransform>();
        _start = GameObject.Find("Start").GetComponent<RectTransform>();

        _bar = GameObject.Find("Bar").GetComponent<RectTransform>();

        _pop = GameObject.Find("Pop").GetComponent<RectTransform>();

        _yes = GameObject.Find("Yes").GetComponent<RectTransform>();

        _no = GameObject.Find("No").GetComponent<RectTransform>();

        //popのスケールは最初は0にする
        _pop.localScale = new Vector3(0, 0, 0);

        //最初バーのスケールは0にする
        _bar.localScale = new Vector3(1.5f, 0, 1);

        //
        _cursor.position = _mainWeapon1.position;

        //メイン武器のリスト作成
        WeaponAdd("WeaponList", _weaponList);

        //サブ武器のリスト作成
        //WeaponAdd("SubWeaponList", _subWeaponList);

        _playerSystem = GameObject.Find("PlayerSystem");

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


        //サブ武器の部分
        //for(int i = 0; i < _weaponNum; i++)
        //{
        //    //Resources/Imagesから一致するものを探してくる
        //    GameObject img = (GameObject)Instantiate(Resources.Load("Images/" + _subWeaponList[i][0].ToString()));
        //    img.transform.SetParent(canvas.transform, false);
        //    img.GetComponent<WeaponName>().setName(_subWeaponList[i][0].ToString());
        //    _subWeaponImage[i] = img.GetComponent<RectTransform>();

        //    rePos = canvas.GetComponent<RectTransform>().position;

        //    _subWeaponImage[i].localPosition = new Vector3(50, 20 * (i + 1), 0);
        //}

        GameObject cusor = (GameObject)Instantiate(Resources.Load("Images/Cusor2"));

        cusor.transform.SetParent(canvas.transform, false);

        _cusor2 = cusor.GetComponent<RectTransform>();

        rePos = _weaponImage[0].localPosition;

        _cusor2.localPosition = rePos;

     
        _cusor2.transform.rotation = Quaternion.Euler(180.0f, 0.0f, 0.0f); ;

        rePos = _yes.localPosition;

        _model = GameObject.Find("PlayerModel");



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

                if (_state < (int)EQUIPMENT_STATE.MAIN_WEAPON1)
                {
                    _state = (int)EQUIPMENT_STATE.NEXT;
                }

                if (_state > (int)EQUIPMENT_STATE.NEXT)
                {
                    _state = (int)EQUIPMENT_STATE.MAIN_WEAPON1;
                }                

                //メインからNextまでの選択
                switch (_state)
                {
                    case (int)EQUIPMENT_STATE.MAIN_WEAPON1:
                        StartCoroutine(GeneratePulseNoise(_mainWeapon1));
                        _cursor.position = _mainWeapon1.position;
                        break;
                    case (int)EQUIPMENT_STATE.MAIN_WEAPON2:
                        StartCoroutine(GeneratePulseNoise(_mainWeapon2));
                        _cursor.position = _mainWeapon2.position;
                        break;
                    //case (int)EQUIPMENT_STATE.SUB_WEAPON:
                    //    StartCoroutine(GeneratePulseNoise(_subWeapon));
                    //    _cursor.position = _subWeapon.position;
                    //    break;
                    case (int)EQUIPMENT_STATE.NEXT:
                        _cursor.position = _start.position;
                        break;

                }

                if (_controller.ButtonDown(Button.A) && _barFlag == false)
                {
                    ChooseMenu((EQUIPMENT_STATE)_state);
                    _audioSource.PlayOneShot(_decision);
                }

            }
            else
            { 
                //コントローラ操作
                _mainState = ChooseState(_mainState);
               
                if (_mainState < (int)WEAPON_STATE.WEAPON1)
                {
                    _mainState = (int)WEAPON_STATE.WEAPON2;
                }

                if (_mainState > (int)WEAPON_STATE.WEAPON2)
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
                    }
                }
                else if (_weaponState == 2)
                {

                }


                if (_controller.ButtonDown(Button.A))
                {
                    _barFlag = false;
                    _audioSource.PlayOneShot(_decision);
                    switch (_weaponState)
                    {
                        case (int)WEAPON_MAIN.MAIN1:
                            //選んだ武器を装備
                            _playerSystem.GetComponent<PlayerSystem>().setMain1(_weaponList[_mainState][0].ToString());
                            _mainWeapon1.GetComponent<WeaponName>().setName(_weaponList[_mainState][0].ToString());
                            _model.GetComponent<WeaponEquipment>().setWeapon1(_weaponList[_mainState][0].ToString());
                            break;
                        case (int)WEAPON_MAIN.MAIN2:
                            //選んだ武器を装備
                            _playerSystem.GetComponent<PlayerSystem>().setMain2(_weaponList[_mainState][0].ToString());
                            _mainWeapon2.GetComponent<WeaponName>().setName(_weaponList[_mainState][0].ToString());
                            _model.GetComponent<WeaponEquipment>().setWeapon2(_weaponList[_mainState][0].ToString());
                            break;
                        //case (int)WEAPON_MAIN.SUB:
                        //    //サブ武器の装備
                        //    break;
                    }

                }

                if (_controller.ButtonDown(Button.B))
                {
                    _audioSource.PlayOneShot(_cansel);
                    _barFlag = false;
                }

            }
        }
        else
        {
            //コントローラ操作

            _nextState = ChooseState(_nextState);

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

            if (_controller.ButtonDown(Button.A))
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
                        break;
                }

            }

            if(_controller.ButtonDown(Button.B))
            {
                _audioSource.PlayOneShot(_cansel);
                _popFlag = false;
                _nextFlag = false;
            }

        }

        //確認場面
        if(_popFlag == true)
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
            BarZoom();
            _mainFlag = true;
        }
        else
        {
            _mainState = 0;
            ZoomBack();
            _mainFlag = false;
        }

        if(_fadeOut.GetComponentInChildren<Fade>().isCheckedFadeOut() && _fadeFlag == true)
        {
            _sceneNextFlag = true;
        }

    }

    //選択しているボタンが押された時
    private void ChooseMenu(EQUIPMENT_STATE d)
    {
        switch (d)
        {
            case EQUIPMENT_STATE.MAIN_WEAPON1:
                _bar.position = _mainWeapon1.position + new Vector3(151,21,0);
                _weaponState = 0;
                _barFlag = true;
                break;

            case EQUIPMENT_STATE.MAIN_WEAPON2:
                _bar.position = _mainWeapon2.position + new Vector3(151, 21, 0);
                _weaponState = 1;
                _barFlag = true;
                break;

            //case EQUIPMENT_STATE.SUB_WEAPON:
            //    _bar.position = _subWeapon.position + new Vector3(151, 21, 0);
            //    _weaponState = 2;
            //    _barFlag = true;
            //    break;
            case EQUIPMENT_STATE.NEXT:
                //Popの表示
                _popFlag = true;
                break;
               
        }

    }


    //バーの拡大
    private void BarZoom()
    {
        if (_bar.localScale.y > -2.5)
        {
            _bar.localScale += new Vector3(0, -0.3f, 0);
        }
    }

    private void ZoomBack()
    {
        if (_bar.localScale.y < 0)
        {
            _bar.localScale = new Vector3(1.5f, 0, 1);
        }
    }

    //Popの拡縮
    private void PopZoom()
    {
        if(_pop.localScale.y < 0.5f)
        {
            _pop.localScale += new Vector3(0.4f, 0.1f, 0.1f);
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



}

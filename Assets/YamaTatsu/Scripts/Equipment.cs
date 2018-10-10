﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Equipment : MonoBehaviour {

    //コントローラのスクリプト
    private GameController _controller;

    //Bar
    [SerializeField]
    private RectTransform _bar;

    //カーソル
    [SerializeField]
    private RectTransform _cursor;

    [SerializeField]
    private RectTransform _mainWeapon1;
    [SerializeField]
    private RectTransform _mainWeapon2;
    [SerializeField]
    private RectTransform _subWeapon;
    [SerializeField]
    private RectTransform _next;

    //stateの値
    [SerializeField]
    private int _state = 0;

    //
    [SerializeField]
    private int _mainState = 0;

    [SerializeField]
    private Vector3 _pos = new Vector3(-200, 0, 0);

    //Barのフラグ
    private bool _barFlag = false;

    //メインのフラグ
    private bool _mainFlag = false;

    //武器一覧のフラグ
    private bool _WeaponFlag = false;

    //武器リスト
    private List<string[]> _weaponList = new List<string[]>();

    //サブ武器リスト
    private List<string[]> _subWeaponList = new List<string[]>();

    //武器の画像リスト
    private RectTransform[] _weaponImage;

    //武器数
    private int _weaponNum = 0;

    //どこを選んでいるかのState
    private enum EQUIPMENT_STATE
    {
        MAIN_WEAPON1,
        MAIN_WEAPON2,
        SUB_WEAPON,
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


	// Use this for initialization
	void Start () {

        //初期化
        _controller = GameController.Instance;

        //コンポーネント
        _cursor = GameObject.Find("Cursor").GetComponent<RectTransform>();
        _mainWeapon1 = GameObject.Find("MainWeapon1").GetComponent<RectTransform>();
        _mainWeapon2 = GameObject.Find("MainWeapon2").GetComponent<RectTransform>();
        _subWeapon = GameObject.Find("SubWeapon").GetComponent<RectTransform>();
        _next = GameObject.Find("Next").GetComponent<RectTransform>();

        _bar = GameObject.Find("Bar").GetComponent<RectTransform>();

        //最初バーのスケールは0にする
        _bar.localScale = new Vector3(3, 0, 1);

        //
        _cursor.position = _mainWeapon1.position + _pos;

        //メイン武器のリスト作成
        WeaponAdd("WeaponList", _weaponList);

        for(int i = 0; 0 < _weaponNum; i++)
        {
            Debug.Log(_weaponList[i].ToString());
            _weaponImage[i] = (RectTransform)Resources.Load("Images/" + _weaponList[i].ToString());
        }

    }
	
	// Update is called once per frame
	void Update () {

        _controller.ControllerUpdate();

        //どこを選択しているかのState
        if (_mainFlag == false)
        {
            if (_controller.OneShotMove(Direction.Front))
            {
                _state -= 1;
                if (_state < (int)EQUIPMENT_STATE.MAIN_WEAPON1)
                {
                    _state = (int)EQUIPMENT_STATE.NEXT;
                }
            }
            else if (_controller.OneShotMove(Direction.Back))
            {
                _state += 1;
                if (_state > (int)EQUIPMENT_STATE.NEXT)
                {
                    _state = (int)EQUIPMENT_STATE.MAIN_WEAPON1;
                }
            }

            switch (_state)
            {
                case (int)EQUIPMENT_STATE.MAIN_WEAPON1:
                    _cursor.position = _mainWeapon1.position + _pos;
                    break;
                case (int)EQUIPMENT_STATE.MAIN_WEAPON2:
                    _cursor.position = _mainWeapon2.position + _pos;
                    break;
                case (int)EQUIPMENT_STATE.SUB_WEAPON:
                    _cursor.position = _subWeapon.position + _pos;
                    break;
                case (int)EQUIPMENT_STATE.NEXT:
                    _cursor.position = _next.position + _pos;
                    break;

            }

            if (_controller.ButtonDown(Button.A) && _barFlag == false)
            {
                ChooseMenu((EQUIPMENT_STATE)_state);
            }

        }
        else
        {
            switch (_mainState)
            {
                case (int)WEAPON_STATE.WEAPON1:
                    break;

                case (int)WEAPON_STATE.WEAPON2:
                    break;
            }


            if (_controller.ButtonDown(Button.A))
            {
                _barFlag = false;
    
                //選んだ武器を装備
            }
        }


        if(_barFlag == true)
        {
            BarZoom();
            _mainFlag = true;
        }
        else
        {
            ZoomBack();
            _mainFlag = false;
        }

    }

    //選択しているボタンが押された時
    private void ChooseMenu(EQUIPMENT_STATE d)
    {
        switch (d)
        {
            case EQUIPMENT_STATE.MAIN_WEAPON1:
                _bar.position = _mainWeapon1.position + new Vector3(151,21,0);
                _barFlag = true;
                break;

            case EQUIPMENT_STATE.MAIN_WEAPON2:
                _bar.position = _mainWeapon2.position + new Vector3(151, 21, 0);
                _barFlag = true;
                break;

            case EQUIPMENT_STATE.SUB_WEAPON:
                _bar.position = _subWeapon.position + new Vector3(151, 21, 0);
                _barFlag = true;
                break;
            case EQUIPMENT_STATE.NEXT:
                //シーン移動
                break;
               
        }

    }

    //選択した武器
    private void ChooseWeapon(WEAPON_STATE d)
    {
        switch (d)
        {
            case WEAPON_STATE.WEAPON1:
                //武器をセットする
                break;

            case WEAPON_STATE.WEAPON2:
                //武器をセットする
                break;
        }

    }

    //バーの拡大
    private void BarZoom()
    {
        if (_bar.localScale.y > -5)
        {
            _bar.localScale += new Vector3(0, -0.3f, 0);
        }
    }

    private void ZoomBack()
    {
        if (_bar.localScale.y < 0)
        {
            _bar.localScale = new Vector3(3, 0, 1);
        }
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


        Debug.Log(list[0].ToString());

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHideText : MonoBehaviour
{
    //　定数
    private const float MAX_ALFA = 1.0f;    //　最大アルファ値
    private const float MIN_ALFA = 0.0f;    //　最小アルファ値

    [SerializeField]
    private float _speed = 0.01f;           //　透明化の速さ
    private float _alfa;                    //  A値を操作するための変数
    private Vector3 _color;                 //  X：赤、Y：緑、Z：青

    private bool _isShow = false;           //　Show終了フラグ
    private bool _isHide = false;           //　Hide終了フラグ

    // Use this for initialization
    void Start()
    {//　フェードするオブジェクトの色を初期化
        _color.x = GetComponent<Text>().color.r;
        _color.y = GetComponent<Text>().color.g;
        _color.z = GetComponent<Text>().color.b;
        _alfa = GetComponent<Text>().color.a;

        _isHide = true;
    }

    // Update is called once per frame
    void Update ()
    {
        //　薄くする
        if (_isHide)
        {
            GetComponent<Text>().color = new Color(_color.x, _color.y, _color.z, _alfa);
            _alfa -= _speed;

            if (_alfa <= MIN_ALFA)
            {
                _isHide = false;
                _isShow = true;
            }
        }
        //　濃くする
        if (_isShow)
        {
            GetComponent<Text>().color = new Color(_color.x, _color.y, _color.z, _alfa);
            _alfa += _speed;

            if (_alfa >= MAX_ALFA)
            {
                _isShow = false;
                _isHide = true;
            }
        }
    }
}

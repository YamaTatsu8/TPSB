using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    //　定数
    private const float MAX_ALFA = 1.0f;
    private const float MIN_ALFA = 0.0f;

    [SerializeField]
    private float _speed = 0.02f;   //　透明化の速さ
    private float _alfa;            //  A値を操作するための変数
    private Vector3 _color;         //  X：赤、Y：緑、Z：青

    private bool _isFadeIn = false;
    private bool _isFadeOut = false;

    // Use this for initialization
    void Start()
    {
        _color.x = GetComponent<Image>().color.r;
        _color.y = GetComponent<Image>().color.g;
        _color.z = GetComponent<Image>().color.b;
        _alfa = GetComponent<Image>().color.a;
    }

    // Update is called once per frame
    void Update()
    {
        //　フェードインかフェードアウトのフラグが立っていれば開始する
        if (_isFadeIn || _isFadeOut)
        {
            FadeUpdate();
        }
    }

    //　フェード更新処理
    private void FadeUpdate()
    {
        //　フェードインを更新する
        if(_isFadeIn)
        {
            GetComponent<Image>().color = new Color(_color.x, _color.y, _color.z, _alfa);
            _alfa -= _speed;

            if (_alfa <= MIN_ALFA)
            {
                _isFadeIn = false;
            }
        }
        //　フェードアウト更新する
        if (_isFadeOut)
        {
            GetComponent<Image>().color = new Color(_color.x, _color.y, _color.z, _alfa);
            _alfa += _speed;

            if (_alfa >= MAX_ALFA)
            {
                _isFadeOut = false;
            }
        }
    }

    //　フェードインを開始する
    public void FadeIn()
    {
        //　アルファ値を最大にする
        _alfa = MAX_ALFA;
        GetComponent<Image>().color = new Color(_color.x, _color.y, _color.z, _alfa);
        _isFadeIn = true;
    }

    //　フェードアウトを開始する
    public void FadeOut()
    {
        //　アルファ値を最小にする
        _alfa = MIN_ALFA;
        GetComponent<Image>().color = new Color(_color.x, _color.y, _color.z, _alfa);
        _isFadeOut = true;
    }

    // フェードインかフェードアウトが終わったかチェック
    public bool isCheckedFadeIn()
    {
        if (!_isFadeIn)
        {
            // 終わった
            return true;
        }
        //　終わってない
        return false;
    }
    public bool isCheckedFadeOut()
    {
        if (!_isFadeOut)
        {
            return true;
        }
        return false;
    }

    public GameObject CreateFade()
    {
        GameObject obj = (GameObject)Instantiate(Resources.Load("Prefabs/FadeCanvas"));
        return obj;
    }
}

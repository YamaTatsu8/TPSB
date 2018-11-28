using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    //　定数
    private const float MAX_ALFA = 1.0f;    //　最大アルファ値
    private const float MIN_ALFA = 0.0f;    //　最小アルファ値

    [SerializeField]
    private float _speed = 0.02f;           //　透明化の速さ
    private float _alfa;                    //  A値を操作するための変数
    private Vector3 _color;                 //  X：赤、Y：緑、Z：青

    private bool _isFadeIn = false;         //　フェードイン終了フラグ
    private bool _isFadeOut = false;        //　フェードアウト終了フラグ

    private GameObject _emptyObj;           //　空オブジェクト

    // Use this for initialization
    void Start()
    {//　フェードするオブジェクトの色を初期化
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

    /// <summary>
    /// フェード更新処理
    /// </summary>
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

    /// <summary>
    /// 　フェードインを開始する
    /// </summary>
    public void FadeIn()
    {
        //　アルファ値を最大にする
        _alfa = MAX_ALFA;
        GetComponent<Image>().color = new Color(_color.x, _color.y, _color.z, _alfa);
        _isFadeIn = true;
    }

    /// <summary>
    /// フェードアウトを開始する
    /// </summary>
    public void FadeOut()
    {
        //　アルファ値を最小にする
        _alfa = MIN_ALFA;
        GetComponent<Image>().color = new Color(_color.x, _color.y, _color.z, _alfa);
        _isFadeOut = true;
    }

    /// <summary>
    /// フェードインが終了したかチェックする
    /// </summary>
    /// <returns>true:終わった, false:終わってない</returns>
    public bool isCheckedFadeIn()
    {
        if (!_isFadeIn)
        {//　終わった
            return true;
        }
        //　終わってない
        return false;
    }
    /// <summary>
    /// フェードアウトが終了したかチェックする
    /// </summary>
    /// <returns>true:終わった, false:終わってない</returns>
    public bool isCheckedFadeOut()
    {
        if (!_isFadeOut)
        {//　終わった
            return true;
        }
        //　終わってない
        return false;
    }

    /// <summary>
    /// フェードするオブジェクトを生成する
    /// </summary>
    /// <returns>GameObject:生成したフェードオブジェクト</returns>
    public GameObject CreateFade()
    {
        if (_emptyObj == null)
        {
            _emptyObj = (GameObject)Instantiate(Resources.Load("Prefabs/FadeCanvas"));
        }
        return _emptyObj;
    }
}

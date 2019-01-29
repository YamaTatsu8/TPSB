using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleGradation : MonoBehaviour
{
    //　定数
    private const float MAX_RGB = 1.0f;         //　最大RGB値
    private const float MIN_RGB = 0.0f;         //　最小RGB値

    [SerializeField]
    private float _speed = 0.01f;               //　透明化の速さ
    private float _alfa;                        //  a値を操作するための変数
    private Vector3 _color;                     //  X：赤、Y：緑、Z：青

    private bool _isToBlue = false;             //　青色に変色終了フラグ

    // Use this for initialization
    void Start()
    {//　色変化するオブジェクトの色を初期化
        _color.x = GetComponent<Image>().color.r;
        _color.y = GetComponent<Image>().color.g;
        _color.z = GetComponent<Image>().color.b;
        _alfa = GetComponent<Image>().color.a;

        _isToBlue = true;
    }

    // Update is called once per frame
    void Update()
    {
        //　青色にする
        if (_isToBlue)
        {
            ToBlue();
        }
        //　黄色にする
        if (!_isToBlue)
        {
            ToYellow();
        }
    }

    /// <summary>
    /// 赤色を足す
    /// </summary>
    /// <returns>true:MAX赤　false:ゼロ赤</returns>
    private bool AddRed()
    {
        if (_color.x >= MAX_RGB)
        {
            return true;
        }
        _color.x += _speed;
        GetComponent<Image>().color = new Color(_color.x, _color.y, _color.z, _alfa);
        return false;
    }
    /// <summary>
    /// 緑色を足す
    /// </summary>
    /// <returns>true:MAX緑　false:ゼロ緑</returns>
    private bool AddGreen()
    {
        if (_color.y >= MAX_RGB)
        {
            return true;
        }
        _color.y += _speed;
        GetComponent<Image>().color = new Color(_color.x, _color.y, _color.z, _alfa);
        return false;
    }
    /// <summary>
    /// 青色を足す
    /// </summary>
    /// <returns>true:MAX青　false:まだ青</returns>
    private bool AddBlue()
    {
        if (_color.z >= MAX_RGB)
        {
            return true;
        }
        _color.z += _speed;
        GetComponent<Image>().color = new Color(_color.x, _color.y, _color.z, _alfa);
        return false;
    }
    /// <summary>
    /// 赤色を減らす
    /// </summary>
    /// <returns>true:ゼロ赤 false:まだ赤い</returns>
    private bool ReduceRed()
    {
        if (_color.x <= MIN_RGB)
        {
            return true;
        }
        _color.x -= _speed;
        GetComponent<Image>().color = new Color(_color.x, _color.y, _color.z, _alfa);
        return false;
    }
    /// <summary>
    /// 青色を減らす
    /// </summary>
    /// <returns>true:ゼロ青　false:まだ青い</returns>
    private bool ReduceGreen()
    {
        if (_color.y <= MIN_RGB)
        {
            return true;
        }
        _color.y -= _speed;
        GetComponent<Image>().color = new Color(_color.x, _color.y, _color.z, _alfa);
        return false;
    }
    /// <summary>
    /// 緑を減らす
    /// </summary>
    /// <returns>true:ゼロ緑　false:まだ緑</returns>
    private bool ReduceBlue()
    { 
        if (_color.z <= MIN_RGB)
        {
            return true;
        }
        _color.z -= _speed;
        GetComponent<Image>().color = new Color(_color.x, _color.y, _color.z, _alfa);
        return false;
    }

    /// <summary>
    /// 青色にする
    /// </summary>
    private void ToBlue()
    {
        if (ReduceRed()) 
        {
            if (AddBlue())
            {
                if (ReduceGreen())
                {
                    _isToBlue = false;
                }
            }
        }
    }
    /// <summary>
    /// 黄色にする
    /// </summary>
    private void ToYellow()
    {
        if (AddGreen())
        {
            if (ReduceBlue())
            {
                if (AddRed())
                {
                    _isToBlue = true;
                }
            }
        }
    }
}

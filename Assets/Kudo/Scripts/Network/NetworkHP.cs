using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour {

    private Image gage;

    //HPの容量
    private float _HP = 100;

    //
    [SerializeField]
    private float _MAX_HP = 100;

    //セッター
    private float _setEP = 0;

    private float _fillProp = 1.0f;

    [SerializeField]
    private GameObject _obj;

    // Use this for initialization
    void Start()
    {
        gage = transform.Find("HP").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        _HP = _obj.GetComponent<Status>().getHP();
        //gageの描画の変更
        gage.fillAmount = (_HP / _MAX_HP) * _fillProp;
    }

    //
    public void UseHp(float hp)
    {
        _HP -= hp;
    }

    public void SetHP(float hp)
    {
        _HP = hp;
    }

    public void get()
    {
        Debug.Log(_HP);
    }

}

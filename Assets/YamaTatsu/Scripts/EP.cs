using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EP : MonoBehaviour {

    private Image gage;

    //EPの容量
    private float _EP = 1000;

    //EPの最大値
    [SerializeField]
    private float MAX_EP = 1000;

    //セッター
    private float _setEP = 0;

    //フラグ
    private bool _flag = false;

    //リカバリーフラグ
    private bool _reFlag = false;

    //ブーストできるかのフラグ
    private bool _boostFlag = true;

    private float _fillProp = 1.0f;

    private bool _epFlag = false;

    // Use this for initialization
    void Start () {

        gage = null;

	}
	
	// Update is called once per frame
	void Update () {


        if(_epFlag == false)
        {
            _epFlag = true;
            gage = transform.Find("ep").GetComponent<Image>();
        }

        //　0になったらfalseにし使えなくする
        if(_EP < 0)
        {
            Debug.Log("EPが0になりました");
            _boostFlag = false;
        }
        else
        {
            _boostFlag = true;
        }
       
        //gageの描画の変更
        gage.fillAmount = (_EP/ MAX_EP) * _fillProp;

    }

    //
    public void UseEp(float ep)
    {
        _EP -= ep;
    }

    public void get()
    {
        Debug.Log(_EP);
    }

    public void RecoveryEP(float ep)
    {
        //MAXの値より低かったら回復する
        if (_EP < MAX_EP)
        {
            _EP += ep;
        }
    }

    public bool getBoostFlag()
    {
        return _boostFlag;
    }

}

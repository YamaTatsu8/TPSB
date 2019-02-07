﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkStatus : MonoBehaviour {

    //　HP
    [SerializeField]
    private float _HP = 100;

    //アニメーター
    private Animator _animator;

    [SerializeField]
    private GameObject _obj;

    private PhotonView _photonView;

    private GameObject _hitObj;
    private bool _isEnemy;
    private int _hitNum;
    private string _hitStr;
    private float _time;

    bool _deadFlag = false;

    // Use this for initialization
    void Start () {

        _animator = _obj.GetComponent<Animator>();

        if (this.gameObject.name == "TrainingEnemy")
        {
            _isEnemy = true;
            _hitNum = 0;
            _time = 3.0f;
            _hitStr = " H I T";
            _hitObj = GameObject.Find("HitCnt");
            _hitObj.SetActive(false);
        }
        else { _isEnemy = false; }

        _photonView = GetComponent<PhotonView>();
    }
	
	// Update is called once per frame
	void Update () {
	    
        // HPが0以下の場合破壊
        if(!_deadFlag && _HP < 0)
        {
            _deadFlag = true;
            NetworkPlayScene.Instance.Search(_photonView.isMine);

            Destroy(this.gameObject);
        }

        if (_HP >= 100)
        {
            _HP = 100;
        }

        if (_hitObj == null) { return; }
        if (!_hitObj.activeSelf) { return; }
        _time -= Time.deltaTime;
        if (_time <= 0.0f)
        {
            _hitNum = 0;
            _time = 3.0f;
            _HP = 100;
            _hitObj.SetActive(false);
        }

        _animator.SetTrigger("Idle");
    }

    public bool getDestroy()
    {
        return true;
    }

    //ダメージを与える処理
    public void hitDamage(int damage)
    {
        //　ターゲットが敵の場合
        if (_isEnemy)
        {
            if (!_hitObj.activeSelf) { _hitObj.SetActive(true); }
            _hitNum++;
            _hitObj.GetComponent<Text>().text = _hitNum.ToString() + _hitStr;
            _time = 1.0f;

            float hp = _HP;
            hp -= damage;
            if (hp >= 1)
            {
                _HP -= damage;
            }
        }
        else
        {
            _HP -= damage;
        }
        //_animator.SetTrigger("Damage");
    }

    public void RecoveryHP(int heal)
    {
        Debug.Log("回復");
        _HP += heal;
    }

    public float getHP()
    {
        return (float)_HP;
    }
  
}

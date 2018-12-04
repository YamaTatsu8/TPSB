﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour {

    //　HP
    [SerializeField]
    private float _HP = 100;

    //アニメーター
    private Animator _animator;

    [SerializeField]
    private GameObject _obj;

    [SerializeField]
    private string _name = "unity";

    // Use this for initialization
    void Start () {

        _obj = GameObject.Find(_name);

        _obj.SetActive(true);

        _animator = _obj.GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {
	
        // HPが0以下の場合破壊
        if(_HP < 0)
        {
            Destroy(this.gameObject);
        }

        //_animator.SetBool("Damage", false);
    }

    public bool getDestroy()
    {
        return true;
    }

    //ダメージを与える処理
    public void hitDamage(int damage)
    {
        _HP -= damage;
        //_animator.SetBool("Damage", true);
    }

    public float getHP()
    {
        return (float)_HP;
    }
  
}

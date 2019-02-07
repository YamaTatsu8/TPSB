﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkBarrier : MonoBehaviour {

    //コントローラー
    GameController controller;

    private GameObject _object;

    //バるあフラグ
    private bool _flag = false;

    //生成してるかどうか
    private bool _reFlag = false;

    //リミット
    [SerializeField]
    private float _limitTime = 2.0f;

    //HP
    [SerializeField]
    private int _HP = 10;

    //アニメーター
    private Animator _animator;

    [SerializeField]
    private GameObject _obj;

    //Guardのオブジェクト
    [SerializeField]
    private GameObject _guard;

    //展開
    private bool _barrier = false;

    // -PhotonView
    private PhotonView _photonView;

    // Use this for initialization
    void Start () {

        controller = GameController.Instance;

        _animator = _obj.GetComponent<Animator>();

        _guard = GameObject.Find("Guard");

        _guard.SetActive(false);

        // -PhotonViewのコンポーネント
        _photonView = GetComponent<PhotonView>();
    }
	
	// Update is called once per frame
	void Update () {
        // -自身でなかったらreturn
        if(!_photonView.isMine)
        {
            return;
        }

        GameObject target = GameObject.FindGameObjectWithTag("Player");

        controller.ControllerUpdate();

        //左ショルダーボタンが押された時
        if (Input.GetButton("L1"))
        {
            _guard.SetActive(true);
            _animator.SetBool("Guard", true);
            transform.LookAt(target.transform, Vector3.up);
            _guard.GetComponent<BoxCollider>().enabled = true;
            _barrier = true;
        }
        else
        {
            _guard.SetActive(false);
            _animator.SetBool("Guard", false);
            _guard.GetComponent<BoxCollider>().enabled = false;
            _barrier = false;
        }

        // 0になったらバリアを消す       
        if(_HP <= 0)
        {
            Destroy(_object);
        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "EnemyBullet" && _barrier == true)
        {
            Debug.Log("Barrier");
            _HP -= 1;
        }
    }

}

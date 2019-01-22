﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCamera : MonoBehaviour {

    //プレイヤーオブジェクト
    [SerializeField]
    private GameObject _player;

    //ターゲット
    private GameObject _target;

    //視界角度制限
    private const float ANGLE_LIMIT_UP = 60f;
    private const float ANGLE_LIMIT_DOWN = -60f;

    //offset
    private Vector3 _offset = new Vector3(0, 2, 0);

    [SerializeField]
    private GameObject target;

    //ロックオン解除時のオブジェ
    private GameObject _cameraObj;

    //コントローラのスクリプト
    GameController controller;

    //カメラ切り替え
    //右側
    private GameObject _right;
    //左側
    private GameObject _left;

    //
    [SerializeField]
    private GameObject _tps;

    //切り替え
    private bool _flag = true;

    //カメラフラグ
    private bool _cameraFlag = false;

    //ロックオン解除フラグ
    private bool _targetFlag;

    // Use this for initialization
    void Start () {

        controller = GameController.Instance;
    }
	
	// Update is called once per frame
	void Update () {

        if(_cameraFlag == false)
        {
            _cameraFlag = true;

            target = GameObject.FindGameObjectWithTag("Enemy");

            _cameraObj = GameObject.Find("CameraObj");

            _right = GameObject.Find("CameraRight");

            _left = GameObject.Find("CameraLeft");

            _tps = _right;

        }

        //R1押されたらロックオンの切り替え
        if(controller.ButtonDown(Button.R1))
        {
            _targetFlag = !_targetFlag;
        }

        //falseの場合、敵をロックする
        if (_targetFlag == false)
        {
            Vector3 pos;

            pos = (_tps.transform.position - target.transform.position);

            //playerのポジションに入れる
            transform.position = _tps.transform.position + pos.normalized * 3 + _offset;

            _target = target;

            if (_target)
            {
                LockOnTargetObjcet(_target);
            }
            float angle_x = 180f <= transform.eulerAngles.x ? transform.eulerAngles.x - 360 : transform.eulerAngles.x;
            transform.eulerAngles = new Vector3(
                Mathf.Clamp(angle_x, ANGLE_LIMIT_DOWN, ANGLE_LIMIT_UP),
                transform.eulerAngles.y,
                transform.eulerAngles.z);
        }
        else if(_targetFlag == true)
        {
            //trueの場合、フリールックカメラに切り替える
            Vector3 pos;

            

            pos = (_tps.transform.position - _cameraObj.transform.position);

            //playerのポジションに入れる
            transform.position = _tps.transform.position + pos.normalized * 3 + _offset;

            if (_cameraObj)
            {
                LockOnTargetObjcet(_cameraObj);
            }
            float angle_x = 180f <= transform.eulerAngles.x ? transform.eulerAngles.x - 360 : transform.eulerAngles.x;
            transform.eulerAngles = new Vector3(
                Mathf.Clamp(angle_x, ANGLE_LIMIT_DOWN, ANGLE_LIMIT_UP),
                transform.eulerAngles.y,
                transform.eulerAngles.z);
        }

        if (controller.ButtonDown(Button.R3))
        {
            _flag = !_flag;

            _tps = _flag ? _right : _left;
        }

    }

    private void LockOnTargetObjcet(GameObject target)
    {
        transform.LookAt(target.transform.position + new Vector3(0, 1.0f, 0), Vector3.up);
    }

    //カメラフラグの取得
    public bool GetFlag()
    {
        return _targetFlag;
    }


    
}

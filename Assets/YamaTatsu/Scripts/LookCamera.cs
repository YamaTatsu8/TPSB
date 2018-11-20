using System.Collections;
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

    // Use this for initialization
    void Start () {

        controller = GameController.Instance;

        //target = GameObject.FindGameObjectWithTag("Target");

        //_right = GameObject.Find("CameraRight");

        //_left = GameObject.Find("CameraLeft");

        //_tps = _right;
        
    }
	
	// Update is called once per frame
	void Update () {

        if(_cameraFlag == false)
        {
            _cameraFlag = true;

            target = GameObject.FindGameObjectWithTag("Enemy");

            _right = GameObject.Find("CameraRight");

            _left = GameObject.Find("CameraLeft");

            _tps = _right;

        }

        Vector3 pos;

        //
        pos = (_tps.transform.position - target.transform.position);

        //playerのポジションに入れる
        transform.position = _tps.transform.position + pos.normalized * 3 + _offset;

        _target = target;

        if(_target)
        {
            LockOnTargetObjcet(_target);
        }

        float angle_x = 180f <= transform.eulerAngles.x ? transform.eulerAngles.x - 360 : transform.eulerAngles.x;
        transform.eulerAngles = new Vector3(
            Mathf.Clamp(angle_x, ANGLE_LIMIT_DOWN, ANGLE_LIMIT_UP),
            transform.eulerAngles.y,
            transform.eulerAngles.z);

        if (controller.ButtonDown(Button.R3))
        {
            _flag = !_flag;

            _tps = _flag ? _right : _left;
        }

    }

    private void LockOnTargetObjcet(GameObject target)
    {

         //target.transform.position + new Vector3(0, 0.5f, 0);

        transform.LookAt(target.transform, Vector3.up);
    }

    
}

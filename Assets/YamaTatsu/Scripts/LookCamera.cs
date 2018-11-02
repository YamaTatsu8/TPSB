using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCamera : MonoBehaviour {

    //プレイヤーオブジェクト
    [SerializeField]
    private GameObject _player;

    //カメラ
    private GameObject _mainCamera;

    //ターゲット
    
    private GameObject _target;

    //視界角度制限
    private const float ANGLE_LIMIT_UP = 60f;
    private const float ANGLE_LIMIT_DOWN = -60f;

    //offset
    private Vector3 _offset;

    [SerializeField]
    private GameObject target;

    //親の情報
    [SerializeField]
    private GameObject _parent;

    //コントローラのスクリプト
    GameController controller;

    //カメラ切り替え
    //右側
    private Vector3 _right = new Vector3(2, 2, 0);
    //左側
    private Vector3 _left = new Vector3(-2, 2, 0);

    //切り替え
    private bool _flag = true;

    // Use this for initialization
    void Start () {

        controller = GameController.Instance;

        _mainCamera = this.gameObject;
       
        _offset = _right;

        _parent = gameObject.transform.parent.gameObject;

        if(_parent.tag == "Player")
        {
            target = GameObject.FindGameObjectWithTag("Target");
        }

    }
	
	// Update is called once per frame
	void Update () {

        

        Vector3 pos;

        //
        pos = (_player.transform.position - target.transform.position);

        //playerのポジションに入れる
        transform.position = _player.transform.position + pos.normalized * 3 + _offset;

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

            _offset = _flag ? _right : _left;
        }

    }

    private void LockOnTargetObjcet(GameObject target)
    {

         //target.transform.position + new Vector3(0, 0.5f, 0);

        transform.LookAt(target.transform, Vector3.up);
    }

    
}

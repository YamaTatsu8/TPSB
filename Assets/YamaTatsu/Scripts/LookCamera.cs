using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCamera : MonoBehaviour {

    //プレイヤーオブジェクト
    [SerializeField]
    private GameObject _player;

    //カメラ
    private GameObject _mainCamera;

    //LookOnスクリプト
    private LookOnTarget _lookOnTarget;

    //ターゲット
    private GameObject _target;

    //視界角度制限
    private const float ANGLE_LIMIT_UP = 60f;
    private const float ANGLE_LIMIT_DOWN = -60f;

    //offset
    private Vector3 _offset;

    private GameObject target;

    // Use this for initialization
    void Start () {

        _mainCamera = Camera.main.gameObject;
        //_player = GameObject.FindGameObjectWithTag("Player");
        _lookOnTarget = _player.GetComponentInChildren<LookOnTarget>();

        _offset = new Vector3(0, 2, 0);

        target = GameObject.FindGameObjectWithTag("Player");

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

    }

    private void LockOnTargetObjcet(GameObject target)
    {
        transform.LookAt(target.transform, Vector3.up);
    }

}

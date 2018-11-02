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

    // Use this for initialization
    void Start () {

        _mainCamera = this.gameObject;
       
        _offset = new Vector3(0, 2, 0);

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

    }

    private void LockOnTargetObjcet(GameObject target)
    {

         //target.transform.position + new Vector3(0, 0.5f, 0);

        transform.LookAt(target.transform, Vector3.up);
    }

    
}

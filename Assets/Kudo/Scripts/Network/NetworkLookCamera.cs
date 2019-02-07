using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkLookCamera : MonoBehaviour {

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

<<<<<<< HEAD
    //ロックオン解除時のオブジェ
    private GameObject _cameraObj;
=======
    [SerializeField]
    private GameObject target;
>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f

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

<<<<<<< HEAD
    //ロックオン解除フラグ
    private bool _targetFlag;

=======
>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f
    // Use this for initialization
    void Start () {

        controller = GameController.Instance;

<<<<<<< HEAD
=======
        //target = GameObject.FindGameObjectWithTag("Target");

        //_right = GameObject.Find("CameraRight");

        //_left = GameObject.Find("CameraLeft");

        //_tps = _right;
        
>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f
    }
	
	// Update is called once per frame
	void Update () {

<<<<<<< HEAD
        Debug.Log("かめら_player:" + _player);

        if (_cameraFlag == false)
        {
            if (_target == null || _cameraObj == null || _right == null || _left == null)
            {
                _target = serchTag(gameObject, "Player");

                Debug.Log("Update_target:" + _target);

                _cameraObj = GameObject.Find("CameraObj");
=======
        if(_cameraFlag == false)
        {

            // -ターゲットが見つかるまでターゲットを探し続ける
            if(target == null || _right == null || _left == null)
            {
                target = GameObject.FindGameObjectWithTag("Enemy");
>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f

                _right = GameObject.Find("CameraRight");

                _left = GameObject.Find("CameraLeft");
<<<<<<< HEAD

=======
>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f
            }
            else
            {
                _cameraFlag = true;
<<<<<<< HEAD

            }
            _tps = _right;
            return;
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

            pos = (_tps.transform.position - _target.transform.position);
=======
            }

            _tps = _right;

            return;
        }

        if (target != null)
        {
            Vector3 pos;

            //
            pos = (_tps.transform.position - target.transform.position);
>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f

            //playerのポジションに入れる
            transform.position = _tps.transform.position + pos.normalized * 3 + _offset;

<<<<<<< HEAD
=======
            _target = target;
>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f

            if (_target)
            {
                LockOnTargetObjcet(_target);
            }
<<<<<<< HEAD
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
=======

>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f
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

<<<<<<< HEAD
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

    GameObject serchTag(GameObject nowObj, string tagName)
    {
        float tmpDis = 0;           //距離用一時変数
        float nearDis = 0;          //最も近いオブジェクトの距離
        //string nearObjName = "";    //オブジェクト名称
        GameObject targetObj = null; //オブジェクト

        //タグ指定されたオブジェクトを配列で取得する
        foreach (GameObject obs in GameObject.FindGameObjectsWithTag(tagName))
        {
            //自身と取得したオブジェクトの距離を取得
            tmpDis = Vector3.Distance(obs.transform.position, nowObj.transform.position);

            //オブジェクトの距離が近いか、距離0であればオブジェクト名を取得
            //一時変数に距離を格納
            if ( nearDis < tmpDis && _player != null && _player != obs)
            {
                nearDis = tmpDis;
                //nearObjName = obs.name;
                targetObj = obs;
                Debug.Log("関数_targetobj:" + targetObj);
            }

        }
        //最も近かったオブジェクトを返す
        //return GameObject.Find(nearObjName);
        return targetObj;
    }

    //指定されたタグの中で最も近いものを取得
    GameObject serchTagObj(GameObject nowObj, string tagName)
    {
        float tmpDis = 0;           //距離用一時変数
        float nearDis = 0;          //最も近いオブジェクトの距離
        //string nearObjName = "";    //オブジェクト名称
        GameObject targetObj = null; //オブジェクト

        //タグ指定されたオブジェクトを配列で取得する
        foreach (GameObject obs in GameObject.FindGameObjectsWithTag(tagName))
        {
            //自身と取得したオブジェクトの距離を取得
            tmpDis = Vector3.Distance(obs.transform.position, nowObj.transform.position);

            //オブジェクトの距離が近いか、距離0であればオブジェクト名を取得
            //一時変数に距離を格納
            if (nearDis == 0 || nearDis > tmpDis)
            {
                    nearDis = tmpDis;
                    //nearObjName = obs.name;
                    targetObj = obs;

            }

        }

        //最も近かったオブジェクトを返す
        //return GameObject.Find(nearObjName);
        return targetObj;
    }

    public void SetPlayer(GameObject obj)
    {
        if(_player == null)
        {
            _player = obj;
        }
    }
=======

    }

    private void LockOnTargetObjcet(GameObject target)
    {

         //target.transform.position + new Vector3(0, 0.5f, 0);

        transform.LookAt(target.transform, Vector3.up);
    }

    
>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f
}

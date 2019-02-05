using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    //コントローラのスクリプト
    GameController controller;

    //インターバル
    const int TIME_INTERVAL = 5;

    //武器の切り替え
    private bool _changeWeapon = true;

    //武器の名前
    [SerializeField]
    private string _weaponName1;
    [SerializeField]
    private string _weaponName2;

    private string _subWeaponName;

    private GameObject _cameraObj;

    //武器のオブジェクト
    [SerializeField]
    private GameObject _weapon1;

    [SerializeField]
    private GameObject _weapon2;

    [SerializeField]
    private GameObject _subWeapon;

    //右手
    [SerializeField]
    private GameObject _rightHand;

    //アニメーター
    private Animator _animator;

    private Vector3 Player_pos;

    //フラグ
    private bool _flag;

    //Model
    [SerializeField]
    private GameObject _model;

    //敵
    [SerializeField]
    private GameObject _target;

    //
    private bool _weaponFlag = true;

    //
    [SerializeField]
    private GameObject[] _weapon;

    [SerializeField]
    private GameObject _obj;

    //ロックオン解除フラグ
    private bool _targetFlag;

    // Use this for initialization
    void Start () {

        controller = GameController.Instance;

        _animator = _obj.GetComponent<Animator>();

        //PlayerSystemから情報をもらってくる
        PlayerSystem playerSystem = FindObjectOfType<PlayerSystem>();

        _weaponName1 = playerSystem.getMain1();

        _weaponName2 = playerSystem.getMain2();

        _subWeaponName = playerSystem.getSub();

        for(int i = 0; i < _weapon.Length; i++)
        {
            if(_weapon[i].name == _weaponName1)
            {
                _weapon1 = _weapon[i];
            }

            if(_weapon[i].name == _weaponName2)
            {
                _weapon2 = _weapon[i];
            }
        }

        _flag = false;

        _weapon1.SetActive(true);

        _weapon2.SetActive(false);

        _target = serchTag(gameObject,"Player");

        _cameraObj = GameObject.Find("CameraObj");

        //サブ武器の取得
        _subWeapon = GameObject.Find(_subWeaponName);

        playerSystem.Init();
    }
	
	// Update is called once per frame
	void Update () {

        controller.ControllerUpdate();

        //R1押されたらロックオンの切り替え
        if (controller.ButtonDown(Button.R1))
        {
            _targetFlag = !_targetFlag;
        }

        if (controller.ButtonDown(Button.X))
        {
            _weaponFlag = !_weaponFlag;
            if(_weaponFlag == true)
            {
                _weapon2.SetActive(false);
                _weapon1.SetActive(true);
            }
            else if(_weaponFlag == false)
            {
                _weapon1.SetActive(false);
                _weapon2.SetActive(true);
            }
            
        }
 
        if (controller.TriggerDown(Trigger.LEFT))
        {
            _animator.SetBool("Attack",true);
            if (_weaponFlag == true)
            {
                _weapon1.GetComponent<WeaponManager>().Attack();
            }
            else if(_weaponFlag == false)
            {
                _weapon2.GetComponent<WeaponManager>().Attack();
            }
            
            if (_flag == false)
            {
                if (_targetFlag == false)
                {
                    _model.transform.LookAt(_target.transform);
                }
                else if(_targetFlag == true)
                {
                    _model.transform.LookAt(_cameraObj.transform);
                }
                //_flag = true;
            }
        }
        else
        {
            //攻撃モーション
            _animator.SetBool("Attack", false);
        }

        if (controller.TriggerDown(Trigger.RIGHT))
        {
            _subWeapon.GetComponent<WeaponManager>().Attack();
        }

        Player_pos = transform.position;

    }

    private void OnCollisionStay(Collision collision)
    {
        //
        if (collision.gameObject.tag == "Ground")
        {
            _flag = false;
        }
        else
        {

        }

    }

    //
    public Vector3 getPosition()
    {
        Vector3 pos = Vector3.zero;

        if (_targetFlag == false)
        {
            pos = _target.transform.position + new Vector3(0, 1, 0);
        }
        else if (_targetFlag == true)
        {
            pos = _cameraObj.transform.position + new Vector3(0, 1, 0);
        }
        return pos;
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
            if (nearDis < tmpDis)
            {
                nearDis = tmpDis;
                //nearObjName = obs.name;
                targetObj = obs;
                Debug.Log(targetObj);
            }

        }
        //最も近かったオブジェクトを返す
        //return GameObject.Find(nearObjName);
        return targetObj;
    }

}

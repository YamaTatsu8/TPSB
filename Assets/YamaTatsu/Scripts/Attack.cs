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

        _target = GameObject.FindGameObjectWithTag("Enemy");

        playerSystem.Init();
    }
	
	// Update is called once per frame
	void Update () {

        controller.ControllerUpdate();

        //Debug.Log(_changeWeapon);

        if(controller.ButtonDown(Button.X))
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
            Vector3 diff = transform.position - Player_pos;

            if (_weaponFlag == true)
            {
                _weapon1.GetComponent<WeaponManager>().Attack();
            }
            else if(_weaponFlag == false)
            {
                _weapon2.GetComponent<WeaponManager>().Attack();
            }
            //transform.rotation = Quaternion.LookRotation(new Vector3(diff.x, 0, diff.z));

            if (_flag == false)
            {
                _model.transform.LookAt(_target.transform);
                //_flag = true;
            }
            //攻撃モーション
            _animator.SetBool("Attack", true);
        }
        else
        {
            //攻撃モーション
            _animator.SetBool("Attack", false);
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
        return _target.transform.position + new Vector3(0,1,0);
    }

}

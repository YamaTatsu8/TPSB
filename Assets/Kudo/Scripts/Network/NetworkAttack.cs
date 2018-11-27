using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkAttack : MonoBehaviour {

    //コントローラのスクリプト
    GameController controller;

    //インターバル
    const int TIME_INTERVAL = 5;

    //武器の切り替え
    private bool _changeWeapon = true;

    //武器の名前
    private string _weaponName1;

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
    private GameObject _target;

    //
    private bool _weaponFlag = true;

    //
    [SerializeField]
    private GameObject[] _weapon;

    // -ネットワーク
    private PhotonView _photonView;

    // Use this for initialization
    void Start () {

        controller = GameController.Instance;

        //PlayerSystemから情報をもらってくる
        PlayerSystem playerSystem = FindObjectOfType<PlayerSystem>();

        _weaponName1 = playerSystem.getMain1();
        _weaponName2 = playerSystem.getMain2();
        _subWeaponName = playerSystem.getSub();

        for (int i = 0; i < _weapon.Length; i++)
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

        //Resorcesから武器を探して装備する
        //_weapon1 = (GameObject)Instantiate(Resources.Load("Prefabs/" + _weaponName1));

        //_weapon2 = (GameObject)Instantiate(Resources.Load("Prefabs/" + _weaponName2));

        _weapon1.SetActive(true);

        _weapon2.SetActive(false);

        //右手の子供にする
        //_weapon1.transform.parent = _rightHand.transform;
        //_weapon1.transform.position = _rightHand.transform.position;

        //_weapon2.transform.parent = _rightHand.transform;
        //_weapon2.transform.position = _rightHand.transform.position;

        //アニメーターのコンポーネント
        _animator = GetComponent<Animator>();

        _target = GameObject.FindGameObjectWithTag("Enemy");

        // -photonviewのコンポーネント
        _photonView = GetComponent<PhotonView>();
    }
	
	// Update is called once per frame
	void Update () {

        controller.ControllerUpdate();

        //Debug.Log(_changeWeapon);

        // -誰がボタンを押したかをチェックする(自身が触ったらtrue
        if (_photonView.isMine)
        {
            if (controller.ButtonDown(Button.X))
            {
                _weaponFlag = !_weaponFlag;
                if (_weaponFlag == true)
                {
                    _weapon2.SetActive(false);
                    _weapon1.SetActive(true);
                }
                else if (_weaponFlag == false)
                {
                    _weapon1.SetActive(false);
                    _weapon2.SetActive(true);
                }
            }

            Debug.Log(_weaponName2);


            if (controller.TriggerDown(Trigger.LEFT))
            {
                Vector3 diff = transform.position - Player_pos;

                if (_weaponFlag == true)
                {
                    _weapon1.GetComponent<NetworkWeaponManager>().Attack();
                }
                else if (_weaponFlag == false)
                {
                    _weapon2.GetComponent<NetworkWeaponManager>().Attack();
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
        if (transform.tag == "Player")
        {
            _target = GameObject.FindGameObjectWithTag("Enemy");
            return _target.transform.position + new Vector3(0, 1, 0);
        }
        else
        {
            _target = GameObject.FindGameObjectWithTag("Player");
            return _target.transform.position + new Vector3(0, 0.5f, 0);
        }
    }

}

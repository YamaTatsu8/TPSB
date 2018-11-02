using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    //コントローラのスクリプト
    GameController controller;

    //弾を飛ばす力
    [SerializeField]
    private float _bulletPower = 300.0f;

    //
    private int _timeCount = 1;

    //インターバル
    const int TIME_INTERVAL = 5;

    //武器の切り替え
    private bool _changeWeapon = true;

    [SerializeField]
    private GameObject _Bullet1;
    [SerializeField]
    private GameObject _Bullet2;

    //武器の名前
    [SerializeField]
    private string _weaponName1;
    [SerializeField]
    private string _weaponName2;
    [SerializeField]
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

    // Use this for initialization
    void Start () {

        controller = GameController.Instance;
        //this.gameObject.GetComponent<Shot>()._bulletPrefab = _Bullet1;

        //PlayerSystemから情報をもらってくる
        PlayerSystem playerSystem = FindObjectOfType<PlayerSystem>();

        _weaponName1 = playerSystem.getMain1();

        _weaponName2 = playerSystem.getMain2();

        _subWeaponName = playerSystem.getSub();

        _flag = false;

        //Resorcesから武器を探して装備する
        _weapon1 = (GameObject)Instantiate(Resources.Load("Prefabs/" + _weaponName1));

        //右手の子供にする
        _weapon1.transform.parent = _rightHand.transform;
        _weapon1.transform.position = _rightHand.transform.position;

        //アニメーターのコンポーネント
        _animator = GetComponent<Animator>();

        _target = GameObject.Find("Enemy");
    }
	
	// Update is called once per frame
	void Update () {

        controller.ControllerUpdate();

        //Debug.Log(_changeWeapon);

        if (controller.TriggerDown(Trigger.LEFT))
        {
            Vector3 diff = transform.position - Player_pos;

            _weapon1.GetComponent<WeaponManager>().Attack();

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
        return _target.transform.position;
    }

}

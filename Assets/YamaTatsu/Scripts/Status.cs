using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour {

    //　HP
    [SerializeField]
    private float _HP = 100;

    //アニメーター
    private Animator _animator;

    [SerializeField]
    private GameObject _obj;

    private GameObject _hitObj;
    private bool _isEnemy;
    private int _hitNum;
    private string _hitStr;
    private float _time;

    private GameObject _enemy;
    private bool _isTraining;

    // Use this for initialization
    void Start () {

        _animator = _obj.GetComponent<Animator>();

        if (this.gameObject.name == "TrainingEnemy")
        {
            _isEnemy = true;
            _hitNum = 0;
            _time = 3.0f;
            _hitStr = " damage!!";
            _hitObj = GameObject.Find("HitCnt");
            _hitObj.SetActive(false);
        }
        else
        {
            _isEnemy = false;

            if ((_enemy = GameObject.Find("TrainingEnemy")) != null)
            {
                _isTraining = true;
            }
            else
            {
                _isTraining = false;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
        // HPが0以下の場合破壊
        if(_HP < 0)
        {
            //Destroy(this.gameObject);
        }

        if(_HP >= 100)
        {
            //_HP = 100;
        }

        if(_isTraining)
        {
            _time -= Time.deltaTime;
            if (_time <= 0.0f)
            {
                _HP = 100;
                _time = 1.0f;
            }
        }

        if (_hitObj == null) { return; }
        if (!_hitObj.activeSelf) { return; }
        _time -= Time.deltaTime;
        if (_time <= 0.0f)
        {
            _hitNum = 0;
            _time = 3.0f;
            _HP = 100;
            _hitObj.SetActive(false);
        }

        _animator.SetTrigger("Idle");
    }

    public bool getDestroy()
    {
        return true;
    }

    //ダメージを与える処理
    public void hitDamage(int damage)
    {
        //　ターゲットが敵の場合
        if (_isEnemy)
        {
            if (!_hitObj.activeSelf) { _hitObj.SetActive(true); }
            _hitNum += damage;
            _hitObj.GetComponent<Text>().text = _hitNum.ToString() + _hitStr;
            _time = 1.0f;

            if (_HP >= 1)
            {
                float hp = _HP;
                hp -= damage;
                if (hp >= 1)
                {
                    _HP -= damage;
                }
            }
        }
        else if (_isTraining)
        {
            _time = 1.0f;
            if (_HP >= 1)
            {
                float hp = _HP;
                hp -= damage;
                if (hp >= 1)
                {
                    _HP -= damage;
                }
            }
        }
        else
        {
            _HP -= damage;
        }
        //_animator.SetTrigger("Damage");
    }

    public void RecoveryHP(int heal)
    {
        Debug.Log("回復");
        _HP += heal;
    }

    public float getHP()
    {
        return (float)_HP;
    }
  
}

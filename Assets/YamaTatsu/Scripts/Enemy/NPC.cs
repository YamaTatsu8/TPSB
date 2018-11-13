using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

    //武器のステート
    private enum STATE_WEAPON
    {
        WEAPON1,        //武器1
        WEAPON2,        //武器2
        SUBWEAPON       //サブ武器
    }

    //Player 
    private GameObject _player;

    //武器1
    //[SerializeField]
    private GameObject _weapon1;

    //武器2
    //[SerializeField]
    private GameObject _weapon2;

    //サブ武器
    //[SerializeField]
    private GameObject _subWeapon;
    //範囲
    [SerializeField]
    private float _range = 200.0f;

    //範囲フラグ
    private bool _rangeFlag;

    [SerializeField]
    private Rigidbody rb;

    //コライダー
    [SerializeField]
    private GameObject _collider;

    //移動スピード
    [SerializeField]
    private float _speed = 5;

    //移動ステート
    private int _moveState = 0;
    
    //
    //private void
    
    //壁判定
    private bool _wallFlag = false;

    //移動ステート
    enum MOVE_STATE
    {
        FRONT,
        LEFT,
        RIGHT,
        BACK,
        STOP,
        JUMP
    }

    //
    private float _interval;

    private float _timer = 0.0f;

    //左右移動フラグ
    private bool _wallMoveFlag = false;

    //
    private bool _playerFlag = false;
  
	// Use this for initialization
	void Start () {
    
        _rangeFlag = true;

        _interval = Random.Range(5.0f, 10.0f);

	}
	
	// Update is called once per frame
	void Update () {

        if(_playerFlag == false)
        {
            _playerFlag = true;
            //敵を探す
            _player = GameObject.Find("Player");

        }

        _wallFlag = _collider.GetComponent<WallCollider>().WallHit();

        //プレイヤーの方向に向かって移動
        Debug.Log(_player);
        Vector3 diff = _player.transform.position - transform.position;

        //
        float distance = (diff.x * diff.x) / 2 + (diff.z * diff.z) / 2;

        if(_wallFlag == true && _wallMoveFlag == false)
        {
            _moveState = Random.Range(1, 2);
            _wallMoveFlag = true;
        }
        else if(_wallFlag == false)
        {
            _wallFlag = false;
            _moveState = 0;
        }

        //
        _timer += Time.deltaTime;

        if(_timer >= _interval)
        {
            _timer = 0;
            _interval = Random.Range(5.0f, 10.0f);
            _moveState = (int)Random.Range(0, 4);
        }

        SetFunction(_moveState);

        transform.LookAt(_player.transform);
	}

    //
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Wall")
        {

        }
    }

    //行動関数
    private void SetFunction(int num)
    {
        switch (num)
        {
            case (int)MOVE_STATE.FRONT:
                Vector3 velocity = gameObject.transform.rotation * new Vector3(0, 0, _speed);
                gameObject.transform.position += velocity * Time.deltaTime;
                break;
            case (int)MOVE_STATE.LEFT:
                velocity = gameObject.transform.rotation * new Vector3(_speed, 0, 0);
                gameObject.transform.position += velocity * Time.deltaTime;
                break;
            case (int)MOVE_STATE.RIGHT:
                velocity = gameObject.transform.rotation * new Vector3(-_speed, 0, 0);
                gameObject.transform.position += velocity * Time.deltaTime;
                break;
            case (int)MOVE_STATE.BACK:
                velocity = gameObject.transform.rotation * new Vector3(0, 0, -_speed);
                gameObject.transform.position += velocity * Time.deltaTime;
                break;
            case (int)MOVE_STATE.STOP:
                velocity = gameObject.transform.rotation * new Vector3(0, 0, 0);
                gameObject.transform.position += velocity * Time.deltaTime;
                break;
            case (int)MOVE_STATE.JUMP:
                velocity = gameObject.transform.rotation * new Vector3(0, _speed, 0);
                gameObject.transform.position += velocity * Time.deltaTime;
                break;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    enum State
    {
        Chase,
        Attack,
    }

    public int _enemyHealth = 10;           // 体力

    public float _rotateSpeed = 45.0f;

    private GameObject _target = null;      // 追跡する対象
    private NavMeshAgent _agent = null;

    public GameObject _explosion = null;    //爆発エフェクト
    private bool _isQuitting = false;

    private State _state = State.Chase;

    public GameObject _weapon;

    public Vector3 _targetPos;

    // 後で別の場所に移す
    private GameObject _point;
    
    // Use this for initialization
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        _target = GameObject.FindGameObjectWithTag("Player");

        // 後で別の場所に移す
        _point = GameObject.Find("PointNum");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_target == null)
        {
            _target = GameObject.FindGameObjectWithTag("Player");
        }
        _targetPos = _target.transform.position;

        //　キャラクターを追いかける状態であればキャラクターの目的地を再設定
        if (_state == State.Chase)
        {
            Debug.Log("Chase1");
            //　攻撃する距離だったら攻撃
            if (_agent.remainingDistance < 5.0f && Mathf.Abs(_agent.transform.position.y - _target.transform.position.y) <= 3)
            {
                Debug.Log("Chase2");
                _state = State.Attack;
            }
            else
            {
                // ターゲットの位置を目的地に設定
                _agent.SetDestination(_target.transform.position);
            }
        }
        else if (_state == State.Attack)
        {
            //　プレイヤーの方向を取得
            var playerDirection = new Vector3(_target.transform.position.x, transform.position.y, _target.transform.position.z) - transform.position;
            //　敵の向きをプレイヤーの方向に少しづつ変える
            var dir = Vector3.RotateTowards(transform.forward, playerDirection, _rotateSpeed * Time.deltaTime, 0f);
            //　算出した方向の角度を敵の角度に設定
            transform.rotation = Quaternion.LookRotation(dir);

            _weapon.GetComponent<BossWeapon>().Attack();

            _state = State.Chase;
            Debug.Log("Attack");
        }
    }

    public void Damage(int damage)
    {
        
        _enemyHealth -= damage;
        if (_enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetTarget(GameObject target)
    {
        this._target = target;
    }

    void OnApplicationQuit()
    {
        _isQuitting = true;
    }

    void OnDestroy()
    {
        if (!_isQuitting && _explosion != null)
        {
            GameObject.Instantiate(_explosion, transform.position, Quaternion.identity);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICharacterControl : MonoBehaviour
{
    private float _rotationSmooth = 2.5f;
    private bool _isWait;

    // 追跡するターゲットの場所
    private Transform _target = null;

    [SerializeField]
    private GameObject _weapon = null;

    // Use this for initialization
    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        _isWait = false;
    }

    // Update is called once per frame
    void Update()
    {
        //　待機状態なら以下の処理を行わない
        if (_isWait) { return; }

        if (_target == null)
        {
            _target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        else
        {
            // プレイヤーの方向を向く
            Quaternion targetRotation = Quaternion.LookRotation(_target.position - transform.position);
            targetRotation.x = 0.0f;
            targetRotation.z = 0.0f;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _rotationSmooth);

            _weapon.GetComponent<EnemyWeaponManager>().Attack();
        }
    }

    public void SetWaitingMode(bool wait)
    {
        _isWait = wait;
    }

    public Vector3 GetTargetPosition()
    {
        return _target.position;
    }
}

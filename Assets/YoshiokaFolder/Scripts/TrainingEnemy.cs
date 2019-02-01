using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingEnemy : MonoBehaviour
{
    //　武器
    [SerializeField]
    private GameObject _weapon;

    //　銃を撃つ対象
    private Transform _target;

    //　待機状態か否か
    private bool _isWait;
    //
    private float _rotationSmooth = 2.5f;

	// Use this for initialization
	void Start ()
    {
        Initialize();
	}

    public void Initialize()
    {
        _target = GameObject.Find("Player").transform;
        _isWait = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //　待機状態なら以下の処理を行わない
		if (_isWait) { return; }

        // プレイヤーの方向を向く
        Quaternion targetRotation = Quaternion.LookRotation(_target.position - transform.position);
        targetRotation.x = 0.0f;
        targetRotation.z = 0.0f;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _rotationSmooth);

        Debug.Log("うんち");
        _weapon.GetComponent<EnemyWeaponManager>().Attack();

    }

    public void SetWaitingMode(bool isWait)
    {
        _isWait = isWait;
    }
}

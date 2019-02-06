using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitController : MonoBehaviour {

    [SerializeField]
    private float _fireRate = 3.0f;

    private float _nextTime = 0.0f;

    WeaponManager _manager;
    
	void Start ()
    {
        _manager = this.GetComponent<WeaponManager>();
	}
	
	void Update ()
    {
        // 一定時間経過で撃つ
        if (Time.time > _nextTime)
        {
            _nextTime = Time.time + _fireRate;

            _manager.Attack();
        }
        
		// 弾が0になったら消す
        if(_manager.RemainingBullets == 0)
        {
            Destroy(this.gameObject);
        }
	}
}

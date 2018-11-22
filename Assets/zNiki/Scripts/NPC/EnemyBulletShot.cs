using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyBulletShot : MonoBehaviour
{
    // 発射間隔
    private float _fireRate;

    // 次弾発射までの時間
    private float _nextTime = 0;

    // カメラ
    private Camera _fpsCam;

    // 銃口
    private Transform _muzzle;

    // 弾のプレハブ
    private GameObject _bulletPrefab;

    // 弾速
    private float _bulletSpeed = 50.0f;

    // 射程(DrawLineの距離)
    private float _range = 30.0f;

    private Vector3 _targetPos = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        _fireRate = 1.0f / this.GetComponent<EnemyWeaponManager>().RoundsPerSecond;

        _fpsCam = Camera.FindObjectOfType<Camera>();

        _muzzle = this.GetComponent<EnemyWeaponManager>().Muzzle;

        _bulletPrefab = this.GetComponent<EnemyWeaponManager>().BulletPrefab;
    }

    public bool Shot(float fireRate)
    {
        _fireRate = fireRate;

        _targetPos = transform.root.GetComponent<AICharacterControl>().GetTargetPosition();

        _targetPos.y += 0.5f; 

        if (Time.time > _nextTime)
        {
            // 次弾発射までの時間更新
            _nextTime = Time.time + _fireRate;

            // 弾丸の生成
            GameObject bulletClone = Instantiate<GameObject>(_bulletPrefab);
            // bulletCloneの位置を調整
            bulletClone.transform.position = _muzzle.position;

            RaycastHit hit;

            if (_targetPos != Vector3.zero)
            {
                bulletClone.GetComponent<Rigidbody>().velocity = (_targetPos - bulletClone.transform.position).normalized * _bulletSpeed;
            }

            bulletClone.GetComponent<EnemyBulletController>().DeleteBullet(bulletClone);

            return true;
        }
        return false;
    }
}
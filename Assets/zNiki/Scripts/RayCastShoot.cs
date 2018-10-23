﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RayCastShoot : MonoBehaviour
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

    // 消滅までの時間
    private float _destroyTime = 3.0f;

    // 射程(DrawLineの距離)
    private float _range = 30.0f;

    // Use this for initialization
    void Start ()
    {
        _fireRate = 1.0f / this.GetComponent<WeaponManager>().RoundsPerSecond;
        
        _fpsCam = Camera.FindObjectOfType<Camera>();

        _muzzle = this.GetComponent<WeaponManager>().Muzzle;

        _bulletPrefab = this.GetComponent<WeaponManager>().BulletPrefab;
    }

    public bool Shot(float fireRate)
    {
        _fireRate = fireRate;

        if (Time.time > _nextTime)
        {
            // 次弾発射までの時間更新
            _nextTime = Time.time + _fireRate;

            // ビューポートの中心にベクターを作成
            Vector3 rayOrigin = _fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

            Ray ray = new Ray(rayOrigin, _fpsCam.transform.forward);

            // 弾丸の生成
            GameObject bulletClone = Instantiate<GameObject>(_bulletPrefab);
            // bulletCloneの位置を調整
            bulletClone.transform.position = _muzzle.position;

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, _range))
            {
                // レイのヒットした地点に飛ばす
                bulletClone.GetComponent<Rigidbody>().velocity = (hit.point - bulletClone.transform.position).normalized * _bulletSpeed;
            }
            else
            {
                // 射程距離分進んだ地点に飛ばす
                bulletClone.GetComponent<Rigidbody>().velocity = (ray.GetPoint(_range) - bulletClone.transform.position).normalized * _bulletSpeed;
            }

            Coroutine coroutine = this.Delay(_destroyTime, () =>
            {
                Destroy(bulletClone);
            });

            return true;
        }
        return false;
    }
}
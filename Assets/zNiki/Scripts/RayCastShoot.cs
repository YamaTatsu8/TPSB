using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RayCastShoot : MonoBehaviour
{
    [SerializeField]
    private float _bulletSpeed = 50.0f;     // 弾速
    [SerializeField]
    private float _fireRate = 0.1f;         // 連射速度

    [SerializeField]
    private float _destroyTime = 3.0f;      // 消滅までの時間

    [SerializeField]
    private Transform _muzzle;              // 銃口

    public GameObject _bulletPrefab       // 弾のプレハブ
    {
        get;
        set;
    }

    private float _range = 30.0f;           // 射程(DrawLineの距離)

    private float _nextTime;                // 次弾発射までの時間

    private Camera _fpsCam;                 // カメラ

    private AudioSource _gunAudio;      
    [SerializeField]
    private AudioClip _shotSound;           // 発射音

    // Use this for initialization
    void Start ()
    {
        // 各コンポーネントの取得
        _fpsCam = GetComponentInChildren<Camera>();
        _gunAudio = GetComponent<AudioSource>();
	}

    public void Shot()
    {
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

            StartCoroutine(DelayMethod(_destroyTime, () =>
            {
                Destroy(bulletClone);
            }));

            //_gunAudio.clip = _shotSound;
            //_gunAudio.Play();
        }
    }

    private IEnumerator DelayMethod(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }
}

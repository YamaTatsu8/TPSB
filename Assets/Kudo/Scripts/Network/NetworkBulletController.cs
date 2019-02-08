﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkBulletController: MonoBehaviour
{
    // 弾のダメージ
    [SerializeField]
    private int _bulletDamage = 1;

    // 消滅までの時間
    [SerializeField]
    private float _destroyTime = 3.0f;

    // SEの名前
    [SerializeField]
    private string _seName = "";

    // 攻撃とヒールの区別用
    private bool _isAttack = true;

    // 多重ヒール防止用
    private bool _isHealed = false;

    // オーディオマネージャー
    private AudioManager _audioManager;

    // -PhotonView
    private PhotonView _photonView;

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
    }

    public int BulletDamage
    {
        get { return _bulletDamage; }
    }

    public bool IsAttack
    {
        set { _isAttack = value; }
    }

    public void DeleteBullet(GameObject bulletClone)
    {
        this.Delay(_destroyTime, () =>
        {
            if (_seName != "")
            {
                _audioManager.PlaySE(_seName);
            }

            Destroy(bulletClone);
        });
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        //if (!_photonView.isMine)
        //{
        //    return;
        //}

        if (_isAttack)
        {
            if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Target")
            {
                collision.gameObject.GetComponent<Status>().hitDamage(_bulletDamage);

                if (_seName != "")
                {
                    _audioManager.PlaySE(_seName);
                }

                Destroy(this.gameObject);
            }

            if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Target")
            {
                if(!collision.gameObject.GetComponent<PhotonView>().isMine)
                {
                    return;
                }
                collision.gameObject.GetComponent<NetworkStatus>().hitDamage(_bulletDamage);
                //object[] args1 = new object[] { _bulletDamage };

                //collision.gameObject.GetComponent<NetworkStatus>().GetComponent<PhotonView>().RPC("hitDamage", PhotonTargets.All, args1);

                if (_seName != "")
                {
                    _audioManager.PlaySE(_seName);
                }

                Destroy(this.gameObject);
            }

            if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Ground")
            {
                if (_seName != "")
                {
                    _audioManager.PlaySE(_seName);
                }

                Destroy(this.gameObject);
            }
        }
        else
        {
            if (collision.gameObject.tag == "Player" && !_isHealed)
            {
                collision.gameObject.GetComponent<NetworkStatus>().RecoveryHP(_bulletDamage);
                object[] args2 = new object[] { _bulletDamage };

                //collision.gameObject.GetComponent<NetworkStatus>().GetComponent<PhotonView>().RPC("RecoveryHP", PhotonTargets.All, args2);

                _isHealed = true;
            }
        }
    }
}

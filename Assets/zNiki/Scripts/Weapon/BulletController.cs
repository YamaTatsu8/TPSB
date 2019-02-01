using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController: MonoBehaviour
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
                collision.gameObject.GetComponent<Status>().RecoveryHP(_bulletDamage);

                _isHealed = true;
            }
        }
    }
}

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

    // オーディオマネージャー
    private AudioManager _audioManager;

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
}

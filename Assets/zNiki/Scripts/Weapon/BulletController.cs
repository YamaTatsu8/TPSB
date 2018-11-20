using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // 弾のダメージ
    [SerializeField]
    private int _bulletDamage = 1;

    // 消滅までの時間
    [SerializeField]
    private float _destroyTime = 3.0f;

    public void DeleteBullet(GameObject bulletClone)
    {
        this.Delay(_destroyTime, () =>
        {
            Destroy(bulletClone);
        });
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Target")
        {
            Destroy(this.gameObject);

            collision.gameObject.GetComponent<Status>().hitDamage(_bulletDamage);
        }
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
    }
}

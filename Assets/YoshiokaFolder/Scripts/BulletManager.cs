using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BulletManager : MonoBehaviour
{
    // bullet prefab
    [SerializeField]
    private GameObject bulletPrefab;
    // 弾丸発射点
    [SerializeField]
    private Transform muzzle;
    // 弾丸の速度
    [SerializeField]
    private float speed = 1000;
    //　弾丸の消えるまでの秒数
    [SerializeField]
    private float deleteTime = 5.0f;

    //　ダメージ
    [SerializeField]
    private float damage = 5.0f;



    //　ダメージSetter
    void SetDamage(float damageValue) { this.damage = damageValue; }
    //　ダメージGetter
    float GetDamage() { return this.damage; }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //　弾発射
        BulletShoot();
    }

    //　弾発射
    void BulletShoot()
    {
        // 仮
        // z キーが押された時
        if (Input.GetKeyDown(KeyCode.Z))
        {
            // 弾丸の複製
            GameObject bullets = Instantiate(bulletPrefab) as GameObject;

            Vector3 force;

            force = this.gameObject.transform.forward * speed;

            // Rigidbodyに力を加えて発射
            bullets.GetComponent<Rigidbody>().AddForce(force);

            // 弾丸の位置を調整
            bullets.transform.position = muzzle.position;

            Destroy(bullets, deleteTime);
        }
    }
}
